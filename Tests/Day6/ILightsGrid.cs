namespace Tests.Day6
{
    public interface ILightsGrid<TLightType>
        where TLightType : ILight, new()
    {
        int TotalBrightness { get; }

        void TurnOnAllOfTheLights();
        void TurnOffAllOfTheLights();
        void Toggle(Point startingPoint, Point finishPoint);
        void TurnOn(Point startingPoint, Point finishPoint);
        void TurnOff(Point startingPoint, Point finishPoint);
    }
}