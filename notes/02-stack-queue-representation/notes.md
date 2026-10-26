# Lab 2 - Stack, Queue and Representation

## Task 1 - Trace the abstractions before coding

Stack:
`Push` A: A
`Push` B: A, B
`Push` C: A, B, C
`Pop` removes C, leaving A, B.
`Pop` removes B, leaving A.
Removed in order: C, B.

Queue:
`Enqueue` A: A
`Enqueue` B: A, B
`Enqueue` C: A, B, C
`Dequeue` removes A, leaving B, C.
`Dequeue` removes B, leaving C.
Removed in order: A, B.

A stack removes the last item added first, while a queue removes the first item added first.

## Task 2 - Implement `ArrayStack<T>`

The stack uses a fixed-size array and a count of how many items are stored.
If `Count` is 3, the top item is at index 2 because array indices start at 0.
The top is always at `_count - 1`, and the next free slot is at `_count`.

`Push` checks if the stack is full, stores the item at `_items[_count]`, and increases the count.
`Peek` checks if the stack is empty and returns the top item without changing the count.
`Pop` checks if the stack is empty, decreases the count, saves the top item, clears its slot, and returns it.
Clearing the slot means the array no longer keeps a reference to the removed item.

Pushing into a full stack throws `InvalidOperationException`.
`Peek` and `Pop` also throw `InvalidOperationException` when the stack is empty.

## Task 3 - Inspect the working `ListQueue<T>`

`Dequeue` removes the item at index 0. This is the oldest item still in the queue, so it follows FIFO: first in, first out.

After checking that the queue is not empty, it runs:

```csharp
T item = _items[0];  // Save the first item.
_items.RemoveAt(0); // Remove it. List<T> shifts the remaining items left.
return item;        // Return the saved item.
```

The shift closes the gap at index 0:

```text
Index:           0  1  2
Before Dequeue:  A  B  C
After Dequeue:   B  C
Returned: A
```

B moves from index 1 to 0, and C moves from index 2 to 1.

With 100,000 items, one `Dequeue` shifts 99,999 items. `Dequeue` takes `O(n)` time because the work grows with the number of queued items. Repeated dequeues can become expensive for a large queue.

`Peek` only reads index 0 without removing anything or shifting items, so it takes `O(1)` time.

The implementation works correctly. The cost comes from how the queue stores and removes its items. Task 3 is about understanding this, so there is no need to optimize it yet.

## Task 4 - Implement `RingQueue<T>`

The ring queue stores items in a fixed-size array and follows FIFO.
`_head` is the index of the next item to remove.
`_tail` is the index where the next item will be added.
`_count` is the number of stored items.

`Enqueue` checks if the queue is full, writes at `_tail`, advances `_tail`, and increases `_count`.
`Peek` checks if the queue is empty and returns the item at `_head` without changing anything.
`Dequeue` checks if the queue is empty, saves and clears the item at `_head`, advances `_head`, decreases `_count`, and returns the saved item.
Clearing the slot releases the array's reference to the removed item.

Both indices wrap back to 0 after the last slot:

```csharp
_tail = (_tail + 1) % Capacity;
_head = (_head + 1) % Capacity;
```

For a five-slot array, `(4 + 1) % 5` is 0.
(Here is a trace. A dash means an unused slot.)

```text
Operation    Array        head  tail  count  Returned
Start        - - - - -      0     0      0
Enqueue A    A - - - -      0     1      1
Enqueue B    A B - - -      0     2      2
Enqueue C    A B C - -      0     3      3
Enqueue D    A B C D -      0     4      4
Enqueue E    A B C D E      0     0      5
Dequeue      - B C D E      1     0      4     A
Dequeue      - - C D E      2     0      3     B
Enqueue F    F - C D E      2     1      4
Enqueue G    F G C D E      2     2      5
Dequeue      F G - D E      3     2      4     C
Dequeue      F G - - E      4     2      3     D
Dequeue      F G - - -      0     2      2     E
Dequeue      - G - - -      1     2      1     F
Dequeue      - - - - -      2     2      0     G
```

The array order can look different from the queue order. Starting from `_head` and wrapping around gives the correct FIFO order.
`_head` and `_tail` can be equal when the queue is empty or full. `Count` tells us which: 0 means empty, `Capacity` means full.

`Enqueue` throws `InvalidOperationException` when full. `Peek` and `Dequeue` throws it when empty.
All three operations take `O(1)` time because no items need to shift. The array takes `O(capacity)` space and does not grow automatically.

All 10 existing lab tests passed, including FIFO order, wrap-around, `Peek`, empty `Dequeue`, and full `Enqueue`.

**Wohoo!**