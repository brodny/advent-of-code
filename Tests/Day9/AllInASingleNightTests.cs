using Ninject;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests.Day9
{
    [TestFixture]
    public sealed class AllInASingleNightTests : TestBase
    {
        [Test]
        public void Sample_route_is_calculated_correctly()
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

            CollectionAssert.AreEqual(new List<string>() { "London", "Dublin", "Belfast", }, shortestRoute.Route);
            Assert.AreEqual(605, shortestRoute.TotalWeight);
        }
    }
}