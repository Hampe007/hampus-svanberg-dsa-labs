# Lab 3 - Hash Tables and Fast Lookup

## Task 1 - Inspect linear lookup

`LinearIdMap<TValue>` stores all key-value pairs in one `List<Entry<TValue>>`. `TryGetValue` checks each entry's key in order. If it finds a match, it returns `true` and assigns the stored value to the `out` parameter.

### What is the worst case when the key is missing?

The method must check every entry before it can conclude that the key is missing. For n entries, this means n key comparisons: O(n) time. It then assigns `default!` to the output value and returns `false`.

A successful lookup also takes O(n) time in the worst case, when the matching entry is last. Finding the key in the first entry takes O(1) time.

### What changes when the collection grows from 100 entries to 100,000?

A missing-key lookup grows from 100 key comparisons to 100,000: 1,000 times as many comparisons. The algorithm stays the same, but its work grows linearly with the number of entries. This does not guarantee exactly 1,000 times the elapsed time, because actual timing also depends on hardware and runtime behavior.

### What operation are we trying to improve?

We want to improve lookup by key: finding the value associated with an ID, or determining that the ID is absent. A hash table uses the key to select a bucket so that lookup only scans entries in that bucket. With a suitable distribution of keys and enough buckets relative to the number of entries, lookup is expected O(1), although collisions can still lead to O(n) worst-case lookup.

`Set` also scans the list to check whether a key already exists before replacing its value or appending a new entry, so that search has O(n) worst-case cost too.
