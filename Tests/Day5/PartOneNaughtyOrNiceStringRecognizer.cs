using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests.Day5
{
    public sealed class PartOneNaughtyOrNiceStringRecognizer : INaughtyOrNiceStringRecognizer
    {
        public StringType RecognizeStringType(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (ContainsOneOfForbiddenStrings(input))
                return StringType.Naughty;

            if (ContainsAtLeastThreeVowels(input) && ContainsAtLeastOneLetterTwiceInARow(input))
                return StringType.Nice;

            return StringType.Naughty;
        }

        private bool ContainsOneOfForbiddenStrings(string input)
        {
            Debug.Assert(input != null);

            foreach (string forbiddenString in _forbiddenStrings)
            {
                if (input.Contains(forbiddenString))
                    return true;
            }

            return false;
        }

        private bool ContainsAtLeastThreeVowels(string input)
        {
            Debug.Assert(input != null);

            return input.Count(chr => _vowels.Contains(chr)) >= 3;
        }

        private bool ContainsAtLeastOneLetterTwiceInARow(string input)
        {
            Debug.Assert(input != null);

            for (int i = 0; i < input.Length - 1; ++i)
            {
                if (input[i] == input[i + 1])
                    return true;
            }

            return false;
        }

        private static readonly IList<string> _forbiddenStrings = new List<string>() { "ab", "cd", "pq", "xy", }.AsReadOnly();
        private static readonly ISet<char> _vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u', };
    }
}