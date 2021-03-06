﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests.Day9
{
    public abstract class SpecifiedRouteCalculator : IGraphRouteCalculator
    {
        private readonly IHamiltonianPathsFinder _hamiltonianPathsFinder;

        public SpecifiedRouteCalculator(IHamiltonianPathsFinder hamiltonianPathsFinder)
        {
            if (hamiltonianPathsFinder == null)
                throw new ArgumentNullException(nameof(hamiltonianPathsFinder));

            _hamiltonianPathsFinder = hamiltonianPathsFinder;
        }

        public IGraphRoute Calculate(IGraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            IEnumerable<GraphRoute> allRoutes = GenerateAllRoutes(graph);
            IGraphRoute routeToReturn = FindRouteToReturn(allRoutes);
            return routeToReturn;
        }

        private IEnumerable<GraphRoute> GenerateAllRoutes(IGraph graph)
        {
            Debug.Assert(graph != null);

            IEnumerable<string> vertices = graph.Vertices;
            IEnumerable<IEnumerable<string>> allPossibleRoutes = FindAllHamiltonianPaths(graph);
            List<List<string>> test = allPossibleRoutes.Select(x => x.ToList()).ToList();
            IEnumerable<GraphRoute> allRoutes = allPossibleRoutes.Select(possibleRoute => ComputeRoute(possibleRoute, graph));
            return allRoutes;
        }

        private IEnumerable<IEnumerable<string>> FindAllHamiltonianPaths(IGraph graph)
        {
            Debug.Assert(graph != null);

            IEnumerable<IEnumerable<string>> allHamiltonianPaths = _hamiltonianPathsFinder.FindPaths(graph);
            return allHamiltonianPaths;
        }

        private GraphRoute ComputeRoute(IEnumerable<string> route, IGraph graph)
        {
            Debug.Assert(route != null);
            Debug.Assert(route.All(vertex => !string.IsNullOrWhiteSpace(vertex)));
            Debug.Assert(graph != null);

            List<string> routeList = route.ToList();
            int totalWeight = 0;
            for (int i = 1; i < routeList.Count; ++i)
            {
                int edgeWeight = graph.GetEdgeWeight(routeList[i - 1], routeList[i]);
                totalWeight += edgeWeight;
            }

            GraphRoute graphRoute = new GraphRoute(routeList, totalWeight);
            return graphRoute;
        }

        protected abstract IGraphRoute FindRouteToReturn(IEnumerable<IGraphRoute> routes);

        private sealed class GraphRoute : IGraphRoute
        {
            public GraphRoute(IEnumerable<string> routeList, int totalWeight)
            {
                if (routeList == null)
                    throw new ArgumentNullException(nameof(routeList));

                Route = routeList.ToList().AsReadOnly();
                TotalWeight = totalWeight;
            }

            public IReadOnlyList<string> Route { get; }
            public int TotalWeight { get; }
        }
    }
}