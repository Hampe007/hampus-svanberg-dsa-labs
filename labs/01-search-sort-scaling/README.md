# Lab 1 — Search, Sort and Scaling

## What this lab is for

This is the first full data-structures-and-algorithms lab. You will connect the growth patterns from Thursday's preparation to real code.

You do **not** need to finish everything immediately. Work in small verified steps.

**Understand → Predict → Implement → Test → Inspect → Commit**

## If you feel lost, start here

1. Open this README.
2. Open `DSA.Lab01.sln` in your IDE.
3. Find the three places below. You do not need to understand every file.
4. Build the solution.
5. Run the tests once and read which tests pass/fail.
6. Start with **Task 1 only**.
7. If you understand the task but cannot see the next coding step, open `HINTS.md` and use one hint level at a time.

Ask for help if you spend more than about 15–20 minutes blocked by Git, the IDE, project setup, or finding a file. Those are not the point of this lab.

## Where the code lives

| What | Path | Why you care |
| --- | --- | --- |
| Algorithms | `src/DSA.Lab01.Core/` | This is where you implement search/sort behavior. |
| Console app | `src/DSA.Lab01.App/Program.cs` | This runs examples and rough timings. |
| Tests | `tests/DSA.Lab01.Tests/` | These check important behavior and edge cases. |
| Progressive hints | `HINTS.md` | Use one level at a time if you understand the task but cannot translate it into code. |

## Run the baseline

From the root of your `gp26-dsa-labs` repository:

```bash
dotnet restore labs/01-search-sort-scaling/DSA.Lab01.sln
dotnet build labs/01-search-sort-scaling/DSA.Lab01.sln
dotnet run --project labs/01-search-sort-scaling/src/DSA.Lab01.App
dotnet test labs/01-search-sort-scaling/tests/DSA.Lab01.Tests
```

The starter is intentionally incomplete. `LinearSearch` is already implemented as an anchor. Some Binary Search and Insertion Sort tests are expected to fail at first.

---

## Task 1 — Understand linear search

Open `SearchAlgorithms.cs` and inspect `LinearSearch`.

Before changing anything, answer:

- What does `i` represent?
- When does the method stop early?
- What does `-1` mean?
- What is the best case?
- What is the worst case?
- If the array becomes twice as large, what happens to the worst-case amount of work?

Run the existing linear-search tests.

## Task 2 — Implement binary search

`BinarySearch` receives an array that is already sorted in ascending order.

Use this process:

1. Keep a `low` and `high` index for the part that could still contain the target.
2. Inspect the middle value.
3. If it matches, return the middle index.
4. If the middle value is too small, discard the lower half.
5. If it is too large, discard the upper half.
6. Continue until the search range is empty.
7. Return `-1` if the target is not present.

Start by tracing this example on paper:

```text
values = [2, 5, 8, 12, 16, 23, 38]
target = 23
```

For every iteration, write `low`, `high`, `middle`, and the value you inspect.

Then implement the method and run the tests.

### Checkpoint

Explain in one or two sentences why binary search requires ordered data.

## Task 3 — Strengthen the tests

Read the supplied tests.

Add **at least one meaningful search test** of your own. Choose a case that increases your confidence rather than simply duplicating an existing test.

Examples to consider:

- a one-element array;
- an empty array;
- a target between two existing values;
- another boundary case you can explain.

## Task 4 — Implement insertion sort

Open `SortAlgorithms.cs`.

Insertion sort grows a sorted section from left to right. For each new value:

1. remember the value you are inserting;
2. move larger values one position to the right;
3. place the remembered value into the gap.

First trace this by hand:

```text
[7, 2, 5, 1]
```

After each outer-loop step, write the full array.

Then implement `InsertionSort` and run the tests.

Do not replace this task with `Array.Sort`. The point here is to understand the mechanism. In normal application code, using the standard library is usually preferable when sorting itself is not the learning objective.

## Task 5 — Observe scaling

Run the console application several times:

```bash
dotnet run --project labs/01-search-sort-scaling/src/DSA.Lab01.App -c Release
```

The app prints rough timings for linear search and binary search on increasingly large sorted arrays.

Record what you observe. Exact timings will vary between machines and runs, so focus on the **pattern**, not the smallest number.

| Input size | Linear search | Binary search | What changed? |
| ---: | ---: | ---: | --- |
| 10,000 | | | |
| 100,000 | | | |
| 1,000,000 | | | |

Think about:

- Which method changes more as `n` grows?
- Does the measurement match your prediction?
- Why should you be cautious about drawing conclusions from one timing run?

## Task 6 — Connect behavior to Big-O

By the end of the lab, be able to explain at an introductory level:

- why array indexing is treated as **O(1)**;
- why linear search is **O(n)** in the worst case;
- why binary search is **O(log n)**;
- why this insertion-sort implementation is **O(n²)** in the worst case.

Big-O describes how work grows with input size. It does **not** tell you an exact number of milliseconds.

## Commit checkpoint

Make commits at coherent working states. For example:

- `Implement binary search`
- `Add binary-search edge case test`
- `Implement insertion sort`

There is no required number of commits.

## Core completion target

If you are short on time or feeling overwhelmed, prioritize this order:

1. understand the existing linear search;
2. implement and test binary search;
3. implement and test insertion sort;
4. run the benchmark once and write down what you observe;
5. answer the checkpoint questions.

The optional work below can wait.

## Optional extensions

### A. Count comparisons instead of time

Instrument a copy of one search algorithm to count how many values it inspects. Compare the exact count against your timing observations.

### B. Try different search targets

Compare searching for the first element, last element, and a missing element. Why does this affect linear search differently from binary search?

### C. Compare with the standard library

After completing your own implementation, explore `Array.BinarySearch` and `Array.Sort`. Compare the API and behavior with your implementation.

### D. Near-sorted insertion sort

Try insertion sort on an already-sorted array and on an array where only two neighboring values are swapped. What do you observe? This hints at why a worst-case bound does not describe every input equally well.

## Working together and using references

Discuss predictions, traces, bugs, and tests with classmates. Pair programming is welcome.

The teacher repository contains a completed reference implementation. Try each core task yourself first. If you use the reference to get unstuck, compare it with your attempt and make sure you can explain the difference.
