using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using LifeHost.Infrastructure;
using LifeHost.Storage;
using Ninject;

namespace LifeHost.Controllers
{
    public class ValuesController : ApiController
    {
        [Inject]
        public IGameOfLife GameOfLife { get; set; }

        [Inject]
        public IGameStorage GameStorage { get; set; }

        public OkResult Process(RequestForProcessing request)
        {
            //надо только запустить метод и идти дальше
            GameOfLife.Process(request);
            return Ok();
        }

        public Part Get(Guid taskId, int part)
        {
            return GameStorage.Get(taskId, part);
        }

        public OkResult RemoveAllParst(Guid taskId)
        {
            GameStorage.RemoveAllParts(taskId);
            return Ok();
        }

        public OkResult Ping()
        {
            return Ok();
        }

    }

    public class RequestForProcessing
    {
        public Guid Id { get; set; }
        public string Field { get; set; }
        public int Steps { get; set; }
        public int Pats { get; set; }
    }
}
