using System.Text.RegularExpressions;

namespace Core.Utilities
{
    public static class Extensions
    {
        public static string GetNiceName(this string text) =>
            Regex.Replace(text, "([a-z0-9])([A-Z0-9])", "$1 $2");
    }
}