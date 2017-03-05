namespace GameOfLife.Services.Tests
{
    public class Field
    {
        public int Size;
        protected bool[,] Matrix;
        public int Current = 0;
        public Cell GetNext()
        {
            var row = Current/Size;
            var column = Current%Size;
            var aliveAround = 0;
            if (Matrix[(row - 1 + Size)%Size, column] == true)
            {
                aliveAround++;
            }
            if (Matrix[(row + 1) % Size, column] == true)
            {
                aliveAround++;
            }

            if (Matrix[row, (column - 1 + Size) % Size] == true)
            {
                aliveAround++;
            }
            if (Matrix[row, (column + 1) % Size] == true)
            {
                aliveAround++;
            }

            if (Matrix[(row - 1 + Size) % Size, (column - 1 + Size) % Size] == true)
            {
                aliveAround++;
            }
            if (Matrix[(row - 1) % Size, (column + 1) % Size] == true)
            {
                aliveAround++;
            }

            if (Matrix[(row + 1) % Size, (column - 1 + Size) % Size] == true)
            {
                aliveAround++;
            }
            if (Matrix[(row + 1), (column + 1) % Size] == true)
            {
                aliveAround++;
            }
            Current++;
            return new Cell {AliveArondCount = aliveAround, IsAlive = Matrix[row, column]};
        }

        public void SetNext(bool isAlive)
        {
            int row = Current / Size;
            int column = Current % Size;
            Matrix[row, column] = isAlive;
            Current++;
        }

        public Field(bool[,] initArr)
        {
            Size = initArr.Length;
            Matrix = initArr;
        }
    }

    public class Cell
    {
        public bool IsAlive { get; set; }
        public int AliveArondCount { get; set; }
    }
}
