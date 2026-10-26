# Lab 2 Progressive Hints

Use the least specific hint that gets you moving again.

## `ArrayStack<T>`

### Hint 1
`Count` is also the index where the **next** pushed item belongs.

### Hint 2
The current top is one position before `Count`.

### Hint 3
For `Pop`: check empty -> decrement count -> read the old top slot -> clear that slot -> return the value.

## `RingQueue<T>`

### Hint 1
Do not move the stored values when you enqueue or dequeue. Move the **indices**.

### Hint 2
`tail` tells you where to write next. `head` tells you where to read next.

### Hint 3
After using either index, advance it with:

```csharp
(index + 1) % Capacity
```

Update `count` separately. Full and empty are determined by `count`, not by whether `head == tail`.

## Debugging a wrap-around bug

Draw five slots numbered 0-4. Trace this exact sequence by hand:

```text
Enqueue A
Enqueue B
Enqueue C
Dequeue
Dequeue
Enqueue D
Enqueue E
Enqueue F
```

After every line, write `head`, `tail`, and `count`.
