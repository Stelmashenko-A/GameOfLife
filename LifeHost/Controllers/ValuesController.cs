using System;
using System.Web.Http;
using System.Web.Http.Results;
using LifeHost.Business.GameOfLife;
using LifeHost.Business.GameStorage;
using Ninject;
using LifeHost.Models;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace LifeHost.Controllers
{
    [RoutePrefix("values")]
    public class ValuesController : ApiController
    {
        [Inject]
        public IGameOfLife GameOfLife { get; set; }

        [Inject]
        public IGameStorage GameStorage { get; set; }

        [HttpPost]
        [Route("process")]
        public OkResult Process(RequestForProcessing request)
        {
            Task.Factory.StartNew(() => GameOfLife.Process(request));
            return Ok();
        }

        [HttpGet]
        [Route("getpart/{taskId}/{part}")]
        public string GetTaskPart(Guid taskId, int part)
        {
            var result = GameStorage.Get(taskId, part);
            return JsonConvert.SerializeObject(result?.Steps);
        }

        [HttpGet]
        [Route("remove/{taskId}")]
        public OkResult RemoveAllParst(Guid taskId)
        {
            GameStorage.RemoveAllParts(taskId);
            return Ok();
        }

        [HttpGet]
        [Route("ping")]
        public OkResult Ping()
        {
            return Ok();
        }

    }
}
