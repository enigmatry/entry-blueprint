using System;
using System.Linq;

namespace Enigmatry.Blueprint.Scheduler.Extensions
{
    public static class ObjectExtensions
    {
        public static T GetAttribute<T>(this object o) where T : Attribute
        {
            return GetAttribute<T>(o.GetType());
        }

        public static T GetAttribute<T>(this Type o) where T : Attribute
        {
            return Attribute.GetCustomAttributes(o, typeof(T)).FirstOrDefault() as T;
        }
    }
}
