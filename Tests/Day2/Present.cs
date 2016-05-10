using System;

namespace Tests.Day2
{
    public sealed class Present
    {
        public Present(int length, int width, int height)
        {
            int firstSideArea = length * width;
            int secondSideArea = width * height;
            int thirdSideArea = height * length;
            TotalArea = 2*firstSideArea + 2*secondSideArea + 2*thirdSideArea;
            Slack = Math.Min(Math.Min(firstSideArea, secondSideArea), thirdSideArea);
            TotalPaperNeeded = TotalArea + Slack;

            RibbonNeeded = Math.Min(2*(length + width), Math.Min(2*(length + height), 2*(width + height)));
            RibbonNeededForTheBow = length * width * height;
            TotalRibbonNeeded = RibbonNeeded + RibbonNeededForTheBow;
        }

        public int TotalArea { get; }
        public int Slack { get; }
        public int TotalPaperNeeded { get; }
        public int RibbonNeeded { get; }
        public int RibbonNeededForTheBow { get; }
        public int TotalRibbonNeeded { get; }
    }
}