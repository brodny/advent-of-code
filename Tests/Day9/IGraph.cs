using System.Collections.Generic;

namespace Tests.Day9
{
    public interface IGraph
    {
        IEnumerable<string> Vertices { get; }
        int GetEdgeWeight(string vertex1, string vertex2);
    }
}