using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using LoadBalancer.Business;
using LoadBalancer.Business.RouteTableStorage;
using LoadBalancer.Models;

namespace LoadBalancer.Controllers
{
    public class HostController : ApiController
    {

        protected RouteTable RouteTable { get; }


        protected IAuthProvider AuthProvider { get; }


        protected RouteTableStorage RouteTableStorage { get; }

        public HostController(RouteTable routeTable, IAuthProvider authProvider, RouteTableStorage routeTableStorage)
        {
            RouteTable = routeTable;
            AuthProvider = authProvider;
            RouteTableStorage = routeTableStorage;
        }

        // DELETE: api/Host/5
        public void Delete(int id)
        {
        }

        [System.Web.Http.HttpGet]
        public AddHostResponce Add(string host, string port)
        {
            var requestForAdding = new RequestForAdding
            {
                Key = AuthProvider.GetVerificationString(),
                RequestId = Guid.NewGuid(),
                TimeOfGet = DateTime.Now,
                Host = host + ":" + port
            };
            RouteTable.RequestForAddings.Add(requestForAdding);
            RouteTableStorage.Save(RouteTable);
            return new AddHostResponce
            {
                RequestId = requestForAdding.RequestId,
                VerificationString = requestForAdding.Key
            };
        }

        public StatusCodeResult Verify(AddHostVerificationRequest request)
        {
            var rows = RouteTable.RequestForAddings.Where(x => x.RequestId == request.RequestId).ToArray();
            if (rows.Length != 1)
            {
                return new StatusCodeResult(HttpStatusCode.Forbidden, new HttpRequestMessage());
            }
            var requestForAdding = rows.FirstOrDefault();
            if (requestForAdding == null)
            {
                return new StatusCodeResult(HttpStatusCode.Conflict, new HttpRequestMessage());
            }
            RouteTable.RequestForAddings.Remove(requestForAdding);
            if ((requestForAdding.TimeOfGet - DateTime.Now).Seconds > 2)
            {
                RouteTableStorage.Save(RouteTable);
                return new StatusCodeResult(HttpStatusCode.BadRequest, new HttpRequestMessage());
            }
            if (requestForAdding.Host != request.Host + ":" + request.Port)
            {
                RouteTableStorage.Save(RouteTable);
                return new StatusCodeResult(HttpStatusCode.BadGateway, new HttpRequestMessage());
            }
            if (!AuthProvider.IsCorrect(request.EncodedString, requestForAdding.Key))
            {
                RouteTableStorage.Save(RouteTable);
                return new StatusCodeResult(HttpStatusCode.GatewayTimeout, new HttpRequestMessage());
            }

            var route = new Route { LastConnection = DateTime.Now, RouteId = Guid.NewGuid(), Host = request.Host + ":" + request.Port };
            RouteTable.Routes.Add(route);

            RouteTableStorage.Save(RouteTable);
            return new StatusCodeResult(HttpStatusCode.OK, new HttpRequestMessage());
        }
    }

    public class AddHostResponce
    {
        public Guid RequestId { get; set; }
        public string VerificationString { get; set; }
    }

    public class AddHostVerificationRequest
    {
        public Guid RequestId { get; set; }
        public string EncodedString { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
    }

    public interface IAuthProvider
    {
        string GetVerificationString();
        bool IsCorrect(string encoded, string verificationString);
    }

    public class AuthProvider : IAuthProvider
    {
        protected const string Key = "11110000111100001111000011110000";
        protected Random Random = new Random();

        public string GetVerificationString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 32; i++)
            {
                sb.Append(Random.Next() % 2);
            }
            return sb.ToString();
        }

        public bool IsCorrect(string encoded, string verificationString)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 32; i++)
            {
                sb.Append(encoded[i] == Key[i] ? "0" : "1");

            }
            var decoded = sb.ToString();
            return decoded == verificationString;
        }
    }

}
