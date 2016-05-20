using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Day8
{
    public sealed class StringParser : IStringParser
    {
        private readonly IStringLengthComputer _stringLengthComputer;
        private readonly IStringEncoder _stringEncoder;

        public StringParser(IStringLengthComputer stringLengthComputer,
            IStringEncoder stringEncoder)
        {
            if (stringLengthComputer == null)
                throw new ArgumentNullException(nameof(stringLengthComputer));
            if (stringEncoder == null)
                throw new ArgumentNullException(nameof(stringEncoder));

            _stringLengthComputer = stringLengthComputer;
            _stringEncoder = stringEncoder;
        }

        public IStringParseResult Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            int charactersOfCode = input.Length;
            int length = _stringLengthComputer.ComputeLength(input);
            int encodedLength = _stringEncoder.Encode(input).Length;

            StringParseResult result = new StringParseResult(charactersOfCode, length, encodedLength);
            return result;
        }

        public IComposedStringParseResult Parse(IEnumerable<string> input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (input.Any(@in => @in == null))
                throw new ArgumentException("One of input strings is null", nameof(input));

            IEnumerable<IStringParseResult> results = input.Select(@in => Parse(@in));
            ComposedStringParseResult result = new ComposedStringParseResult(results);
            return result;
        }

        private sealed class StringParseResult : IStringParseResult
        {
            public StringParseResult(int charactersOfCode, int length, int encodedLength)
            {
                CharactersOfCode = charactersOfCode;
                Length = length;
                EncodedLength = encodedLength;
            }

            public int CharactersOfCode { get; }
            public int Length { get; }
            public int EncodedLength { get; }
        }

        private sealed class ComposedStringParseResult : IComposedStringParseResult
        {
            private readonly IReadOnlyList<IStringParseResult> _results;

            public ComposedStringParseResult(IEnumerable<IStringParseResult> results)
            {
                if (results == null)
                    throw new ArgumentNullException(nameof(results));
                if (results.Any(result => result == null))
                    throw new ArgumentException("One of results is null", nameof(results));

                _results = results.ToList().AsReadOnly();
                CharactersOfCode = _results.Sum(result => result.CharactersOfCode);
                Length = _results.Sum(result => result.Length);
                EncodedLength = _results.Sum(result => result.EncodedLength);
            }

            public int CharactersOfCode { get; }
            public int Length { get; }
            public int EncodedLength { get; }

            public IEnumerator<IStringParseResult> GetEnumerator() => _results.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }
    }
}