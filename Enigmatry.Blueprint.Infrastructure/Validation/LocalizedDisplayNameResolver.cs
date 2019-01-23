using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;

namespace Enigmatry.Blueprint.Infrastructure.Validation
{
    public static class LocalizedDisplayNameResolver
    {
        private static ResourceManager LocalizationResourceManager {get;set;}

        private static string ResolveDisplayName(Type type, MemberInfo memberInfo, LambdaExpression arg3)
        {
            //this will get in this case, "DocumentNumber", the property name. 
            //If we don't find anything in metadata / resource, that what will be displayed in the error message.
            string displayName = memberInfo.Name;
            var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
            if (displayAttribute != null)
            {
                displayName = displayAttribute.GetName();
                return LocalizationResourceManager.GetString(displayName);
            }

            return displayName;
        }

        public static Func<Type, MemberInfo, LambdaExpression, string> ResolveDisplayName(ResourceManager resourceManager)
        {
            LocalizationResourceManager = resourceManager;
            return ResolveDisplayName;
        }
    }
}