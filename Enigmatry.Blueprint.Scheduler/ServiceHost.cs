using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Scheduler.Extensions;
using Enigmatry.Blueprint.Scheduler.Jobs.Common;
using Quartz;

namespace Enigmatry.Blueprint.Scheduler
{
    public class ServiceHost
    {
        private IScheduler Scheduler { get; }
        private readonly IEnumerable<IJobBase> _jobs;

        public ServiceHost(IScheduler scheduler, IEnumerable<IJobBase> jobs)
        {
            Scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
            _jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
        }

        public async Task WhenStarted()
        {
            await Scheduler.Start();
            
            foreach (var job in _jobs.ToList())
                await Scheduler.ScheduleCronJob(job);

            await Task.CompletedTask;
        }

        public async Task WhenPaused() =>
            await Scheduler.PauseAll();

        public async Task WhenContinued() =>
            await Scheduler.ResumeAll();

        public async Task WhenStopped() =>
            await Scheduler.Shutdown();
    }
}
