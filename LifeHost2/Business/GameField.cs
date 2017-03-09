namespace LifeHost.Business
{
    public class GameField
    {
        private readonly int _size;
        private readonly byte[,] _field;
        public GameField(byte[] array, int size)
        {
            _size = size;
            _field = new byte[_size, _size];
            var counter = 0;

            for (var i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; j++)
                {
                    _field[i, j] = array[counter];
                    counter++;
                }
            }
        }

        public int GetLiveCount()
        {
            var count = 0;

            for (var i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; j++)
                {
                    if (_field[i, j] == 1)
                        count++;
                }
            }
            return count;
        }

        public int[,] GetPointNeighbours(int[,] nb, int x, int y)
        {
            var k = 0;

            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
                    if (i == x && j == y)
                        continue;
                    nb[k, 0] = i;
                    nb[k, 1] = j;
                    k++;
                }
            }

            return nb;
        }

        public int CountLiveNeighbours(int x, int y)
        {
            var count = 0;
            var nb = new int[8, 2];

            GetPointNeighbours(nb, x, y);

            for (var i = 0; i < 8; i++)
            {
                var _x = nb[i, 0];
                var _y = nb[i, 1];

                if (_x < 0 || _y < 0)
                    continue;

                if (_x >= _size || _y >= _size)
                    continue;

                if (_field[_x, _y] == 1)
                    count++;
            }
            return count;
        }

        public void NextGeneration()
        {
            int live_nb;
            byte point;
            var preField = new byte[_size, _size];

            for (var i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; j++)
                {
                    point = _field[i, j];
                    live_nb = CountLiveNeighbours(i, j);

                    if (point == 0)
                    {
                        if (live_nb == 3)
                            preField[i, j] = 1;
                    }
                    else if (live_nb < 2 || live_nb > 3)
                        preField[i, j] = 0;
                }
            }
            CopyField(preField,_field);
        }

        public void CopyField(byte[,] src, byte[,] dest)
        {
            for (var i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; j++)
                {
                    dest[i, j] = src[i, j];
                }
            }
        }

        public byte[,] GetState()
        {
            var preField = new byte[_size, _size];
            CopyField(_field, preField);
            return preField;
        }
    }
}