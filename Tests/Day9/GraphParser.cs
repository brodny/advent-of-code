using System;
using System.Linq;

namespace Tests.Day9
{
    public class GraphParser : IGraphParser
    {
        public IGraph Parse(string[] graphDescription)
        {
            if (graphDescription == null)
                throw new ArgumentNullException(nameof(graphDescription));
            if (graphDescription.Any(descriptionLine => string.IsNullOrWhiteSpace(descriptionLine)))
                throw new ArgumentException("One of descriptions is null or empty", nameof(graphDescription));

            throw new NotImplementedException();
        }
    }
}