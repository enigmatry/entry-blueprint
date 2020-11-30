using System;
using System.Globalization;

namespace Enigmatry.Blueprint.Core.Helpers
{
    public static class StringExtensions
    {
        public static bool HasContent(this string value) => !String.IsNullOrEmpty(value);

        public static string? ToNullIfEmpty(this string value) => String.IsNullOrEmpty(value) ? null : value;

        public static string ToCamelCase(this string s)
        {
            if (String.IsNullOrEmpty(s) || !Char.IsUpper(s[0]))
            {
                return s;
            }

            char[] chars = s.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !Char.IsUpper(chars[i]))
                {
                    break;
                }

                bool hasNext = i + 1 < chars.Length;
                if (i > 0 && hasNext && !Char.IsUpper(chars[i + 1]))
                {
                    break;
                }

                chars[i] = Char.ToLower(chars[i], CultureInfo.InvariantCulture);
            }

            return new string(chars);
        }
    }
}
