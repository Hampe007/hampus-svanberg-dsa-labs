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
            if (_count == Capacity)
            {
                throw new InvalidOperationException("The queue is full.");
            }

            _items[_tail] = item;
            _tail = (_tail + 1) % Capacity;
            _count++;
        }

        public T Peek()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return _items[_head];
        }

        public T Dequeue()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            T item = _items[_head];
            _items[_head] = default!;
            _head = (_head + 1) % Capacity;
            _count--;
            return item;
        }
    }
}
