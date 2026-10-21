# Lab 1 — Progressive Hints

Use this file when you understand the task but cannot see the next coding step.

**Do not read every hint immediately.** Try Hint 1, return to your code, and only continue if you are still blocked. Discussing a hint with a classmate is encouraged.

---

## Binary Search

### Hint 1 — What must remain true?

At any point, if the target exists, it must be somewhere between two indexes: `low` and `high`.

After you inspect the middle value, one half can no longer contain the target. Your next range must therefore be smaller than the previous range.

### Hint 2 — What state do you need?

Start with:

```text
low = first valid index
high = last valid index
```

Repeat while the range is still valid. Each iteration needs:

```text
middle = an index halfway between low and high
middleValue = value stored at middle
```

Then decide whether you are done, must search strictly to the right, or must search strictly to the left.

### Hint 3 — Pseudocode structure

```text
low = 0
high = length - 1

while low <= high
    middle = low + (high - low) / 2

    if value at middle equals target
        return middle

    if value at middle is smaller than target
        low = middle + 1
    otherwise
        high = middle - 1

return not-found
```

Before translating this to C#, trace it with `[2, 5, 8, 12, 16, 23, 38]`, target `23`.

---

## Insertion Sort

### Hint 1 — Think in two regions

Treat the left side of the array as already sorted. Take the next value and insert it into the correct position in that sorted region.

For `[7, 2, 5, 1]`, begin by treating `[7]` as sorted. Your first value to insert is `2`.

### Hint 2 — What state do you need?

For each position starting at index `1`:

- remember the value you want to insert;
- look left from the position immediately before it;
- while the left value is larger, shift that value one place to the right;
- when shifting stops, put the remembered value into the gap.

### Hint 3 — Pseudocode structure

```text
for each index i from 1 to the end
    valueToInsert = values[i]
    j = i - 1

    while j is a valid index AND values[j] > valueToInsert
        move values[j] one position right
        decrease j

    place valueToInsert at j + 1
```

Trace the array after every outer-loop iteration before translating the pseudocode to C#.

---

## Still stuck?

Bring a tiny concrete example when you ask for help:

> “I am on Binary Search. With values `[2, 5, 8]` and target `8`, my `low/high/middle` become __. I expected __, but I get __.”

That gives a classmate or teacher something specific to investigate with you.
