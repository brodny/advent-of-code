using System;

namespace Tests.Day9
{
    public sealed class ShortestDistanceCalculator : IShortestDistanceCalculator, IGraphDistanceCalculator
    {
        public int Calculate(IGraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            throw new NotImplementedException();
        }
    }
}