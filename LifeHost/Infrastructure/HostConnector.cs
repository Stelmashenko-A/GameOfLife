using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace LifeHost.Infrastructure
{
    public class HostConnector
    {
        public HostMetadata Connect(string url)
        {
            var host = new HostMetadata();
            using (var client = new HttpClient())
            {
                var referer = ConfigurationSettings.AppSettings.Get("Referer");
                var strs = referer.Split(':');
                client.DefaultRequestHeaders.Referrer = new Uri(referer);
                AddHostResponce responce = null;
                var task = client.GetAsync(url + "/api/host/add?host=" + strs[0] + "&port=" + strs[1]).ContinueWith((taskwithresponse) =>
                        {
                            var response = taskwithresponse.Result;
                            var jsonString = response.Content.ReadAsStringAsync();
                            jsonString.Wait();
                            responce = JsonConvert.DeserializeObject<AddHostResponce>(jsonString.Result);

                        });
                task.Wait();
                var markedStr = MarkString(responce.VerificationString);
                var r = new AddHostVerificationRequest() { EncodedString = markedStr, RequestId = responce.RequestId, Host = strs[0], Port = strs[1] };
                var buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(r));
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpStatusCode result = HttpStatusCode.BadRequest;
                task = client.PostAsync(url + "/api/host/Verify", byteContent).ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    result = response.StatusCode;
                });
                task.Wait();
                if (result != HttpStatusCode.OK)
                {
                    throw new Exception(result.ToString());
                }
            }
            return host;
        }
        protected const string Key = "11110000111100001111000011110000";
        protected string MarkString(string str)
        {

            var sb = new StringBuilder();
            for (var i = 0; i < 32; i++)
            {
                sb.Append(str[i] == Key[i] ? "0" : "1");

            }
            return sb.ToString();

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
}