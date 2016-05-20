using Ninject;
using NUnit.Framework;

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
            IShortestDistanceCalculator calculator = Kernel.Get<IShortestDistanceCalculator>();
            int shortestDistance = calculator.Calculate(graph);

            Assert.AreEqual(605, shortestDistance);
        }
    }
}