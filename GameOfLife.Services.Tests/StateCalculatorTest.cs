using NUnit.Framework;

namespace GameOfLife.Services.Tests
{
    public class StateCalculatorTest
    {
        public const int IsDead = 0;
        public const int IsAlive = 1;

        [Test]
        public void CellIsBorn()
        {
            IStateCalculator stateCalculator = new StateCalculator();
            const int currentState = 0;
            const int neighborsNumber = 3;
            var newState = stateCalculator.NextState(currentState, neighborsNumber);
            Assert.AreEqual(IsAlive, newState);
        }
    }
}
