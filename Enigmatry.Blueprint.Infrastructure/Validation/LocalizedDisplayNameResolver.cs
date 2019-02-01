using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using static System.String;

namespace Enigmatry.Blueprint.Infrastructure.Validation
{
    public static class LocalizedDisplayNameResolver
    {
        private static ResourceManager ResourceManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceManager">ResourceManager from which to read localized display names</param>
        /// <returns></returns>
        public static Func<Type, MemberInfo, LambdaExpression, string> ResolveDisplayName(
            ResourceManager resourceManager)
        {
            ResourceManager = resourceManager;
            return ResolveDisplayName;
        }

        private static string ResolveDisplayName(Type type, MemberInfo memberInfo, LambdaExpression arg3)
        {
            return memberInfo == null ? null : DisplayNameCache.GetCachedDisplayName(memberInfo, ResourceManager);
        }
    }

    // Taken from FluentValidation.DisplayNameCache - except it supports localization
    internal static class DisplayNameCache
    {
        private static readonly ConcurrentDictionary<MemberInfo, Func<string>> Cache =
            new ConcurrentDictionary<MemberInfo, Func<string>>();

        public static string GetCachedDisplayName(MemberInfo member, ResourceManager localizationResourceManager)
        {
            Func<string> result = Cache.GetOrAdd(member, m => GetDisplayName(m, localizationResourceManager));
            return result?.Invoke();
        }

        private static Func<string> GetDisplayName(MemberInfo member, ResourceManager localizationResourceManager)
        {
            if (member == null) return null;

            var displayAttribute = member.GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute != null)
            {
                string result = GetLocalizedName(displayAttribute.GetName());
                return () => result;
            }

            // Couldn't find a name from a DisplayAttribute. Try DisplayNameAttribute instead.
            var displayNameAttribute = member.GetCustomAttribute<DisplayNameAttribute>();

            if (displayNameAttribute != null)
            {
                string result = GetLocalizedName(displayNameAttribute.DisplayName);
                return () => result;
            }

            return null;

            string GetLocalizedName(string displayName)
            {
                string result = localizationResourceManager.GetString(displayName);
                return !IsNullOrEmpty(result) ? result : displayName;
            }
        }
    }
}
