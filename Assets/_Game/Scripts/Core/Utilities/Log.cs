﻿using System.Text;
using UnityEngine;

namespace Core.Utilities
{
    public static class Log
    {
        private const string StatColor = "<color=#FF6B37>";
        private const string ValuesColor = "<color=#60FF79>";
        private const string AttributeColor = "<color=#3BAAFF>";

        private const string RedColor = "<color=#FF3243>";
        private const string GreenColor = "<color=#60FF79>";
        private const string CyanColor = "<color=#3BAAFF>";
        private const string OrangeColor = "<color=#FF6B37>";
        private const string PurpleColor = "<color=#A775FF>";

        private const string RedFat = "<r><b>";
        private const string RedFatEndPrefix = "</b></>";
        private const string GreenFat = "<g><b>";
        private const string GreenFatEndPrefix = "</b></>";
        private const string CyanFat = "<c><b>";
        private const string CyanFatEndPrefix = "</b></>";
        private const string OrangeFat = "<o><b>";
        private const string OrangeFatEndPrefix = "</b></>";
        private const string PurpleFat = "<p><b>";
        private const string PurpleFatEndPrefix = "</b></>";

        private const string ColorEndPrefix = "</color>";

        public static string Print(string log)
        {
            HandleMessage(log, out string logResult);
            return logResult;
        }

        public static string Print(string tag, string log)
        {
            string message = $"[<st><b>{tag}</b></>] {log}";
            HandleMessage(message, out string logResult);
            return logResult;
        }

        private static void HandleMessage(string log, out string result)
        {
            StringBuilder text = new(log);

            text.Insert(0, "<color=white>");
            
            text.Replace("<rb>", RedFat);
            text.Replace("</rb>", RedFatEndPrefix);
            text.Replace("<gb>", GreenFat);
            text.Replace("</gb>", GreenFatEndPrefix);
            text.Replace("<cb>", CyanFat);
            text.Replace("</cb>", CyanFatEndPrefix);
            text.Replace("<ob>", OrangeFat);
            text.Replace("</ob>", OrangeFatEndPrefix);
            text.Replace("<pb>", PurpleFat);
            text.Replace("</pb>", PurpleFatEndPrefix);

            text.Replace("<r>", RedColor);
            text.Replace("<g>", GreenColor);
            text.Replace("<c>", CyanColor);
            text.Replace("<o>", OrangeColor);
            text.Replace("<p>", PurpleColor);

            text.Replace("<st>", StatColor);
            text.Replace("<val>", ValuesColor);
            text.Replace("<att>", AttributeColor);

            text.Replace("</>", ColorEndPrefix);
            text.Replace("</r>", ColorEndPrefix);
            text.Replace("</g>", ColorEndPrefix);
            text.Replace("</c>", ColorEndPrefix);
            text.Replace("</o>", ColorEndPrefix);
            text.Replace("</p>", ColorEndPrefix);

            int lastIndex = text.Length;
            text.Insert(lastIndex, "</color>");
            
            result = text.ToString();
        }
    }
}