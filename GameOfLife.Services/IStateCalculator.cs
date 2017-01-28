namespace GameOfLife.Services
{
    public interface IStateCalculator
    {
        int NextState(int currentState, int neighborsNumber);
    }
}
