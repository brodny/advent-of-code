using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Day9
{
    public sealed class LongestRouteCalculator : SpecifiedRouteCalculator, ILongestRouteCalculator
    {
        public LongestRouteCalculator(IHamiltonianPathsFinder hamiltonianPathsFinder)
            : base(hamiltonianPathsFinder)
        {
        }

        protected override IGraphRoute FindRouteToReturn(IEnumerable<IGraphRoute> routes)
        {
            if (routes == null)
                throw new ArgumentNullException(nameof(routes));
            if (routes.Any(route => route == null))
                throw new ArgumentException("One of routes is null", nameof(routes));

            IOrderedEnumerable<IGraphRoute> sortedRoutes = routes.OrderByDescending(route => route.TotalWeight);
            IGraphRoute longestRoute = sortedRoutes.FirstOrDefault();
            return longestRoute;
        }
    }
}