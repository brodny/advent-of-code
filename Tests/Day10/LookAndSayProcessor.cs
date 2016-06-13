using System;
using System.Diagnostics;
using System.Text;

namespace Tests.Day10
{
    public sealed class LookAndSayProcessor : ILookAndSayProcessor
    {
        public string Process(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            StringBuilder resultBuilder = new StringBuilder();

            int i = 0;
            while (i < input.Length)
            {
                string sameCharacters = GetTheSameCharacters(input, i);
                Debug.Assert(sameCharacters != null);
                Debug.Assert(sameCharacters.Length > 0);

                resultBuilder.Append(sameCharacters.Length).Append(sameCharacters[0]);
                i += sameCharacters.Length;
            }

            return resultBuilder.ToString();
        }

        public string ProcessIteratively(string input, int numberOfIterations)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (numberOfIterations < 1)
                throw new ArgumentOutOfRangeException(nameof(numberOfIterations), "Number of iterations must be equal to or larger than 1.");

            for (int i = 0; i < numberOfIterations; ++i)
            {
                input = Process(input);
            }

            return input;
        }

        private string GetTheSameCharacters(string input, int startingIndex)
        {
            Debug.Assert(input != null);
            Debug.Assert(startingIndex >= 0);
            Debug.Assert(startingIndex < input.Length);

            int sameCharactersEndIndex = startingIndex + 1;
            while (sameCharactersEndIndex < input.Length && input[sameCharactersEndIndex] == input[startingIndex])
            {
                ++sameCharactersEndIndex;
            }

            int sameCharactersSubstringLength = sameCharactersEndIndex - startingIndex;
            string sameCharactersString = input.Substring(startingIndex, sameCharactersSubstringLength);
            return sameCharactersString;
        }
    }
}