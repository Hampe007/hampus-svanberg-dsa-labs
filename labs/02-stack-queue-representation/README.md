# Lab 2 - Stack, Queue and Representation

## What this lab is for

This lab is about a central DSA idea: **the operations you need should influence how you represent the data**.

You will work with two abstractions:

- a **stack**: last in, first out (LIFO);
- a **queue**: first in, first out (FIFO).

You will also compare two queue representations that can produce the same visible behavior while having different costs.

**Understand -> Trace -> Implement -> Test -> Inspect -> Commit**

## If you feel lost

You do not need to understand the whole solution.

1. Open this README.
2. Open `DSA.Lab02.sln`.
3. Find `ArrayStack.cs`, `ListQueue.cs`, `RingQueue.cs`, and the tests.
4. Build and run the tests once before editing anything.
5. Work on one task at a time.
6. Use `HINTS.md` one level at a time.
7. Ask early if the blocker is Git, the IDE, project setup, or finding a file.

## Run the lab

From the root of your `gp26-dsa-labs` repository:

```bash
dotnet restore labs/02-stack-queue-representation/DSA.Lab02.sln
dotnet build labs/02-stack-queue-representation/DSA.Lab02.sln
dotnet run --project labs/02-stack-queue-representation/src/DSA.Lab02.App
dotnet test labs/02-stack-queue-representation/tests/DSA.Lab02.Tests
```

The starter is intentionally incomplete. `ListQueue<T>` is a working baseline. `ArrayStack<T>` and `RingQueue<T>` contain incomplete operations.

## Task 1 - Trace the abstractions before coding

For a stack, trace:

```text
Push A
Push B
Push C
Pop
Pop
```

For a queue, trace:

```text
Enqueue A
Enqueue B
Enqueue C
Dequeue
Dequeue
```

Write the values removed in order. Explain the difference in one sentence.

## Task 2 - Implement `ArrayStack<T>`

The supplied representation is a fixed-capacity array plus a count.

Required behavior:

- `Push` adds at the logical top;
- `Peek` returns the logical top without removing it;
- `Pop` returns and removes the logical top;
- empty operations throw `InvalidOperationException`;
- pushing into a full stack throws `InvalidOperationException`.

Before coding, answer:

> If `Count == 3`, which array index contains the top item?

That relationship is the key invariant for this implementation.

## Task 3 - Inspect the working `ListQueue<T>`

`ListQueue<T>` works correctly. It is deliberately straightforward rather than efficient.

Read its `Dequeue` implementation and answer:

- Which element is removed?
- What must `List<T>` do with the remaining elements afterwards?
- If the queue contains 100,000 items, which operation may become expensive?

Do **not** optimize it yet. Understand it first.

## Task 4 - Implement `RingQueue<T>`

The ring queue uses a fixed-size array and three pieces of state:

```text
head   -> index of the next value to remove
tail   -> index where the next value will be inserted
count  -> number of stored values
```

The indices wrap back to zero when they reach the end of the array.

Implement:

- `Enqueue`
- `Peek`
- `Dequeue`

A useful wrap expression is:

```csharp
(index + 1) % Capacity
```

Do not copy code you cannot trace. Use a five-slot array on paper and move `head`, `tail`, and `count` after every operation.

## Task 5 - Compare representations

Both `ListQueue<T>` and `RingQueue<T>` are queues. They expose the same important behavior but organize the data differently.

Answer:

1. Why can `ListQueue<T>.Dequeue` require work proportional to the number of queued items?
2. Why does `RingQueue<T>.Dequeue` avoid shifting the remaining items?
3. What trade-off does the fixed-capacity ring queue make?
4. Why is the *abstraction* "queue" different from the *representation* used to implement it?

## Task 6 - Add one meaningful test

Add at least one test that increases your confidence. Good candidates include:

- wrap-around after several enqueue/dequeue operations;
- filling a structure exactly to capacity;
- empty after repeated operations;
- preserving FIFO/LIFO behavior through a longer sequence.

Be able to answer: **what bug would this test catch?**

## Unity Bridge

The shared **Frontier Dispatch** Unity laboratory contains a runnable Stack & Queue scene:

- undoing courier movement demonstrates stack behavior;
- dispatch jobs demonstrate queue behavior.

The Unity scene is for application and visualization. You are **not required** to reimplement the lab inside Unity. The core learning target is the headless C# structure and the reasoning behind it.

## Core completion target

If you feel overwhelmed, prioritize:

1. trace stack vs queue correctly;
2. complete `ArrayStack<T>`;
3. understand why `ListQueue<T>` can be inefficient;
4. complete `RingQueue<T>`;
5. run the tests and answer the four comparison questions.

## Optional extensions

- Add a `Clear` method while preserving invariants.
- Count how many element assignments `ListQueue<T>` and `RingQueue<T>` perform during 1,000 dequeues.
- In the Unity laboratory, inspect how the working game-facing demo uses the standard library `Stack<T>` and `Queue<T>`.
- Replace one standard collection in a personal Unity copy with your completed structure and verify behavior.
