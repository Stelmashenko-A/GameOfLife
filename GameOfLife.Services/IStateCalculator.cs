namespace GameOfLife.Services
{
    public interface IStateCalculator
    {
        int NextState(int currentState, int neighborsNumber);
    }

    public class StateCalculator : IStateCalculator
    {
        public int NextState(int currentState, int neighborsNumber)
        {
            throw new System.NotImplementedException();
        }
    }
}
