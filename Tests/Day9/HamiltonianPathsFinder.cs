using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests.Day9
{
    public sealed class HamiltonianPathsFinder : IHamiltonianPathsFinder
    {
        public IEnumerable<IEnumerable<string>> FindPaths(IGraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            List<string> vertices = graph.Vertices.ToList();

            hamiltonianPathsForCompleteGraph = new List<IEnumerable<int>>();
            for (int i = 0; i < vertices.Count; ++i)
            {
                GenerateHamiltonianPathsForCompleteGraph(Enumerable.Range(0, vertices.Count).ToList(), i, new Stack<int>(), new bool[vertices.Count]);
            }
            IEnumerable<IEnumerable<int>> allHamiltonianPaths = hamiltonianPathsForCompleteGraph;
            IEnumerable<IEnumerable<string>> allPaths = allHamiltonianPaths
                .Select(hamiltonianPath => hamiltonianPath.Select(vertexIndex => vertices[vertexIndex]));
            return allPaths;
        }

        private List<IEnumerable<int>> hamiltonianPathsForCompleteGraph;

        private void GenerateHamiltonianPathsForCompleteGraph(List<int> vertices,
            int currentVertex, Stack<int> verticesInPath, bool[] visitedVertices)
        {
            Debug.Assert(vertices != null);
            Debug.Assert(verticesInPath != null);
            Debug.Assert(visitedVertices != null);

            verticesInPath.Push(currentVertex);
            if (verticesInPath.Count == vertices.Count)
            {
                hamiltonianPathsForCompleteGraph.Add(verticesInPath.ToList());
            }
            else
            {
                visitedVertices[currentVertex] = true;
                foreach (int neighbour in GetNeighbours(vertices, currentVertex))
                {
                    if (!visitedVertices[neighbour])
                        GenerateHamiltonianPathsForCompleteGraph(vertices, neighbour, verticesInPath, visitedVertices);
                }
                visitedVertices[currentVertex] = false;
            }
            verticesInPath.Pop();
        }

        private IEnumerable<int> GetNeighbours(List<int> vertices, int vertex)
        {
            Debug.Assert(vertices != null);

            return vertices.Except(vertex);
        }
    }
}