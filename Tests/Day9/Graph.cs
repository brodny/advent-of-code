using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tests.Day9
{
    public sealed class Graph : IGraph
    {
        public bool ContainsVertex(string vertex)
        {
            CheckVertexName(vertex);
            return _vertices.Contains(vertex);
        }

        public void AddVertex(string vertex)
        {
            CheckVertexName(vertex);
            if (ContainsVertex(vertex))
                throw new ArgumentException($"Vertex '{vertex}' already exists.", nameof(vertex));
            _vertices.Add(vertex);
        }

        public void AddEdge(string firstVertex, string secondVertex, int weight)
        {
            CheckVertexName(firstVertex);
            CheckVertexName(secondVertex);
            CheckIfVertexExists(firstVertex);
            CheckIfVertexExists(secondVertex);

            Tuple<string, string> edge = Tuple.Create(firstVertex, secondVertex);
            _edges.Add(edge, weight);
        }

        private void CheckVertexName(string vertex)
        {
            if (!VertexNameIsCorrect(vertex))
                throw new ArgumentNullException(nameof(vertex));
        }

        private bool VertexNameIsCorrect(string vertex)
        {
            return !string.IsNullOrWhiteSpace(vertex);
        }

        private void CheckIfVertexExists(string vertex)
        {
            Debug.Assert(VertexNameIsCorrect(vertex));
            if (!ContainsVertex(vertex))
                throw new ArgumentException($"Vertex '{vertex}' does not exist", nameof(vertex));
        }

        private readonly HashSet<string> _vertices = new HashSet<string>();
        private readonly Dictionary<Tuple<string, string>, int> _edges = new Dictionary<Tuple<string, string>, int>();
    }
}