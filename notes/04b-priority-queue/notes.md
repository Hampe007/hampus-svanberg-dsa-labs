# Lab 4B - Priority Queue

## Task 1 - Inspect the baseline

- **Enqueue - O(1) amortized:** Appending to the unsorted list is cheap. Occasionally the list must resize and copy its entries, but the average cost per insertion is constant over many insertions.
- **Peek minimum - O(n):** The queue scans the list to find the smallest priority.
- **Dequeue minimum - O(n):** The queue scans for the smallest priority, then removes the entry. Removing it may also shift later entries, so the total remains O(n).

1. **Which operation is cheap?** `Enqueue`, because it appends without sorting or searching.
2. **Which operation repeats work?** Both `PeekNext` and `DequeueNext` scan for the minimum on every call.
3. **What happens when 100,000 candidates are waiting?** Each peek or dequeue scans all 100,000 entries to find the minimum. Repeatedly removing candidates becomes expensive: draining the queue takes O(n^2) total time.
4. **Why did Problem D from the World Map workshop motivate a different representation?** It repeatedly needs the cheapest waiting candidate. A binary min-heap makes that repeated operation more efficient, with O(1) peek and O(log n) dequeue, while insertion takes O(log n).
