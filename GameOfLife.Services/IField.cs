namespace GameOfLife.Services
{
    public interface IField
    {
        bool[,] GetNextState();
    }
}
