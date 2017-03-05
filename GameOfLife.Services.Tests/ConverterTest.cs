using NUnit.Framework;


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
}