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

## Task 3 - Insert with sift up

`Enqueue` appends a new `PriorityEntry<T>` to the list, then calls `SiftUp` with its index. Appending preserves the complete-tree shape, but the new priority may be smaller than its parent's priority.

1. Compare the entry with its parent using `(index - 1) / 2`.
2. Stop if the parent has an equal or lower priority.
3. Otherwise, swap the entry with its parent and continue from the parent index.
4. Stop when the entry reaches the root or its parent already satisfies the min-heap invariant.

For the README example, inserting priority 2 gives:

```text
before:                 [3, 4, 9, 12, 7, 10]
append 2 at index 6:    [3, 4, 9, 12, 7, 10, 2]
swap with parent at 2: [3, 4, 2, 12, 7, 10, 9]
swap with parent at 0: [2, 4, 3, 12, 7, 10, 9]
```

The new entry reaches the root, restoring the min-heap invariant. Equal priorities do not need to swap, and inserting into an empty queue stops immediately because the new entry is already at index 0.

Sift up follows one path toward the root and takes O(log n) time in the worst case, using O(1) additional space. Enqueue takes O(log n) amortized time, an individual insertion can take O(n) if the list must resize.

The four selected tests pass, covering insertion and peek, a late smaller item moving to the root, an empty queue's count, and peeking on an empty queue.

## Task 4 - Remove the minimum with sift down

`DequeueNext` removes and returns the root, which contains the smallest priority.

1. Throw `InvalidOperationException` if the queue is empty.
2. Remember the root entry.
3. Move the final entry to index 0 and remove the final list slot. This preserves the complete-tree shape.
4. If entries remain, call `SiftDown(0)` to restore the min-heap invariant.
5. Return the remembered root.

`SiftDown` chooses the child with the smaller priority. If the current entry has an equal or lower priority, it stops. Otherwise, it swaps with that child and continues downward until the ordering is correct or there are no children.

For example:

```text
before:                    [3, 4, 9, 12, 7, 10]
replace root, remove last:  [10, 4, 9, 12, 7]
swap with smaller child 4: [4, 10, 9, 12, 7]
swap with smaller child 7: [4, 7, 9, 12, 10]
return removed priority:   3
```

Choosing the smaller child matters: swapping 10 with 9 in the first step would leave 9 above its smaller child 4, breaking the invariant.

- **Empty heap:** Throws before accessing index 0.
- **One remaining item:** Removes the only entry and skips sift down because the heap is now empty.
- **Only a left child:** Uses the left child without accessing a missing right child.
- **Two children:** Compares their priorities and uses the smaller one. Either child is valid when their priorities are equal.

Removal takes O(log n) time in the worst case because sift down follows one path through the tree's height. It uses O(1) additional space. Removing the final list slot avoids shifting the remaining entries.

All 10 supplied tests pass, including empty removal, one-item removal, restoring the next minimum, ascending removal order, and preserving items with duplicate priorities.
