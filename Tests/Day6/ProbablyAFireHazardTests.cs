using Ninject;
using NUnit.Framework;
using System;

namespace Tests.Day6
{
    [TestFixture]
    public sealed class ProbablyAFireHazardTests : TestBase
    {
        // turn on 0,0 through 999,999 would turn on (or leave on) every light.
        [TestCase("turn on 0,0 through 999,999", false, ExpectedResult = 1000000)]
        // turn on 0,0 through 999,999 would turn on (or leave on) every light.
        [TestCase("turn on 0,0 through 999,999", true, ExpectedResult = 1000000)]
        // toggle 0,0 through 999,0 would toggle the first line of 1000 lights,
        // turning off the ones that were on, and turning on the ones that were off.
        [TestCase("toggle 0,0 through 999,0", false, ExpectedResult = 1000)]
        // toggle 0,0 through 999,0 would toggle the first line of 1000 lights,
        // turning off the ones that were on, and turning on the ones that were off.
        [TestCase("toggle 0,0 through 999,0", true, ExpectedResult = 999000)]
        // turn off 499,499 through 500,500 would turn off (or leave off) the middle four lights.
        [TestCase("turn off 499,499 through 500,500", false, ExpectedResult = 0)]
        // turn off 499,499 through 500,500 would turn off (or leave off) the middle four lights.
        [TestCase("turn off 499,499 through 500,500", true, ExpectedResult = 999996)]
        public int Probably_a_fire_hazard_returns_specified_example_values(string input, bool allLightsAreInitiallyTurnedOn)
        {
            ILightsGrid<PartOneLight> lightsGrid = Kernel.Get<ILightsGrid<PartOneLight>>();
            if (allLightsAreInitiallyTurnedOn)
            {
                lightsGrid.TurnOnAllOfTheLights();
            }
            else
            {
                lightsGrid.TurnOffAllOfTheLights();
            }

            ILightsGridController lightsGridController = new LightsGridController<PartOneLight>(lightsGrid);
            lightsGridController.ProcessCommand(input);
            int numberOfLightsThatAreTurnedOn = lightsGrid.TotalBrightness;
            return numberOfLightsThatAreTurnedOn;
        }

        [Test]
        public void ProbablyAFireHazard_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day6.ProbablyAFireHazard_PuzzleInput.txt");
            string[] mySplittedPuzzleInput = Utils.SplitLines(myPuzzleInput);

            ILightsGrid<PartOneLight> lightsGrid = Kernel.Get<ILightsGrid<PartOneLight>>();
            lightsGrid.TurnOffAllOfTheLights();
            ILightsGridController lightsGridController = new LightsGridController<PartOneLight>(lightsGrid);
            lightsGridController.ProcessCommands(mySplittedPuzzleInput);
            int numberOfLightsThatAreTurnedOn = lightsGrid.TotalBrightness;
            Console.WriteLine($"Answer = {numberOfLightsThatAreTurnedOn}");
        }

        // turn on 0,0 through 0,0 would increase the total brightness by 1.
        [TestCase("turn on 0,0 through 0,0", ExpectedResult = 1)]
        // toggle 0,0 through 999,999 would increase the total brightness by 2000000.
        [TestCase("toggle 0,0 through 999,999", ExpectedResult = 2000000)]
        public int Probably_a_fire_hazard_part_two_returns_specified_example_values(string input)
        {
            ILightsGrid<PartTwoLight> lightsGrid = Kernel.Get<ILightsGrid<PartTwoLight>>();
            ILightsGridController lightsGridController = new LightsGridController<PartTwoLight>(lightsGrid);
            lightsGridController.ProcessCommand(input);
            int totalBrightness = lightsGrid.TotalBrightness;
            return totalBrightness;
        }

        [Test]
        public void ProbablyAFireHazard_part_two_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day6.ProbablyAFireHazard_PuzzleInput.txt");
            string[] mySplittedPuzzleInput = Utils.SplitLines(myPuzzleInput);

            ILightsGrid<PartTwoLight> lightsGrid = Kernel.Get<ILightsGrid<PartTwoLight>>();
            ILightsGridController lightsGridController = new LightsGridController<PartTwoLight>(lightsGrid);
            lightsGridController.ProcessCommands(mySplittedPuzzleInput);
            int totalBrightness = lightsGrid.TotalBrightness;
            Console.WriteLine($"Answer = {totalBrightness}");
        }
    }
}