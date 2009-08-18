using System.Text.RegularExpressions;

namespace SpecExpress.Util
{
    public static class StringExtensions
    {
        public static string SplitPascalCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return Regex.Replace(input, "([A-Z])", " $1").Trim();
        }
    }
}