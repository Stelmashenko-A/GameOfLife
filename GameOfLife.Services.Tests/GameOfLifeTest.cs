using System;
using LifeHost.Controllers;
using LifeHost.Storage;
using NUnit.Framework;

namespace GameOfLife.Services.Tests
{
    public class GameOfLifeTest
    {
        [Test]
        public void TestGame()
        {
            LifeHost.Controllers.GameOfLife gol = new LifeHost.Controllers.GameOfLife();
            gol.StateCalculator = new StateCalculator();
            gol.Converter = new Converter();
            gol.GameStorage=new GameStorage();
            gol.Process(new RequestForProcessing {Field = "0000000100000100111000000",Id = Guid.NewGuid(),Pats = 10,Steps = 100});
        }

    }
}