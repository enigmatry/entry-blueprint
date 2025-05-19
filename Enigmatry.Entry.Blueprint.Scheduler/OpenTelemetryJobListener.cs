using System.Diagnostics;
using Quartz;

namespace Enigmatry.Entry.Blueprint.Scheduler;

internal sealed class OpenTelemetryJobListener() : IJobListener
{
    public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default) => Task.CompletedTask;

    public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default) => Task.CompletedTask;

    public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
    {
        TrackJobExecution(context, jobException);
        return Task.CompletedTask;
    }

    private void TrackJobExecution(IJobExecutionContext context, JobExecutionException? jobException = null)
    {
        var jobName = context.JobDetail.Key.Name;
        using var activitySource = new ActivitySource(context.Scheduler.SchedulerName);
        using var activity = activitySource.StartActivity(jobName, ActivityKind.Server);
        if (activity == null)
        {
            return;
        }

        activity.SetStartTime(context.FireTimeUtc.UtcDateTime);
        activity.DisplayName = jobName;
        activity.SetEndTime(DateTime.UtcNow);

        if (jobException != null)
        {
            activity.SetStatus(ActivityStatusCode.Error);
            activity.AddException(jobException);
        }
        else
        {
            activity.SetStatus(ActivityStatusCode.Ok);
        }
    }


    public string Name => nameof(OpenTelemetryJobListener);
}
