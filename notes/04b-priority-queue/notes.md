# Lab 4B - Priority Queue

## Task 1 - Inspect the baseline

- **Enqueue - O(1) amortized:** Appending to the unsorted list is cheap. Occasionally the list must resize and copy its entries, but the average cost per insertion is constant over many insertions.
- **Peek minimum - O(n):** The queue scans the list to find the smallest priority.
- **Dequeue minimum - O(n):** The queue scans for the smallest priority, then removes the entry. Removing it may also shift later entries, so the total remains O(n).

1. **Which operation is cheap?** `Enqueue`, because it appends without sorting or searching.
2. **Which operation repeats work?** Both `PeekNext` and `DequeueNext` scan for the minimum on every call.
3. **What happens when 100,000 candidates are waiting?** Each peek or dequeue scans all 100,000 entries to find the minimum. Repeatedly removing candidates becomes expensive: draining the queue takes O(n^2) total time.
4. **Why did Problem D from the World Map workshop motivate a different representation?** It repeatedly needs the cheapest waiting candidate. A binary min-heap makes that repeated operation more efficient, with O(1) peek and O(log n) dequeue, while insertion takes O(log n).

## Task 2 - Connect the heap to Lab 4A trees

The example heap can be represented as both a complete binary tree and a list:

```text
          3
        /   \
       4     9
      / \   /
     12  7 10

index:     0  1  2   3  4   5
priority: [3, 4, 9, 12, 7, 10]
```

For index `i`, the parent index is `(i - 1) / 2` using integer division, the left child index is `2 * i + 1`, and the right child index is `2 * i + 2`. The root at index 0 has no parent, so the parent formula is only used when `i > 0`. Child indices must be less than the list count, which is 6 here.

| Index | Priority | Parent index (priority) | Left child index (priority) | Right child index (priority) |
| --- | --- | --- | --- | --- |
| 0 | 3 | None (root) | 1 (4) | 2 (9) |
| 1 | 4 | 0 (3) | 3 (12) | 4 (7) |
| 2 | 9 | 0 (3) | 5 (10) | None (index 6 is outside the list) |
| 3 | 12 | 1 (4) | None (index 7 is outside the list) | None (index 8 is outside the list) |
| 4 | 7 | 1 (4) | None (index 9 is outside the list) | None (index 10 is outside the list) |
| 5 | 10 | 2 (9) | None (index 11 is outside the list) | None (index 12 is outside the list) |

The tree is complete because each level fills from left to right. It satisfies the min-heap invariant because every parent has a priority less than or equal to its children: `3 <= 4`, `3 <= 9`, `4 <= 12`, `4 <= 7`, and `9 <= 10`.

The minimum is therefore at the root. The list is not fully sorted: for example, 12 appears before 7. Unlike Lab 4A's linked nodes, the list represents parent-child relationships through indices instead of child references.
