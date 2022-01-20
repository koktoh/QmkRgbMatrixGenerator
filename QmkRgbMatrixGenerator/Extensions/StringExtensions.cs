using System;
using System.Collections.Generic;

namespace QmkRgbMatrixGenerator.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        public static bool HasMeaningfulValue(this string source)
        {
            return !string.IsNullOrWhiteSpace(source);
        }

        public static string Join(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        public static string JoinComma(this IEnumerable<string> source, bool trailWhiteSpace = false)
        {
            var separator = trailWhiteSpace ? ", " : ",";

            return source.Join(separator);
        }

        public static string JoinNewLine(this IEnumerable<string> source)
        {
            return source.Join(Environment.NewLine);
        }

        public static IEnumerable<string> SplitComma(this string source, StringSplitOptions options = StringSplitOptions.None)
        {
            return source.Split(",", options);
        }

        public static IEnumerable<string> SplitNewLine(this string source, StringSplitOptions options = StringSplitOptions.None)
        {
            return source.Split(Environment.NewLine, options);
        }

        public static string Trim(this string source, string trimStr)
        {
            return source.Trim(trimStr.ToCharArray());
        }

        public static string TrimStart(this string source, string trimStr)
        {
            return source.TrimStart(trimStr.ToCharArray());
        }

        public static string TrimEnd(this string source, string trimStr)
        {
            return source.TrimEnd(trimStr.ToCharArray());
        }

        public static string Escape(this string source)
        {
            return source?.Replace(@"\", @"\\").Replace(@"""", @"\""");
        }
    }
}
