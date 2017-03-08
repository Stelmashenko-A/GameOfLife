using LifeHost.Business.GameStorage;
using Quartz;

namespace LifeHost.ScheduledJobs
{
    public class CleanStorageJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (IGameCleaner gameCleaner = new GameCleaner())
            {
                gameCleaner.Clean();
            }
        }
    }
}