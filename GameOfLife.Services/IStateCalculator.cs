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
            if (currentState == 0 && neighborsNumber == 3)
            {
                return 1;
            }

            if (currentState == 1 && (neighborsNumber == 2 || neighborsNumber == 3))
            {
                return 1;
            }

            if (currentState == 1 && (neighborsNumber < 2 || neighborsNumber > 3))
            {
                return 0;
            }

            if (currentState == 0 && neighborsNumber != 3)
            {
                return 0;
            }

            throw new System.NotImplementedException();
        }
    }
}
