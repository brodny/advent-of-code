using System.Collections.Generic;

namespace Tests.Day9
{
    public interface IGraphRoute
    {
        IReadOnlyList<string> Route { get; }
        int TotalWeight { get; }
    }
}