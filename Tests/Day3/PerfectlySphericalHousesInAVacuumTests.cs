using Ninject;
using NUnit.Framework;
using System;

namespace Tests.Day3
{
    [TestFixture]
    public sealed class PerfectlySphericalHousesInAVacuumTests : TestBase
    {
        // > delivers presents to 2 houses: one at the starting location, and one to the east.
        [TestCase(">", ExpectedResult = 2)]
        // ^>v< delivers presents to 4 houses in a square, including twice to the house at his starting/ending location.
        [TestCase("^>v<", ExpectedResult = 4)]
        // ^v^v^v^v^v delivers a bunch of presents to some very lucky children at only 2 houses.
        [TestCase("^v^v^v^v^v", ExpectedResult = 2)]
        public int Perfectly_spherical_houses_in_a_Vacuum_returns_specified_example_values(string input)
        {
            IPerfectlySphericalHousesInAVacuum perfectlySphericalHousesInAVacuum = Kernel.Get<IPerfectlySphericalHousesInAVacuum>();
            perfectlySphericalHousesInAVacuum.Process(input);
            int housesThatReceiveAtLeastOnePresent = perfectlySphericalHousesInAVacuum.HousesWithAtLeastOnePresent;
            return housesThatReceiveAtLeastOnePresent;
        }

        [Test]
        public void PerfectlySphericalHousesInAVacuum_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day3.PerfectlySphericalHousesInAVacuum_PuzzleInput.txt");
            IPerfectlySphericalHousesInAVacuum perfectlySphericalHousesInAVacuum = Kernel.Get<IPerfectlySphericalHousesInAVacuum>();
            perfectlySphericalHousesInAVacuum.Process(myPuzzleInput);
            Console.WriteLine($"Answer = {perfectlySphericalHousesInAVacuum.HousesWithAtLeastOnePresent}");
        }

        // ^v delivers presents to 3 houses, because Santa goes north, and then Robo-Santa goes south.
        [TestCase("^v", ExpectedResult = 3)]
        // ^>v< now delivers presents to 3 houses, and Santa and Robo-Santa end up back where they started.
        [TestCase("^>v<", ExpectedResult = 3)]
        // ^v^v^v^v^v now delivers presents to 11 houses, with Santa going one direction and Robo-Santa going the other.
        [TestCase("^v^v^v^v^v", ExpectedResult = 11)]
        public int Perfectly_spherical_houses_in_a_Vacuum_returns_specified_example_values_for_two_agents(string input)
        {
            IPerfectlySphericalHousesInAVacuum perfectlySphericalHousesInAVacuum = Kernel.Get<IPerfectlySphericalHousesInAVacuum>();
            perfectlySphericalHousesInAVacuum.NumberOfAgents = 2;
            perfectlySphericalHousesInAVacuum.Process(input);
            int housesThatReceiveAtLeastOnePresent = perfectlySphericalHousesInAVacuum.HousesWithAtLeastOnePresent;
            return housesThatReceiveAtLeastOnePresent;
        }

        [Test]
        public void PerfectlySphericalHousesInAVacuum_part_two_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day3.PerfectlySphericalHousesInAVacuum_PuzzleInput.txt");
            IPerfectlySphericalHousesInAVacuum perfectlySphericalHousesInAVacuum = Kernel.Get<IPerfectlySphericalHousesInAVacuum>();
            perfectlySphericalHousesInAVacuum.NumberOfAgents = 2;
            perfectlySphericalHousesInAVacuum.Process(myPuzzleInput);
            Console.WriteLine($"Answer = {perfectlySphericalHousesInAVacuum.HousesWithAtLeastOnePresent}");
        }
    }
}