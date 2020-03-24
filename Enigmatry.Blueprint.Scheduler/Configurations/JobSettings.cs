namespace Enigmatry.Blueprint.Scheduler.Configurations
{
    public class JobSettings
    {
        public string Name { get; set; }
        public string SchedulerInterval { get; set; }
        public bool Enabled { get; set; } = true;
    }
}
