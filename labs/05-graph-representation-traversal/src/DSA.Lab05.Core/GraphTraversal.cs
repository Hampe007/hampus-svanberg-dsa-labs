using System;
using System.Collections.Generic;

namespace DSA.Lab05
{
    /// <summary>
    /// Iterative graph traversals.
    ///
    /// Complete these methods after the adjacency representation is working.
    /// Think in terms of:
    /// frontier + visited + neighbors.
    /// </summary>
    public static class GraphTraversal
    {
        public static IReadOnlyList<string> BreadthFirstOrder(
            AdjacencyGraph graph,
            string start)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            if (!graph.ContainsNode(start))
            {
                return Array.Empty<string>();
            }

            var order = new List<string>();
            var frontier = new Queue<string>();
            var visited = new HashSet<string>();

            visited.Add(start);
            frontier.Enqueue(start);

            while (frontier.Count > 0)
            {
                string current = frontier.Dequeue();
                order.Add(current);

                foreach (string neighbor in graph.GetNeighbors(current))
                {
                    if (visited.Add(neighbor))
                    {
                        frontier.Enqueue(neighbor);
                    }
                }
            }

            return order;
        }

        public static IReadOnlyList<string> DepthFirstOrder(
            AdjacencyGraph graph,
            string start)
        {
            throw new NotImplementedException();
        }

        public static bool IsReachableBfs(
            AdjacencyGraph graph,
            string start,
            string target)
        {
            throw new NotImplementedException();
        }
    }
}
