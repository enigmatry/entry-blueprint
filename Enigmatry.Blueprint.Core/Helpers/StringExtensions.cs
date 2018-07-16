namespace Enigmatry.Blueprint.Core.Helpers
{
    public static class StringExtensions
    {
        public static bool HasContent(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static string ToNullIfEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }

        public static string FirstLetterToLowerCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }
    }
}