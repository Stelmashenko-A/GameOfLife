namespace GameOfLife.Services
{
    public interface IStateCalculator
    {
        bool NextState(bool currentState, int neighborsNumber);
    }
}
