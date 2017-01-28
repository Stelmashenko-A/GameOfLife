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

        [Test]
        public void CellStayAlive()
        {
            IStateCalculator stateCalculator = new StateCalculator();
            const int currentState = IsAlive;
            int[] neighborsNumbers = { 2, 3 };
            int[] expectedResult = { IsAlive, IsAlive };
            int[] actualResult = new int[2];

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
            const int currentState = IsAlive;
            int[] neighborsNumbers = { 0, 1, 4, 5, 6, 7, 8 };
            int[] expectedResult = { IsDead, IsDead, IsDead, IsDead, IsDead, IsDead, IsDead };
            int[] actualResult = new int[7];

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
            const int currentState = IsDead;
            int[] neighborsNumbers = { 0, 1, 2, 4, 5, 6, 7, 8 };
            int[] expectedResult = { IsDead, IsDead, IsDead, IsDead, IsDead, IsDead, IsDead, IsDead };
            int[] actualResult = new int[8];

            for (var i = 0; i < neighborsNumbers.Length; i++)
            {
                actualResult[i] = stateCalculator.NextState(currentState, neighborsNumbers[i]);
            }

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
