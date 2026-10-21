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

## Task 3 — Strengthen the tests

### Choosing an extra search test

The supplied binary-search tests already cover a matching index, the first element, the last element, and a missing target between two existing values. They did not cover an empty array.

I added `BinarySearch_ReturnsMinusOneForEmptyArray` to check that searching an empty array returns `-1` without trying to access an element that does not exist.

```csharp
[Test]
public void BinarySearch_ReturnsMinusOneForEmptyArray()
{
    int[] values = { };

    int result = SearchAlgorithms.BinarySearch(values, 7);

    Assert.That(result, Is.EqualTo(-1));
}
```

### Why this case is meaningful

For an empty array, `low` starts at `0` and `high` becomes `-1`. The condition `low <= high` is immediately false, so the search loop is skipped and the method returns `-1`.

This test checks the boundary where there are no valid indices. It would catch an implementation that tries to inspect a middle value before checking whether the search range is empty. The existing empty-array test for linear search does not verify this behavior in binary search.

## Task 4 — Implement insertion sort

Insertion sort grows a sorted section from left to right. I save the next value, shift larger values one position to the right, and insert the saved value into the gap.

### Tracing the example

The first value forms the initial sorted section. For `[7, 2, 5, 1]`, the full array after each outer-loop step is:

| Step | Value to insert | Action | Full array afterward |
| --- | ---: | --- | --- |
| 1 | 2 | Shift 7 right and insert 2 at index 0. | `[2, 7, 5, 1]` |
| 2 | 5 | Shift 7 right and insert 5 at index 1. | `[2, 5, 7, 1]` |
| 3 | 1 | Shift 7, 5, and 2 right and insert 1 at index 0. | `[1, 2, 5, 7]` |

### How my implementation works

The outer loop starts at `i = 1` because the first value by itself is already sorted. `i` identifies the next value to insert into the sorted section.

I save `values[i]` in `valueToInsert` before moving any values. This prevents losing the value when its original position is overwritten.

`j` starts at `i - 1` and moves backward through the sorted section. While `j >= 0` and `values[j] > valueToInsert`, I copy `values[j]` to `values[j + 1]` and decrease `j`.

The condition checks `j >= 0` first. Because `&&` stops evaluating when that condition is false, the code never tries to read `values[-1]`.

When the inner loop ends, `j + 1` is the insertion position. I place `valueToInsert` there. After each outer-loop step, the sorted section includes one more value.

The method changes the original array directly. It does not create a second array or use `Array.Sort`. Empty and one-element arrays need no sorting, so the outer loop is skipped. Null input throws `ArgumentNullException`.

### Efficiency

The implementation uses O(1) extra space because it only stores a fixed number of variables.

For an already-sorted array, no values need shifting, so the running time is O(n). In the worst case, such as a reverse-sorted array, each new value must move past all earlier values. The total number of shifts is `1 + 2 + ... + (n - 1)`, giving O(n²) time.

Shifting larger values is a standard efficient implementation of insertion sort. Other sorting algorithms can be faster for large unsorted arrays, but this implementation follows the mechanism required by the task, so I followed the task specs.