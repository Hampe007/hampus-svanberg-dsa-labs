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
