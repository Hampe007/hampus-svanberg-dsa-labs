using System;
using System.Collections.Generic;

namespace DSA.Lab04B
{
    /// <summary>
    /// Priority queue implemented with a binary min-heap.
    ///
    /// Invariant:
    /// each parent's Priority is <= the Priority of each of its children.
    ///
    /// The heap is NOT fully sorted.
    /// </summary>
    public sealed class BinaryHeapDispatchQueue<T>
    {
        private readonly List<PriorityEntry<T>> _heap = new List<PriorityEntry<T>>();

        public int Count => _heap.Count;

        public void Enqueue(T item, int priority)
        {
            // TODO:
            // 1. Append the new entry at the end.
            // 2. Restore the heap invariant upward.
            throw new NotImplementedException();
        }

        public PriorityEntry<T> PeekNext()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("The dispatch queue is empty.");
            }

            return _heap[0];
        }

        public PriorityEntry<T> DequeueNext()
        {
            // TODO:
            // 1. Reject an empty queue.
            // 2. Remember the root: that is the minimum.
            // 3. Move the final heap entry to the root and remove the final slot.
            // 4. If anything remains, restore the invariant downward.
            // 5. Return the old root.
            throw new NotImplementedException();
        }

        private void SiftUp(int index)
        {
            // TODO: compare the entry with its parent and swap while needed.
            throw new NotImplementedException();
        }

        private void SiftDown(int index)
        {
            // TODO:
            // Compare the current entry with its children.
            // If a child should come first, swap with the smaller-priority child.
            throw new NotImplementedException();
        }

        private static int ParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private static int LeftChildIndex(int index)
        {
            return 2 * index + 1;
        }

        private static int RightChildIndex(int index)
        {
            return 2 * index + 2;
        }

        private void Swap(int a, int b)
        {
            PriorityEntry<T> temp = _heap[a];
            _heap[a] = _heap[b];
            _heap[b] = temp;
        }
    }
}
