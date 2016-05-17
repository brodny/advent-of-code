using System;
using System.Diagnostics;

namespace Tests.Day8
{
    public sealed class StringParser : IStringParser
    {
        private readonly IStringLengthComputer _stringLengthComputer;

        public StringParser(IStringLengthComputer stringLengthComputer)
        {
            if (stringLengthComputer == null)
                throw new ArgumentNullException(nameof(stringLengthComputer));

            _stringLengthComputer = stringLengthComputer;
        }

        public IStringParseResult Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            int charactersOfCode = input.Length;
            int length = _stringLengthComputer.ComputeLength(input);

            StringParseResult result = new StringParseResult(charactersOfCode, length);
            return result;
        }

        private sealed class StringParseResult : IStringParseResult
        {
            public StringParseResult(int charactersOfCode, int length)
            {
                CharactersOfCode = charactersOfCode;
                Length = length;
            }

            public int CharactersOfCode { get; }
            public int Length { get; }
        }
    }
}