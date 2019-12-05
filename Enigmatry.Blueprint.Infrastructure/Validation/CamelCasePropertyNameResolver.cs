using System;
using System.Linq.Expressions;
using System.Reflection;
using Enigmatry.Blueprint.Core.Helpers;
using FluentValidation.Internal;

namespace Enigmatry.Blueprint.Infrastructure.Validation
{
    // Enables FluentValidation error messages to contain camel cased property names instead of pascal cased.
    // E.g. Instead of 'UserName' we get 'userName'
    public static class CamelCasePropertyNameResolver
    {
        public static string ResolvePropertyName(Type type, MemberInfo memberInfo, LambdaExpression expression)
        {
            string propertyName = DefaultPropertyNameResolver(memberInfo, expression);

            return propertyName.ToCamelCase();
        }

        private static string DefaultPropertyNameResolver(MemberInfo memberInfo, LambdaExpression expression)
        {
            if (expression != null)
            {
                PropertyChain chain = PropertyChain.FromExpression(expression);
                if (chain.Count > 0) return chain.ToString();
            }

            return memberInfo != null ? memberInfo.Name : String.Empty;
        }
    }
}
