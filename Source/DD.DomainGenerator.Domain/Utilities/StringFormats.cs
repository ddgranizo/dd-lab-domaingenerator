using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DD.DomainGenerator.Utilities
{
    public static class StringFormats
    {
        public static char[] Delimiters = new char[] { '-', '_', ' ', '.' };
        public static Uri ParseStringUri(string uri)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(uri, UriKind.Absolute, out uriResult)
                && uriResult.Scheme == Uri.UriSchemeHttp;
            return uriResult;
        }

        public static string ToRepositoryNameFormat(this string s)
        {
            return string.Join("-", s.ToNamespacePascalCase()
                .ToLowerInvariant()
                .Split(new char[] { '.' }));
        }

        public static string ToNamespacePascalCase(this string s)
        {
            var words = s.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries)
                         .Select(word => word.Substring(0, 1).ToUpper() +
                                         word.Substring(1).ToLower()).ToList();
            for (int i = 0; i < words.Count; i++)
            {
                var item = words[i];
                if (item.Length < 3)
                {
                    words[i] = item.ToUpperInvariant();
                }
            }
            var result = string.Join('.', words);
            return result;
        }

        public static string ToWordPascalCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new Exception("Null or empty string cannot be converted to WordPascalCase");
            }
            var splitted = s.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
            if (splitted.Length == 1)
            {
                var firstCharacter = s.Substring(0, 1);
                var capitalized = firstCharacter.ToUpper();
                var otherPart = string.Empty;
                if (s.Length > 1)
                {
                    otherPart = s.Substring(1, s.Length -1);
                }
                return $"{capitalized}{otherPart}";
            }
            var words = splitted.Select(word => word.Substring(0, 1).ToUpper() +
                                         word.Substring(1).ToLower()).ToList();
            var result = string.Concat(words);
            return result;
        }


        public static string[] StringToParams(string text)
        {
            return Regex.Matches(text, @"[\""].+?[\""]|[^ ]+")
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .Select(k => k.Trim('\"'))
                        .ToArray();
        }


        public static string MillisecondsToHumanTime(long ms)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(ms);
            string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds);
            return answer;
        }


    }
}
