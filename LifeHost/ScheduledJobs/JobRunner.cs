using Quartz;
using Quartz.Impl;

namespace LifeHost.ScheduledJobs
{
    public class JobRunner
    {
        public void Run()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            var sched = schedFact.GetScheduler();
            sched.Start();

            var job = JobBuilder.Create<CleanStorageJob>()
                .WithIdentity("CleanStorageJob", "CleanStorage")
                .Build();
            
            var trigger = TriggerBuilder.Create()
                .WithIdentity("CleanStorageTrigger", "CleanStorage")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(1)
                    .RepeatForever())
                .Build();

            sched.ScheduleJob(job, trigger);
        }
    }
}