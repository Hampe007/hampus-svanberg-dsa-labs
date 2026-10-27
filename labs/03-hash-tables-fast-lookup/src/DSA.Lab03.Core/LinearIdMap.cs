using System.Collections.Generic;

namespace DSA.Lab03
{
    public sealed class LinearIdMap<TValue>
    {
        private readonly List<Entry<TValue>> _entries = new List<Entry<TValue>>();

        public int Count => _entries.Count;

        public void Set(int key, TValue value)
        {
            for (int i = 0; i < _entries.Count; ++i)
            {
                if (_entries[i].Key == key)
                {
                    _entries[i] = new Entry<TValue>(key, value);
                    return;
                }
            }

            _entries.Add(new Entry<TValue>(key, value));
        }

        public bool TryGetValue(int key, out TValue value)
        {
            for (int i = 0; i < _entries.Count; ++i)
            {
                if (_entries[i].Key == key)
                {
                    value = _entries[i].Value;
                    return true;
                }
            }

            value = default!;
            return false;
        }
    }
}
