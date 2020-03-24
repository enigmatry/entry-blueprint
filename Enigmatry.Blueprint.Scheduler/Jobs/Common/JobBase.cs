using System;
using System.Linq;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Scheduler.Attributes;
using Enigmatry.Blueprint.Scheduler.Configurations;
using Enigmatry.Blueprint.Scheduler.Extensions;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Enigmatry.Blueprint.Scheduler.Jobs.Common
{
    [DisallowConcurrentExecution]
    public abstract class JobBase : IJobBase
    {
        private readonly JobSettings _jobSettings;
        protected readonly ILogger _logger;

        protected JobBase(AppSettings appSettings, ILogger logger)
        {
            _jobSettings = appSettings.Jobs.FirstOrDefault(x => x.Name == this.GetAttribute<JobNameAttribute>()?.GetName()) 
                           ?? throw new ArgumentNullException(GetType().ToString(), "Job configuration could not be found!");
            _logger = logger;
        }

        public string GetName() => _jobSettings.Name;

        public string GetInterval() => _jobSettings.SchedulerInterval;

        public bool IsEnabled() => _jobSettings.Enabled;

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await Run();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        protected abstract Task Run();
    }
}
