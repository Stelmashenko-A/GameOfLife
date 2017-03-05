using System;

namespace GameOfLife.Services
{
    public class Field : IField
    {
        protected bool[,] State { get; set; }
        protected int Size { get; }
        public IStateCalculator StateCalculator { get; set; }

        public Field(bool[,] field, IStateCalculator stateCalculator)
        {
            State = field;
            StateCalculator = stateCalculator;
            Size = (int)Math.Sqrt(State.Length);
        }

        public bool[,] GetNextState()
        {
            var newState = new bool[Size, Size];
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    var aliveNeighbors = AliveNeighbors(i, j);
                    newState[i, j] = StateCalculator.NextState(State[i, j], aliveNeighbors);
                }
            }
            State = newState;
            return Copy(State);
        }

        protected bool[,] Copy(bool[,] src)
        {
            var size = (int)Math.Sqrt(src.Length);
            var copy = new bool[size, size];
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    copy[i, j] = src[i, j];
                }
            }
            return src;
        }

        protected int AliveNeighbors(int i, int j)
        {
            var row = i;
            var column = j;
            var aliveAround = 0;
            if (State[(row - 1 + Size) % Size, column])
            {
                aliveAround++;
            }
            if (State[(row + 1) % Size, column])
            {
                aliveAround++;
            }

            if (State[row, (column - 1 + Size) % Size])
            {
                aliveAround++;
            }
            if (State[row, (column + 1) % Size])
            {
                aliveAround++;
            }

            if (State[(row - 1 + Size) % Size, (column - 1 + Size) % Size])
            {
                aliveAround++;
            }
            if (State[(row - 1 + Size) % Size, (column + 1) % Size])
            {
                aliveAround++;
            }

            if (State[(row + 1) % Size, (column - 1 + Size) % Size])
            {
                aliveAround++;
            }
            if (State[(row + 1) % Size, (column + 1) % Size])
            {
                aliveAround++;
            }
            return aliveAround;
        }

    }
}