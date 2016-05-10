using System;

namespace Tests.Day1
{
    public sealed class NotQuiteLispPositionFinder : INotQuiteLispPositionFinder
    {
        private readonly INotQuiteLisp _notQuiteLisp;

        public NotQuiteLispPositionFinder(
            INotQuiteLisp notQuiteLisp
        )
        {
            if (notQuiteLisp == null)
                throw new ArgumentNullException(nameof(notQuiteLisp));

            _notQuiteLisp = notQuiteLisp;
        }

        public int FindIndexOfElementThatFirstReachesSpecifiedPosition(string input, int expectedPosition)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            _notQuiteLisp.Reset();

            for (int i = 0; i < input.Length; ++i)
            {
                string command = input[i].ToString();
                _notQuiteLisp.Process(command);
                if (_notQuiteLisp.FloorNumber == expectedPosition)
                    return i;
            }

            return -1;
        }
    }
}