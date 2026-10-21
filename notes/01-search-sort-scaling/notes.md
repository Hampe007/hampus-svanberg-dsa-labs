@ -0,0 +1,73 @@
# Lab 01 — Search, Sort and Scaling

## Task 1 — Understand linear search

`LinearSearch` goes through the array one value at a time, comparing each value with the target. It returns the index of the first match. If the target does not exist, it returns `-1`.

### What does i represent?

`i` is the zero-based index of the value currently being checked. It starts at `0` and increases by one after each iteration. `values[i]` reads the value at that index.

### When does the method stop early?

When `values[i] == target`, the method immediately returns `i`. It does not check the remaining values.

### What does -1 mean?

`-1` means that the target was not found. Array indices start at `0`, so `-1` cannot be confused with a valid index. An empty array also returns `-1` because there are no values to check.

### What is the best case?

The target is the first value in the array. The method checks one value and returns `0`, regardless of the array's size. This is O(1).

### What is the worst case?

The target is the last value or is missing from the array. The method must check all `n` values. This is O(n).

### If the array becomes twice as large, what happens to the worst-case amount of work?

The method checks twice as many values in the worst case. For example, an array with 100 values may require 100 checks, while an array with 200 values may require 200 checks. This describes the amount of work, not an exact running time.
## Task 2 — Implement binary search

`BinarySearch` searches an array that is already sorted in ascending order. Instead of checking every value, it checks the middle of the remaining search range and discards the half that cannot contain the target.

### Tracing the example

For `values = [2, 5, 8, 12, 16, 23, 38]` and `target = 23`:

| Iteration | low | high | middle | Value inspected | Action |
| --- | ---: | ---: | ---: | ---: | --- |
| 1 | 0 | 6 | 3 | 12 | Set `low = 4` because 12 is smaller than 23. |
| 2 | 4 | 6 | 5 | 23 | Return index `5` because the value matches. |

### How my implementation works

I start with `low = 0` and `high = sortedValues.Length - 1`, so the search range initially includes the entire array.

The `while (low <= high)` loop continues while the range contains at least one possible index. I calculate the middle index with `(low + high) / 2`. Integer division rounds down here.

If the middle value matches the target, I return its index. If it is smaller than the target, I set `low = middle + 1`. If it is larger, I set `high = middle - 1`.

The `+ 1` and `- 1` exclude the value already checked and ensure that the search range becomes smaller. If the range becomes empty, the method returns `-1`.

An empty array starts with `high = -1`, so the loop is skipped and the method returns `-1`. The existing null check throws `ArgumentNullException` if the input array is null.

### Why does binary search require ordered data?

The ordering tells me which half can still contain the target. Without sorted data, discarding a half could remove the target before I find it.

### Efficiency

Binary search has O(log n) worst-case time complexity because each unsuccessful iteration discards roughly half the remaining values. My iterative implementation uses O(1) extra space because it only keeps a fixed number of variables.

A safer midpoint calculation for very large arrays is `low + (high - low) / 2`, which avoids potential integer overflow from adding `low` and `high`.