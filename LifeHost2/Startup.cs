using LifeHost.Business.GameStorage;
using LifeHost.ScheduledJobs;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LifeHost.Startup))]

namespace LifeHost
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            using (var sb = new IndexBuilder())
            {
                sb.Build();
            }

            var jr = new JobRunner();
            jr.Run();
        }
    }
}
