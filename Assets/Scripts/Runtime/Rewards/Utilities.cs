using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Runtime.Rewards
{
    public static class Utilities
    {
        private static readonly List<char> NumberSuffixes = new List<char>() { ' ', 'K', 'M', 'B', 'T', };
        private const int SuffixSpace = 3;

        public static string UseSuffix(this int number)
        {
            var stringBuilder = new StringBuilder(4);
            var suffixIndex = (number.ToString().Length - 1) / SuffixSpace;
            var shortNumber = Mathf.CeilToInt(number / Mathf.Pow(10, SuffixSpace * suffixIndex));
            stringBuilder.Append(shortNumber);
            stringBuilder.Append(NumberSuffixes[suffixIndex]);
            return stringBuilder.ToString();
        }
    }
}