using System;
using Tests.Tools;

namespace Tests.Day1
{
    public sealed class NotQuiteLisp : INotQuiteLisp
    {
        private readonly IParser _parser;

        public NotQuiteLisp(IParser parser)
        {
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

            _parser = parser;
            _parser.DefineToken('(', GoUpOneFloor);
            _parser.DefineToken(')', GoDownOneFloor);
        }

        private int _floorNumber = 0;
        public int FloorNumber => _floorNumber;

        public void Process(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            _parser.Parse(input);
        }

        public void Reset()
        {
            _floorNumber = 0;
        }

        private void GoUpOneFloor()
        {
            ++_floorNumber;
        }

        private void GoDownOneFloor()
        {
            --_floorNumber;
        }
    }
}