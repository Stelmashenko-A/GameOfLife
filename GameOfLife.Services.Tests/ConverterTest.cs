using System;
using LifeHost.Controllers;
using LifeHost.Storage;
using NUnit.Framework;
using GameOfLife = LifeHost.Controllers.GameOfLife;

namespace GameOfLife.Services.Tests
{
    public class ConverterTest
    {
        [Test]
        public void ConvertToArray()
        {
            var converter = new Converter();
            var str = "1100";
            bool[,] expectedResult = { { true, true }, { false, false } };
            var actualResult = converter.Convert(str);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ConvertToString()
        {
            var converter = new Converter();
            bool[,] arr = { { true, true }, { false, false } };
            var expectedResult = "1100";
            var actualResult = converter.Convert(arr);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }

    public class GameOfLifeTest
    {
        [Test]
        public void ConvertToArray()
        {
            LifeHost.Controllers.GameOfLife gol = new LifeHost.Controllers.GameOfLife();
            gol.StateCalculator = new StateCalculator();
            gol.Converter = new Converter();
            gol.GameStorage=new GameStorage();

            gol.Process(new RequestForProcessing() {Field = "0000000100000100111000000",Id = Guid.NewGuid(),Pats = 10,Steps = 100});
        }

    }
}