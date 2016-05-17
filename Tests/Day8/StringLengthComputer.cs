using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tests.Day8
{
    public sealed class StringLengthComputer : IStringLengthComputer
    {
        public int ComputeLength(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            _length = 0;

            IEnumerable<char> stringCharactersWithoutDelimitingQuotationMarks =
                GetStringCharactersWithoutDelimitingQuotationMarks(input);

            foreach (char stringCharacter in stringCharactersWithoutDelimitingQuotationMarks)
            {
                if (IsInHexadecimalCharMode())
                {
                    ProcessHexadecimalCharMode();
                }
                else if (IsInEscapeMode())
                {
                    ProcessEscapeMode(stringCharacter);
                }
                else
                {
                    ProcessNormalMode(stringCharacter);
                }
            }

            return _length;
        }

        private bool IsInHexadecimalCharMode() => _hexadecimalCharMode;
        private bool IsInEscapeMode() => _escapeMode;

        private void ProcessHexadecimalCharMode()
        {
            MarkOneMoreHexadecimalNotationCharacter();
            if (AllHexadecimalCharactersProcessed())
            {
                ExitHexadecimalCharMode();
                ExitEscapeMode();
                IncrementLength();
            }
        }

        private void MarkOneMoreHexadecimalNotationCharacter()
        {
            ++_hexadecimalCharactersFound;
        }

        private const int CHARACTERS_FOR_HEXADECIMAL_NOTATION = 2;

        private bool AllHexadecimalCharactersProcessed()
        {
            return _hexadecimalCharactersFound == CHARACTERS_FOR_HEXADECIMAL_NOTATION;
        }

        private void ExitHexadecimalCharMode()
        {
            _hexadecimalCharactersFound = 0;
            _hexadecimalCharMode = false;
        }

        private void ExitEscapeMode()
        {
            _escapeMode = false;
        }

        private void IncrementLength()
        {
            ++_length;
        }

        private void ProcessEscapeMode(char stringCharacter)
        {
            if (CharacterBeginsHexadecimalCharacterNotation(stringCharacter))
            {
                EnterHexadecimalCharMode();
            }
            else
            {
                IncrementLength();
                ExitEscapeMode();
            }
        }

        private const char CHARACTER_BEGINNING_HEXADECIMAL_CHARACTER_NOTATION = 'x';
        private static bool CharacterBeginsHexadecimalCharacterNotation(char stringCharacter)
        {
            return stringCharacter == CHARACTER_BEGINNING_HEXADECIMAL_CHARACTER_NOTATION;
        }

        private void EnterHexadecimalCharMode()
        {
            _hexadecimalCharMode = true;
        }

        private void ProcessNormalMode(char stringCharacter)
        {
            if (CharacterBeginsEscapeMode(stringCharacter))
            {
                EnterEscapeMode();
            }
            else
            {
                IncrementLength();
            }
        }

        private void EnterEscapeMode()
        {
            _escapeMode = true;
        }

        private static bool CharacterBeginsEscapeMode(char stringCharacter)
        {
            return stringCharacter == '\\';
        }

        private IEnumerable<char> GetStringCharactersWithoutDelimitingQuotationMarks(string input)
        {
            Debug.Assert(input != null);

            return input.Substring(1, input.Length - 2);
        }

        private int _length = 0;
        private bool _escapeMode = false;
        private bool _hexadecimalCharMode = false;
        private int _hexadecimalCharactersFound = 0;
    }
}