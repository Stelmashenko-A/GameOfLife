using System;
using System.Collections.Generic;
using GameOfLife.Services;
using LifeHost.Business.GameStorage;
using LifeHost.Controllers;
using LifeHost.Models;
using Ninject;

namespace LifeHost.Business.GameOfLife
{
    public class GameOfLife : IGameOfLife
    {
        [Inject]
        public IStateCalculator StateCalculator { get; set; }

        [Inject]
        public IConverter Converter { get; set; }

        [Inject]
        public IGameStorage GameStorage { get; set; }

        protected IField Field { get; set; }

        public void Process(RequestForProcessing request)
        {
            var data = Converter.Convert(request.Field);
            Field = new Field(data, StateCalculator);
            var partSize = request.Steps / request.Parts;
            var partCounter = 0;

            var list = new List<string>();
            for (int i = 0; i < request.Steps; i++)
            {
                var newStep = Field.GetNextState();
                list.Add(Converter.Convert(newStep));
                if ((i+1) % partSize == 0)
                {
                    var part = new Part { Id = Guid.NewGuid(), PartNumber = partCounter, Steps = list, TaskId = request.Id };
                    GameStorage.Save(part);
                    list = new List<string>();
                    partCounter++;
                }
            }
            if (list.Count != 0)
            {
                var part = new Part { Id = Guid.NewGuid(), PartNumber = partCounter, Steps = list, TaskId = request.Id };
                GameStorage.Save(part);
            }
        }
    }
}