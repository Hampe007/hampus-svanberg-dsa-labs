using System;
using System.Collections.Generic;

namespace DSA.Lab06
{
    public sealed class DijkstraRouteFinder
    {
        public RouteResult FindRoute(WeightedGraph graph, string start, string goal)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            if (!graph.ContainsNode(start) || !graph.ContainsNode(goal))
            {
                return RouteResult.NotFound(0);
            }

            if (start == goal)
            {
                return RouteResult.Success(new[] { start }, 0, 0);
            }

            var bestCost = new Dictionary<string, int>();
            var cameFrom = new Dictionary<string, string>();
            var frontier = new MinFrontier<string>();

            bestCost[start] = 0;
            frontier.Enqueue(start, 0);

            int expandedNodes = 0;

            while (frontier.Count > 0)
            {
                var entry = frontier.Dequeue();
                string current = entry.Value;

                // TODO Task 5: skip stale entries before expanding the node.
                expandedNodes++;

                // TODO Tasks 6 and 7: finalize the goal and reconstruct its route.

                foreach (WeightedEdge edge in graph.GetOutgoing(current))
                {
                    int candidate = bestCost[current] + edge.Cost;

                    if (!bestCost.TryGetValue(edge.To, out int knownCost) || candidate < knownCost)
                    {
                        bestCost[edge.To] = candidate;
                        cameFrom[edge.To] = current;
                        frontier.Enqueue(edge.To, candidate);
                    }
                }
            }

            return RouteResult.NotFound(expandedNodes);
        }
    }
}
