# Lab 3 Progressive Hints

## `Set`

### Hint 1
Find the correct bucket first. Only search inside that bucket.

### Hint 2
If the key already exists, replace its value and do not change `Count`.

### Hint 3
Otherwise append a new `Entry<TValue>` to the bucket and increment `Count`.

## `TryGetValue`

### Hint 1
Again, calculate one bucket and search only there.

### Hint 2
If a matching key is found, assign its value to the `out` parameter and return true.

### Hint 3
If no match is found, assign `default!` and return false.

## Collision debugging

With five buckets, trace keys 1, 6, and 11. They all map to bucket 1. Your implementation must still distinguish them by the original key.
