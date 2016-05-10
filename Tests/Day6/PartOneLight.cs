namespace Tests.Day6
{
    internal sealed class PartOneLight : ILight
    {
        private bool _isTurnedOn = false;
        public int TotalBrightness => _isTurnedOn ? 1 : 0;

        public void Toggle()
        {
            _isTurnedOn = !_isTurnedOn;
        }

        public void TurnOn()
        {
            _isTurnedOn = true;
        }

        public void TurnOff()
        {
            _isTurnedOn = false;
        }
    }
}