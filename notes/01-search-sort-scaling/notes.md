# Lab 01 - Search, Sort and Scaling

## Task 1 - Understand linear search

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

## Task 2 - Implement binary search

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

## Task 3 - Strengthen the tests

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

## Task 4 - Implement insertion sort

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

## Task 5 - Observe scaling

I ran the console application nine times in Release mode from the Lab 01 folder:

```bash
dotnet run --project ./src/DSA.Lab01.App -c Release
```

The benchmark searches for `-1` in sorted arrays containing values from `0` to `n - 1`. Because the target is missing, linear search must inspect the entire array. Binary search repeatedly halves the remaining range until it is empty.

### Recorded timings

The table uses the median of the nine displayed timings for each input size and method. The median is the middle value after sorting the measurements. It represents a typical run without choosing the fastest result. Each displayed timing is itself an average per search over repeated calls in the app. All times are in microseconds (µs).

| Input size | Linear search (median) | Binary search (median) | What changed? |
| ---: | ---: | ---: | --- |
| 10,000 | 6.11 µs | 0.10 µs | Starting point for comparison. |
| 100,000 | 32.01 µs | 0.12 µs | Input grew 10 times; linear time grew about 5.2 times, while binary time grew about 1.2 times. |
| 1,000,000 | 305.34 µs | 0.19 µs | Input grew another 10 times; linear time grew about 9.5 times, while binary time grew about 1.6 times. |

Across the nine runs, linear search ranged from 6.07–6.68 µs at 10,000 values, 31.91–37.41 µs at 100,000, and 301.21–317.89 µs at 1,000,000. Binary search ranged from 0.10–0.17 µs, 0.12–0.12 µs, and 0.18–0.20 µs respectively, at the precision shown by the app.

### Which method changes more as n grows?

Linear search changes much more. From 10,000 to 1,000,000 values, the input becomes 100 times larger and its median time increases about 50 times. Binary search's median time increases only about 1.9 times and stays below one microsecond in these runs.

### Does the measurement match my prediction?

The overall pattern matches the prediction: linear search grows much more strongly with input size than binary search. Linear search checks all `n` values for this missing target, giving O(n) work. Binary search takes O(log n) work; increasing the input tenfold adds only about three or four search iterations.

The timings do not scale in exact proportion to the theoretical work. In particular, the first tenfold increase produces about a fivefold increase in linear time rather than tenfold. Runtime warm-up, measurement overhead, and hardware effects can affect these rough timings. The results support the expected growth pattern, but timing measurements alone do not prove the complexity.

### Why should I be cautious about drawing conclusions from one timing run?

Background activity, CPU speed changes, memory caches, and .NET runtime compilation and optimization can influence a run. This app uses different repetition counts for different sizes and has no dedicated benchmark warm-up. Binary-search timings are especially short, so overhead and rounding can noticeably affect the displayed numbers.

Repeating the application helps reveal the consistent pattern and unusual results. For example, linear search at 100,000 values usually took about 32 µs, but two runs showed 35.44 µs and 37.41 µs. I should compare the pattern across runs rather than treat any single measurement as an exact performance guarantee.

(Also the use of IDE, and in my case, Rider, may be diffrent to someone who uses VS or VS Code which can impact the timings)

## Task 6 - Connect behavior to Big-O

Big-O describes how the amount of work grows as the input size `n` increases. It gives an upper bound on growth, ignoring constant factors and smaller terms. It does not tell me an exact number of milliseconds or mean that every input takes the worst-case amount of work.

### Why is array indexing treated as O(1)?

Accessing `values[index]` goes directly to a position in the array. For an `int[]`, the position can be calculated from the array's starting location, the index, and the fixed size of each integer. The program does not need to scan the earlier elements.

The amount of work does not grow with the array's length, so indexing is treated as O(1), or constant time. Constant time does not mean that every access takes exactly the same number of nanoseconds; hardware and memory caches can still affect the measured time.

### Why is linear search O(n) in the worst case?

My `LinearSearch` checks values one at a time. If the target is missing or is the last value, it must check all `n` values before returning.

Doubling the array size doubles the number of values checked in the worst case. This gives O(n), or linear growth. If the first value matches, the method returns after one check, so its best case is O(1).

### Why is binary search O(log n)?

My `BinarySearch` checks the middle value and discards roughly half of the remaining range after each unsuccessful comparison. The possible range shrinks like `n`, `n / 2`, `n / 4`, `n / 8`, and so on.

The number of times I can halve `n` before reaching a range of about one value is approximately `log₂(n)`. For example, one million values need at most about 20 iterations. Doubling the input adds only about one iteration in the worst case.

Each iteration does a constant amount of work, giving O(log n) worst-case time. This depends on having sorted data and direct array indexing. The method assumes the array is already sorted; this bound does not include sorting it first.

### Why is insertion sort O(n²) in the worst case?

My `InsertionSort` inserts each new value into the sorted section. For a reverse-sorted array, every new value is smaller than all the values already in that section, so all of them must shift right.

The first insertion shifts one value, the next shifts two, and the last can shift `n - 1` values. The total number of shifts is:

```text
1 + 2 + ... + (n - 1) = n(n - 1) / 2
```

The `n²` term dominates as `n` grows, so the worst-case time is O(n²), or quadratic growth. Doubling a large input produces roughly four times as much worst-case work.

Nested loops alone do not prove O(n²): the important detail is how many times the inner loop runs overall. On an already-sorted array, no shifting is needed, so this implementation takes O(n) time.

### Connecting the explanations to my measurements

In Task 5, linear-search timings grew much more than binary-search timings as the input grew. This agrees with the expected O(n) and O(log n) patterns. The measured times did not follow exact ratios because Big-O describes growth in work rather than precise elapsed time.
