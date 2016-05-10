using System;

namespace Tests.Day6
{
    internal sealed class PartTwoLight : ILight
    {
        private int _totalBrightness = 0;
        public int TotalBrightness => _totalBrightness;

        public void Toggle()
        {
            _totalBrightness += 2;
        }

        public void TurnOn()
        {
            ++_totalBrightness;
        }

        public void TurnOff()
        {
            --_totalBrightness;
            if (_totalBrightness < 0)
                _totalBrightness = 0;
        }
    }
}