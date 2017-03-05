using System;
using System.Text;

namespace GameOfLife.Services
{
    public interface IConverter
    {
        string Convert(bool[,] src);
        bool[,] Convert(string src);
    }

    public class Converter : IConverter
    {
        public string Convert(bool[,] src)
        {
            var sb = new StringBuilder();
            foreach (var b in src)
            {
                sb.Append(b ? "1" : "0");
            }
            return sb.ToString();
        }

        public bool[,] Convert(string src)
        {
            var size = (int)Math.Sqrt(src.Length);
            var arr = new bool[size, size];
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    arr[i, j] = src[i*size + j] == '1';
                }
            }
            return arr;
        }
    }
}
