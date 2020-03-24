using Quartz;

namespace Enigmatry.Blueprint.Scheduler.Jobs.Common
{
    public interface IJobBase : IJob
    {
        string GetName();
        string GetInterval();
        bool IsEnabled();
    }
}
