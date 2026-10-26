# Lab 2 - Stack, Queue and Representation

## Task 1 - Trace the abstractions before coding

Stack:
Push A: A
Push B: A, B
Push C: A, B, C
Pop removes C, leaving A, B.
Pop removes B, leaving A.
Removed in order: C, B.

Queue:
Enqueue A: A
Enqueue B: A, B
Enqueue C: A, B, C
Dequeue removes A, leaving B, C.
Dequeue removes B, leaving C.
Removed in order: A, B.

A stack removes the last item added first, while a queue removes the first item added first.

## Task 2 - Implement ArrayStack<T>

The stack uses a fixed-size array and a count of how many items are stored.
If Count is 3, the top item is at index 2 because array indices start at 0.
The top is always at _count - 1, and the next free slot is at _count.

Push checks if the stack is full, stores the item at _items[_count], and increases the count.
Peek checks if the stack is empty and returns the top item without changing the count.
Pop checks if the stack is empty, decreases the count, saves the top item, clears its slot, and returns it.
Clearing the slot means the array no longer keeps a reference to the removed item.

Pushing into a full stack throws InvalidOperationException.
Peek and Pop also throw InvalidOperationException when the stack is empty.

## Task 3 - Inspect the working ListQueue<T>

Dequeue removes the item at index 0. This is the oldest item still in the queue, so it follows FIFO: first in, first out.

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

With 100,000 items, one Dequeue shifts 99,999 items. Dequeue takes O(n) time because the work grows with the number of queued items. Repeated dequeues can become expensive for a large queue.

Peek only reads index 0 without removing anything or shifting items, so it takes O(1) time.

The implementation works correctly. The cost comes from how the queue stores and removes its items. Task 3 is about understanding this, so there is no need to optimize it yet.
