using System;
using System.Linq;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Scheduler.Jobs.Common;
using JetBrains.Annotations;
using Quartz;

namespace Enigmatry.Blueprint.Scheduler.Extensions
{
    public static class SchedulerExtensions
    {
        [UsedImplicitly]
        public static async Task ScheduleCronJob(this IScheduler scheduler, IJobBase jobBase)
        {
            if (scheduler == null)
                throw new ArgumentNullException(nameof(scheduler));
            if (jobBase == null)
                throw new ArgumentNullException(nameof(jobBase));

            if (!jobBase.IsEnabled())
                return;

            var entityMethod = typeof(JobBuilder).GetMethods().First(m => m.Name == "Create" && m.IsGenericMethod);

            if (entityMethod == null)
                return;

            IJobDetail job = ((JobBuilder)entityMethod.MakeGenericMethod(jobBase.GetType()).Invoke(null, null))
                .WithIdentity(jobBase.GetName())
                .Build();

            ITrigger trigger = TriggerBuilder
                .Create()
                .WithIdentity(jobBase.GetName())
                .WithCronSchedule(jobBase.GetInterval())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
