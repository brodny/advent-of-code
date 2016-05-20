using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

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

            Graph graph = new Graph();

            for (int i = 0; i < graphDescription.Length; ++i)
            {
                ParseDescription(graphDescription[i], graph);
            }

            return graph;
        }

        private void ParseDescription(string graphDescription, Graph graph)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(graphDescription));
            Debug.Assert(graph != null);

            GroupCollection matches = _parserRegex.Match(graphDescription).Groups;
            if (matches.Count != 4)
                throw new ArgumentException("Invalid input", nameof(graphDescription));

            string firstCity = matches[1].Value;
            string secondCity = matches[2].Value;
            string distanceStr = matches[3].Value;

            if (string.IsNullOrWhiteSpace(firstCity))
                throw new ArgumentException("First city is not specified", nameof(graphDescription));
            if (string.IsNullOrWhiteSpace(secondCity))
                throw new ArgumentException("Second city is not specified", nameof(graphDescription));
            if (string.IsNullOrWhiteSpace(distanceStr))
                throw new ArgumentException("Distance is not specified", nameof(graphDescription));

            int distance;
            if (!int.TryParse(distanceStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out distance))
                throw new ArgumentException("Distance is not a number", nameof(graphDescription));

            AddVertexIfDoesNotExist(graph, firstCity);
            AddVertexIfDoesNotExist(graph, secondCity);
            graph.AddEdge(firstCity, secondCity, distance);
        }

        private static void AddVertexIfDoesNotExist(Graph graph, string vertex)
        {
            Debug.Assert(graph != null);
            Debug.Assert(!string.IsNullOrWhiteSpace(vertex));

            if (!graph.ContainsVertex(vertex))
                graph.AddVertex(vertex);
        }

        private readonly Regex _parserRegex = new Regex(@"^(\w+)\sto\s(\w+)\s=\s(\d+)$", RegexOptions.Compiled);
    }
}