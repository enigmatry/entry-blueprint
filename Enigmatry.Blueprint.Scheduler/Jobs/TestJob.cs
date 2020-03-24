using System.Threading.Tasks;
using Enigmatry.Blueprint.Scheduler.Attributes;
using Enigmatry.Blueprint.Scheduler.Configurations;
using Enigmatry.Blueprint.Scheduler.Jobs.Common;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Scheduler.Jobs
{
    [JobName("blueprint.test.job")]
    public class TestJob : JobBase
    {
        public TestJob(AppSettings appSettings, ILogger<TestJob> logger) : base(appSettings, logger)
        {
        }

        protected override Task Run()
        {
            _logger.LogInformation("TEST JOB EXECUTING...");
            _logger.LogInformation("TEST JOB DONE!");
            return Task.CompletedTask;
        }
    }
}
