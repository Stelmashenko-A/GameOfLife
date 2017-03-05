namespace GameOfLife.Services
{
    public class StateCalculator : IStateCalculator
    {
        public bool NextState(bool currentState, int neighborsNumber)
        {
            if (neighborsNumber < 2 || neighborsNumber > 3)
            {
                return false;
            }

            return neighborsNumber == 3 || currentState;
        }
    }
}