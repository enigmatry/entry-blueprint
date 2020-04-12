using System;
using System.Linq;

namespace Enigmatry.Blueprint.Scheduler.Extensions
{
    public static class TypeExtensions
    {
        public static bool ImplementsInterface(this Type concreteType, Type interfaceType)
        {
            return
                concreteType.GetInterfaces().Any(
                    t =>
                        (interfaceType.IsGenericTypeDefinition && t.IsGenericType
                            ? t.GetGenericTypeDefinition()
                            : t) == interfaceType);
        }

        public static bool HasAttributes<T>(this Type concreteType) where T : Attribute
        {
            return concreteType.GetAttribute<T>() != null;
        }
    }
}
