using System;

namespace DSA.Lab02
{
    public sealed class RingQueue<T>
    {
        private readonly T[] _items;
        private int _head;
        private int _tail;
        private int _count;

        public RingQueue(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _items = new T[capacity];
        }

        public int Count => _count;
        public int Capacity => _items.Length;

        public void Enqueue(T item)
        {
            // TODO: write at tail, advance tail with wrap-around, update count.
            throw new NotImplementedException();
        }

        public T Peek()
        {
            // TODO: return the item at head without removing it.
            throw new NotImplementedException();
        }

        public T Dequeue()
        {
            // TODO: read/clear at head, advance head with wrap-around, update count.
            throw new NotImplementedException();
        }
    }
}
