using System.Collections.Generic;

namespace Enigmatry.Blueprint.Scheduler.Configurations
{
    public class AppSettings
    {
        public string ServiceName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<JobSettings> Jobs { get; set; } = new List<JobSettings>();
    }
}
