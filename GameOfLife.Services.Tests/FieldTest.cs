using NUnit.Framework;

namespace GameOfLife.Services.Tests
{
    public class FieldTest
    {
        public const bool IsDead = false;
        public const bool IsAlive = true;

        [Test]
        public void CopyTest()
        {
            bool[,] array = { { IsDead, IsDead, IsDead }, { IsDead, IsAlive, IsDead }, { IsDead, IsDead, IsDead } };
            var field = new TestProtectedField(array, new StateCalculator());
            bool[,] expectedResult = { { IsDead, IsDead, IsDead }, { IsDead, IsAlive, IsDead }, { IsDead, IsDead, IsDead } };
            bool[,] actualResult = { { IsDead, IsDead, IsDead }, { IsDead, IsAlive, IsDead }, { IsDead, IsDead, IsDead } };
            field.CopyTest(array);
            Assert.AreNotSame(expectedResult, actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void NextStep()
        {
            bool[,] array = { { IsDead, IsDead, IsDead }, { IsDead, IsAlive, IsDead }, { IsDead, IsDead, IsDead } };
            var field = new Services.Field(array, new StateCalculator());
            bool[,] expectedResult = { { IsDead, IsDead, IsDead }, { IsDead, IsDead, IsDead }, { IsDead, IsDead, IsDead } };
            var actualResult = field.GetNextState();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void AliveNeighborsTest()
        {
            bool[,] array = { { IsDead, IsDead, IsDead }, { IsDead, IsAlive, IsDead }, { IsDead, IsDead, IsDead } };
            var field = new TestProtectedField(array, new StateCalculator());
            const int expectedResult = 0;
            var actualResult = field.AliveNeighborsTest(1,1);

            Assert.AreEqual(expectedResult, actualResult);
        }

        public class TestProtectedField : Services.Field
        {
            public TestProtectedField(bool[,] field, IStateCalculator stateCalculator) : base(field, stateCalculator)
            {
            }

            public int AliveNeighborsTest(int i, int j)
            {
                return AliveNeighbors(i, j);
            }

            public bool[,] CopyTest(bool[,] src)
            {
                return Copy(src);
            }
        }
    }

}