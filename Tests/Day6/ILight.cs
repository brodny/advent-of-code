namespace Tests.Day6
{
    public interface ILight
    {
        int TotalBrightness { get; }
        void Toggle();
        void TurnOff();
        void TurnOn();
    }
}