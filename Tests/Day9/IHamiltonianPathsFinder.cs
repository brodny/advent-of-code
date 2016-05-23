using System.Collections.Generic;

namespace Tests.Day9
{
    public interface IHamiltonianPathsFinder
    {
        IEnumerable<IEnumerable<string>> FindPaths(IGraph graph);
    }
}