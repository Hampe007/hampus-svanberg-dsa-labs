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

## Task 2 - Understand buckets

The bucket function is `bucketIndex = key % bucketCount`. The `%` operator gives the remainder after division. With five buckets, the possible indices are 0 through 4.

| Key | Calculation | Bucket index |
| --- | --- | --- |
| 101 | 101 = 20 * 5 + 1 | 1 |
| 205 | 205 = 41 * 5 + 0 | 0 |
| 734 | 734 = 146 * 5 + 4 | 4 |
| 206 | 206 = 41 * 5 + 1 | 1 |
| 999 | 999 = 199 * 5 + 4 | 4 |

### Which keys collide?

- 101 and 206 collide in bucket 1.
- 734 and 999 collide in bucket 4.
- 205 is alone in bucket 0. Buckets 2 and 3 are empty.

A collision means that different keys produce the same bucket index. It is expected and does not mean that one entry must overwrite another. This hash table uses separate chaining: each bucket contains a list of entries. To find a key, we calculate its bucket index and then compare the full keys within that bucket to identify the correct entry.

## Task 3 - Implement `IntHashTable<TValue>`

The table uses separate chaining: an array of buckets, where each bucket is a `List<Entry<TValue>>`. `GetBucketIndex(key)` calculates `key % _buckets.Length` and rejects negative keys. The methods search only the selected bucket and compare full keys to distinguish entries that collide.

### Set

`Set` searches the selected bucket for the key. If the key already exists, it replaces the entry with `new Entry<TValue>(key, value)` and returns immediately. The whole entry is replaced because `Entry<TValue>` is immutable. Updating an existing key does not increase `Count`.

If the loop finishes without a match, the method adds a new entry to the bucket and increments `_count`. These steps happen after the loop, so insertion also works when the bucket is empty. Different keys in the same bucket remain separate entries.

### TryGetValue

`TryGetValue` searches the selected bucket. When it finds the key, it assigns the stored value to the `out` parameter and returns `true`. If the key is missing, it assigns `default!` and returns `false`. The `!` suppresses a nullable warning; it does not change the default value at runtime.

### ContainsKey

`ContainsKey` reuses lookup with `return TryGetValue(key, out _);`. The discard `_` ignores the stored value because only the boolean result is needed.

Checking whether the output value is non-null would be incorrect. An existing key can store null, while a missing integer-valued key produces the non-null default value 0. The boolean result tells us whether the key exists.

## Task 4 - Reason about collisions

### Why can keys 1 and 6 both exist when the table has five buckets?

Both keys select bucket 1 because `1 % 5 = 1` and `6 % 5 = 1`. The table uses separate chaining, so each bucket holds a list of entries. `Set` compares the full keys within that list: it replaces an entry only when its key matches, otherwise it adds a new entry. Keys 1 and 6 therefore remain separate entries, and lookup can retrieve each value by comparing the full key.

### What happens to lookup cost if almost every key ends up in the same bucket?

The bucket becomes a long list, and lookup must scan through it. If that bucket holds almost all n entries, finding a key near the end or confirming that a key mapping to that bucket is missing takes O(n) time. The table then behaves much like the linear lookup for those searches. A lookup in an empty bucket is still O(1).

### Why does expected O(1) lookup not mean "exactly one CPU operation"?

Big-O describes how the amount of work grows with the collection size. A lookup still calculates the bucket index, accesses the bucket, compares keys, and returns a result. These steps require multiple CPU operations, and collisions can require several comparisons.

Expected O(1) lookup assumes a suitable distribution of keys and enough buckets relative to the number of entries, so bucket lists stay short on average. This implementation has a fixed bucket count and does not resize. If we keep adding entries without adding buckets, even evenly distributed keys create longer lists, so lookup cannot stay constant on average indefinitely.

## Task 5 - Add one meaningful test

Added `HashTable_MissingCollidingKeyReturnsFalse`. This test creates a table with five buckets and stores keys 1 and 6 which it then looks up key 11, which was never added. (All three keys map to bucket 1.)

The test verifies that `TryGetValue(11, out var value)` returns `false`, that the output value is null for this string-valued table, and that `ContainsKey(11)` returns `false`.

Using `out var value` declares the output variable and lets C# infer its type from the method parameter. The assertions still check the same behavior as when the type was written explicitly.

This covers a gap in the existing missing-key test which stores key 1 but looks up key 99, which maps to an empty bucket. The new test checks that lookup correctly reports a missing key after searching an occupied bucket containing colliding entries. Sharing a bucket does not mean that the key exists.