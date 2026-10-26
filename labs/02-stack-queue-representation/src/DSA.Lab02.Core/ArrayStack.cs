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
            // TODO: place the item at the logical top and update the state.
            throw new NotImplementedException();
        }

        public T Peek()
        {
            // TODO: return the logical top without removing it.
            throw new NotImplementedException();
        }

        public T Pop()
        {
            // TODO: remove and return the logical top.
            throw new NotImplementedException();
        }
    }
}
