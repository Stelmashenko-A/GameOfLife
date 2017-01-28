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
            if (neighborsNumber < 2 || neighborsNumber > 3)
            {
                return 0;
            }

            return neighborsNumber == 3 ? 1 : currentState;
        }
    }
}
