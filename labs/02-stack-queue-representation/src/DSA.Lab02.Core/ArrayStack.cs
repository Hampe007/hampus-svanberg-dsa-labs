using System;

namespace DSA.Lab02
{
    public sealed class ArrayStack<T>
    {
        private readonly T[] _items;
        private int _count;

        public ArrayStack(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _items = new T[capacity];
        }

        public int Count => _count;
        public int Capacity => _items.Length;

        public void Push(T item)
        {
            if (_count == Capacity)
            {
                throw new InvalidOperationException("The stack is full.");
            }
            _items[_count] = item;
            _count++;
        }

        public T Peek()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            return _items[_count - 1];
        }

        public T Pop()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            _count--;
            T item = _items[_count];
            _items[_count] = default!;
            return item;
        }
    }
}
