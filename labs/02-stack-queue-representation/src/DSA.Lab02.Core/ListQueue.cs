using System;
using System.Collections.Generic;

namespace DSA.Lab02
{
    // Correct behavior, deliberately straightforward representation.
    // Removing index 0 from List<T> shifts the remaining elements left.
    public sealed class ListQueue<T>
    {
        private readonly List<T> _items = new List<T>();

        public int Count => _items.Count;

        public void Enqueue(T item)
        {
            _items.Add(item);
        }

        public T Peek()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return _items[0];
        }

        public T Dequeue()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            T item = _items[0];
            _items.RemoveAt(0);
            return item;
        }
    }
}
