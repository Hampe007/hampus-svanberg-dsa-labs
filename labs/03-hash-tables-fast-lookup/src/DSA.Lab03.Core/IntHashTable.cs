using System;
using System.Collections.Generic;

namespace DSA.Lab03
{
    public sealed class IntHashTable<TValue>
    {
        private readonly List<Entry<TValue>>[] _buckets;
        private int _count;

        public IntHashTable(int bucketCount)
        {
            if (bucketCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bucketCount));
            }

            _buckets = new List<Entry<TValue>>[bucketCount];
            for (int i = 0; i < _buckets.Length; ++i)
            {
                _buckets[i] = new List<Entry<TValue>>();
            }
        }

        public int Count => _count;
        public int BucketCount => _buckets.Length;

        public void Set(int key, TValue value)
        {
            // TODO: search only the correct bucket, replace or append.
            
            int bucketIndex = GetBucketIndex(key);
            List<Entry<TValue>> bucket = _buckets[bucketIndex];

            for (int i = 0; i < bucket.Count; i++)
            {
                if (bucket[i].Key == key)
                {
                    bucket[i] = new Entry<TValue>(key, value);
                    return;
                }
            }
            bucket.Add(new Entry<TValue>(key, value));
            _count++;
        }

        public bool TryGetValue(int key, out TValue value)
        {
            // TODO: search only the correct bucket.
            
            int bucketIndex = GetBucketIndex(key);
            List<Entry<TValue>> bucket = _buckets[bucketIndex];
            
            for (int i = 0; i < bucket.Count; i++)
            {
                if (bucket[i].Key == key)
                {
                    value = bucket[i].Value;
                    return true;
                }
            }
            
            value = default!;
            return false;
        }

        public bool ContainsKey(int key)
        {
            // TODO: reuse the lookup behavior if useful.
            
            return TryGetValue(key, out _);
        }

        private int GetBucketIndex(int key)
        {
            if (key < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(key), "This learning implementation uses non-negative integer keys.");
            }

            return key % _buckets.Length;
        }
    }
}
