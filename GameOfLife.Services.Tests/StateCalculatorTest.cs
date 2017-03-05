using NUnit.Framework;

namespace GameOfLife.Services.Tests
{
    public class StateCalculatorTest
    {
        public const bool IsDead = false;
        public const bool IsAlive = true;

        [Test]
        public void CellIsBorn()
        {
            IStateCalculator stateCalculator = new StateCalculator();
            const bool currentState = false;
            const int neighborsNumber = 3;
            var newState = stateCalculator.NextState(currentState, neighborsNumber);
            Assert.AreEqual(IsAlive, newState);
        }

        [Test]
        public void CellStayAlive()
        {
            IStateCalculator stateCalculator = new StateCalculator();
            const bool currentState = IsAlive;
            int[] neighborsNumbers = { 2, 3 };
            bool[] expectedResult = { IsAlive, IsAlive };
            bool[] actualResult = new bool[2];

            for (var i = 0; i < neighborsNumbers.Length; i++)
            {
                actualResult[i] = stateCalculator.NextState(currentState, neighborsNumbers[i]);
            }

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CellDie()
        {
            IStateCalculator stateCalculator = new StateCalculator();
            const bool currentState = IsAlive;
            int[] neighborsNumbers = { 0, 1, 4, 5, 6, 7, 8 };
            bool[] expectedResult = { IsDead, IsDead, IsDead, IsDead, IsDead, IsDead, IsDead };
            bool[] actualResult = new bool[7];

            for (var i = 0; i < neighborsNumbers.Length; i++)
            {
                actualResult[i] = stateCalculator.NextState(currentState, neighborsNumbers[i]);
            }

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CellNotBorn()
        {
            IStateCalculator stateCalculator = new StateCalculator();
            const bool currentState = IsDead;
            int[] neighborsNumbers = { 0, 1, 2, 4, 5, 6, 7, 8 };
            bool[] expectedResult = { IsDead, IsDead, IsDead, IsDead, IsDead, IsDead, IsDead, IsDead };
            bool[] actualResult = new bool[8];

            for (var i = 0; i < neighborsNumbers.Length; i++)
            {
                actualResult[i] = stateCalculator.NextState(currentState, neighborsNumbers[i]);
            }

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
