﻿using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests.Day9
{
    [TestFixture]
    public sealed class AllInASingleNightTests : TestBase
    {
        [Test]
        public void Shortest_sample_route_is_calculated_correctly()
        {
            string[] citiesAndDistances = new string[]
            {
                "London to Dublin = 464",
                "London to Belfast = 518",
                "Dublin to Belfast = 141",
            };

            IGraphParser parser = Kernel.Get<IGraphParser>();
            IGraph graph = parser.Parse(citiesAndDistances);
            IShortestRouteCalculator calculator = Kernel.Get<IShortestRouteCalculator>();
            IGraphRoute shortestRoute = calculator.Calculate(graph);

            Assert.That(shortestRoute.Route, Is.EquivalentTo(new List<string>() { "London", "Dublin", "Belfast", })
                | Is.EquivalentTo(new List<string>() { "Belfast", "Dublin", "London", }));
            Assert.AreEqual(605, shortestRoute.TotalWeight);
        }

        [Test]
        public void AllInASingleNight_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day9.AllInASingleNight_PuzzleInput.txt");
            string[] citiesAndDistances = Utils.SplitLines(myPuzzleInput);

            IGraphParser parser = Kernel.Get<IGraphParser>();
            IGraph graph = parser.Parse(citiesAndDistances);
            IShortestRouteCalculator calculator = Kernel.Get<IShortestRouteCalculator>();
            IGraphRoute shortestRoute = calculator.Calculate(graph);

            Console.WriteLine($"Answer = {shortestRoute.TotalWeight}");
            Console.WriteLine($"Route = {string.Join(" ", shortestRoute.Route)}");
        }

        [Test]
        public void Longest_sample_route_is_calculated_correctly()
        {
            string[] citiesAndDistances = new string[]
            {
                "London to Dublin = 464",
                "London to Belfast = 518",
                "Dublin to Belfast = 141",
            };

            IGraphParser parser = Kernel.Get<IGraphParser>();
            IGraph graph = parser.Parse(citiesAndDistances);
            ILongestRouteCalculator calculator = Kernel.Get<ILongestRouteCalculator>();
            IGraphRoute longestRoute = calculator.Calculate(graph);

            Assert.That(longestRoute.Route, Is.EquivalentTo(new List<string>() { "Dublin", "London", "Belfast", })
                | Is.EquivalentTo(new List<string>() { "Belfast", "London", "Dublin", }));
            Assert.AreEqual(982, longestRoute.TotalWeight);
        }

        [Test]
        public void AllInASingleNight_part_two_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day9.AllInASingleNight_PuzzleInput.txt");
            string[] citiesAndDistances = Utils.SplitLines(myPuzzleInput);

            IGraphParser parser = Kernel.Get<IGraphParser>();
            IGraph graph = parser.Parse(citiesAndDistances);
            ILongestRouteCalculator calculator = Kernel.Get<ILongestRouteCalculator>();
            IGraphRoute shortestRoute = calculator.Calculate(graph);

            Console.WriteLine($"Answer = {shortestRoute.TotalWeight}");
            Console.WriteLine($"Route = {string.Join(" ", shortestRoute.Route)}");
        }
    }
}