using System;

namespace Enigmatry.Blueprint.Scheduler.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JobNameAttribute : Attribute
    {
        private readonly string _name;

        public JobNameAttribute(string name)
        {
            _name = name;
        }

        public string GetName() => _name;
    }
}
