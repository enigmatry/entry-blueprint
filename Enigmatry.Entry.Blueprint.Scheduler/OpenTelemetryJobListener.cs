using System.Diagnostics;
using System.Reflection;
using Quartz;

namespace Enigmatry.Entry.Blueprint.Scheduler;

internal sealed class OpenTelemetryJobListener(ILogger<OpenTelemetryJobListener> logger) : IJobListener, IDisposable
{
    private ActivitySource _activitySource = new(Assembly.GetExecutingAssembly().GetName().Name!);
    private bool _disposed;

    public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default) => Task.CompletedTask;

    public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default) => Task.CompletedTask;

    public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
    {
        TrackJobExecution(context, jobException);
        return Task.CompletedTask;
    }

    private void TrackJobExecution(IJobExecutionContext context, JobExecutionException? jobException = null)
    {
        logger.LogInformation("Job {JobName} executed at {FireTimeUtc}", context.JobDetail.Key.Name, context.FireTimeUtc.UtcDateTime);
        var jobName = context.JobDetail.Key.Name;
        using var activity = _activitySource.StartActivity(jobName, ActivityKind.Server);
        if (activity == null)
        {
            return;
        }

        activity.SetStartTime(context.FireTimeUtc.UtcDateTime);

        if (jobException != null)
        {
            activity.SetStatus(ActivityStatusCode.Error)
                .AddException(jobException);
        }
        else
        {
            _ = activity.SetStatus(ActivityStatusCode.Ok);
        }
    }

    public string Name => nameof(OpenTelemetryJobListener);
    
    public void Dispose()
    {
        if (!_disposed)
        {
            _activitySource.Dispose();
            _disposed = true;
        }
    }
}
