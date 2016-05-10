using System;
using System.Diagnostics;

namespace Tests.Day5
{
    public sealed class PartTwoNaughtyOrNiceStringRecognizer : INaughtyOrNiceStringRecognizer
    {
        public StringType RecognizeStringType(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (ContainsAPairOfAnyTwoLettersThatAppearsAtLeastTwiceInTheStringWithoutOverlapping(input)
                && ContainsAtLeastOneLetterWhichRepeatsWithExactlyOneLetterBetweenThem(input))
                return StringType.Nice;

            return StringType.Naughty;
        }

        private bool ContainsAPairOfAnyTwoLettersThatAppearsAtLeastTwiceInTheStringWithoutOverlapping(string input)
        {
            Debug.Assert(input != null);

            const int NUMBER_OF_NEXT_LETTERS = 2;
            for (int i = 0; i < input.Length - NUMBER_OF_NEXT_LETTERS; ++i)
            {
                string nextLetters = input.Substring(i, NUMBER_OF_NEXT_LETTERS);
                int nextTwoLettersStartingIndex = i + NUMBER_OF_NEXT_LETTERS;
                int indexOfNextAppearanceOfNextTwoLetters = input.IndexOf(nextLetters, nextTwoLettersStartingIndex);
                if (indexOfNextAppearanceOfNextTwoLetters != -1)
                    return true;
            }

            return false;
        }

        private bool ContainsAtLeastOneLetterWhichRepeatsWithExactlyOneLetterBetweenThem(string input)
        {
            Debug.Assert(input != null);

            const int NUMBER_OF_CHARACTERS_TO_SKIP = 1;
            for (int i = 0; i < input.Length - (NUMBER_OF_CHARACTERS_TO_SKIP + 1); ++i)
            {
                char currentCharacter = input[i];
                int nextCharacterIndex = i + NUMBER_OF_CHARACTERS_TO_SKIP + 1;
                char nextCharacter = input[nextCharacterIndex];
                if (currentCharacter == nextCharacter)
                    return true;
            }

            return false;
        }
    }
}