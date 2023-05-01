using System;
using System.Text.RegularExpressions;
using Core.Enums;

namespace Core.Utilities
{
    public static class Extensions
    {
        public static string GetNiceName(this string text) =>
            Regex.Replace(text, "([a-z0-9])([A-Z0-9])", "$1 $2");

        public static void GetWordDirection(this string text, out Direction direction)
        {
            bool isDownDirection = string.Equals(text, "down", StringComparison.OrdinalIgnoreCase);

            if (isDownDirection)
            {
                direction = Direction.Down;
                return;
            }

            bool isAcrossDirection = string.Equals(text, "across", StringComparison.OrdinalIgnoreCase);
            direction = isAcrossDirection ? Direction.Across : Direction.Wrong;
        }
    }
}