using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using LoadBalancer.Business;
using LoadBalancer.Business.RouteTableStorage;
using LoadBalancer.Models;
using Newtonsoft.Json;

namespace LoadBalancer.Controllers
{
    [RoutePrefix("values")]
    public class ValuesController : ApiController
    {
        private readonly RouteTableStorage _routeTableStorage;

        public ValuesController(RouteTableStorage routeTableStorage)
        {
            _routeTableStorage = routeTableStorage;
        }

        [HttpPost]
        [Route("process")]
        public IHttpActionResult Process(RequestForProcessing request)
        {
            request.Id = Guid.NewGuid();

            var httpClient = new HttpClient();
            var routeTable = _routeTableStorage.Load();

            var freeHosts = routeTable.Routes
                .Where(r => r.Host != ":-1" && !(r.CurrentTasks?.Any() ?? false))
                .ToList();

            if (!freeHosts.Any())
            {
                freeHosts = routeTable.Routes
                    .OrderByDescending(r => r.CurrentTasks.Count)
                    .ToList();
            }

            foreach (var route in freeHosts)
            {
                httpClient.BaseAddress = new Uri($"http://{route.Host}");

                var result = httpClient.GetAsync("/values/ping").Result;
                if (result.IsSuccessStatusCode)
                {
                    if (route.CurrentTasks == null)
                    {
                        route.CurrentTasks = new List<GameTask>();
                    }
                    route.CurrentTasks.Add(new GameTask() { TaskId = request.Id, PartId = 0 });
                    route.LastConnection = DateTime.Now;

                    _routeTableStorage.Save(routeTable);

                    var content = new StringContent(
                        JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                    httpClient.PostAsync("/values/process", content);

                    return Ok(new { TaskId = request.Id, route.Host });
                }
                routeTable.Routes.Remove(route);
                _routeTableStorage.Save(routeTable);
            }
            return BadRequest("There are not free hosts.");
        }

        [HttpGet]
        [Route("get/{task}/{part}")]
        public IHttpActionResult Get(Guid task, int part)
        {
            var processingHost = _routeTableStorage
                .Load().Routes
                .FirstOrDefault(r => r.CurrentTasks?.Any(x => x.TaskId == task) ?? false);

            if (processingHost != null)
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri($"http://{processingHost.Host}")
                };
                return Ok(httpClient.GetAsync($"/values/getpart/{task}/{part}").Result
                    .Content.ReadAsStringAsync().Result);
            }
            return BadRequest("Host, processing your task, was not be found.");
        }

        [HttpGet]
        [Route("remove/{taskId}")]
        public IHttpActionResult Remove(Guid taskId)
        {
            var routeTable = _routeTableStorage.Load();
            var route = routeTable.Routes.FirstOrDefault(r => r.CurrentTasks.Any(x=>x.TaskId ==taskId));
            var forRemoving = route?.CurrentTasks.FirstOrDefault(x => x.TaskId == taskId);
            if (!(route?.CurrentTasks.Remove(forRemoving) ?? true))
            {
                return BadRequest("Error occured while deleting of task");
            }
            _routeTableStorage.Save(routeTable);

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri($"http://{route.Host}")
            };
            var result = httpClient.GetAsync($"/values/remove/{taskId}").Result;

            if (result.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest("Host, processing your task, was not be found.");
        }

        [HttpGet]
        [Route("hosts")]
        public IHttpActionResult GetHosts()
        {
            return Ok(new Hosts(_routeTableStorage.Load().Routes));
        }
    }
}
