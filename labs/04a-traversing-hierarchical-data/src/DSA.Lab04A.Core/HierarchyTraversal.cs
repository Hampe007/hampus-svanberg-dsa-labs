using System;
using System.Collections.Generic;

namespace DSA.Lab04A
{
    /// <summary>
    /// Recursive algorithms over a rooted hierarchy.
    ///
    /// The key idea is structural recursion:
    /// solve the current node, then apply the same operation to each child subtree.
    /// </summary>
    public static class HierarchyTraversal
    {
        public static int CountNodes<T>(HierarchyNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            int count = 1;

            foreach (var child in root.Children)
            {
                count += CountNodes(child);
            }

            return count;
        }

        public static bool Contains<T>(HierarchyNode<T> root, T target)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (EqualityComparer<T>.Default.Equals(root.Value, target))
            {
                return true;
            }

            foreach (var child in root.Children)
            {
                if (Contains(child, target))
                {
                    return true;
                }
            }

            return false;
        }

        public static IReadOnlyList<T> DepthFirstPreOrder<T>(HierarchyNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            var values = new List<T>();
            VisitPreOrder(root, values);
            return values;
        }

        private static void VisitPreOrder<T>(HierarchyNode<T> node, List<T> values)
        {
            values.Add(node.Value);

            foreach (var child in node.Children)
            {
                VisitPreOrder(child, values);
            }
        }

        public static int MaxDepth<T>(HierarchyNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            int maxDepth = 0;

            foreach (var child in root.Children)
            {
                int childDepth = 1 + MaxDepth(child);
                maxDepth = Math.Max(maxDepth, childDepth);
            }

            return maxDepth;
        }
    }
}
