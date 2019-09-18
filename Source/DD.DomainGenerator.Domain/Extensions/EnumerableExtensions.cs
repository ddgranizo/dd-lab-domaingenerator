using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Extensions
{
    public static class EnumerableExtensions
    {

        public static string ToDisplayList<T>(
            this IEnumerable<T> source,
            Func<T, string> func,
            string header,
            string lineInitialzierChar = "-",
            bool tab = true)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(header);
            bool enumerated = false;
            if (lineInitialzierChar.ToLowerInvariant() == "i")
            {
                enumerated = true;
            }
            int counter = 1;
            foreach (var item in source)
            {
                StringBuilder sbLine = new StringBuilder(tab ? "\t" : "");
                sbLine.Append(enumerated ? $"{counter++.ToString()}- " : $"{lineInitialzierChar} ");
                sbLine.Append(func(item));
                sb.AppendLine(sbLine.ToString());
            }
            return sb.ToString();
        }
    }
}
