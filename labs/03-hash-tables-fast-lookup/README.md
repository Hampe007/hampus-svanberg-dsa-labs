# Lab 3 - Hash Tables and Fast Lookup

## What this lab is for

This lab explores how a key can guide us toward a small part of the stored data instead of scanning everything.

You will compare:

- a working linear ID lookup;
- a small integer-key hash table with separate chaining;
- standard `Dictionary<TKey,TValue>` / `HashSet<T>` as the production tools you would normally reach for.

The goal is **not** to recreate the full .NET Dictionary implementation. The goal is to understand buckets, collisions, lookup behavior, and trade-offs.

## Core path

1. Inspect the working `LinearIdMap<TValue>`.
2. Trace the bucket function by hand.
3. Implement `Set`, `TryGetValue`, and `ContainsKey` in `IntHashTable<TValue>`.
4. Make the collision tests pass.
5. Add one meaningful test.
6. Explain when you would use a standard Dictionary or HashSet in real game code.

## Run the lab

```bash
dotnet restore labs/03-hash-tables-fast-lookup/DSA.Lab03.sln
dotnet build labs/03-hash-tables-fast-lookup/DSA.Lab03.sln
dotnet run --project labs/03-hash-tables-fast-lookup/src/DSA.Lab03.App
dotnet test labs/03-hash-tables-fast-lookup/tests/DSA.Lab03.Tests
```

## Task 1 - Inspect linear lookup

`LinearIdMap<TValue>` stores entries in one `List<Entry<TValue>>` and scans them during lookup.

Ask:

- What is the worst case when the key is missing?
- What changes when the collection grows from 100 entries to 100,000?
- What operation are we trying to improve?

## Task 2 - Understand buckets

The supplied hash table uses positive integer keys and:

```csharp
bucketIndex = key % bucketCount;
```

With five buckets, calculate the bucket for:

```text
101, 205, 734, 206, 999
```

Which keys collide?

A collision is **not an error**. It means several keys map to the same bucket and the table needs a way to distinguish them inside that bucket.

## Task 3 - Implement `IntHashTable<TValue>`

This lab uses **separate chaining**: every bucket contains a small list of entries.

Implement:

- `Set(int key, TValue value)` - insert a new key or replace its existing value;
- `TryGetValue(int key, out TValue value)`;
- `ContainsKey(int key)`.

The bucket function is already supplied. Keep the implementation small and readable.

## Task 4 - Reason about collisions

Run the collision tests and answer:

1. Why can keys 1 and 6 both exist when the table has five buckets?
2. What happens to lookup cost if almost every key ends up in the same bucket?
3. Why does expected O(1) lookup not mean "exactly one CPU operation"?

## Task 5 - Add one meaningful test

Examples:

- replacing an existing value does not increase `Count`;
- several keys collide in the same bucket;
- a missing colliding key still returns false;
- zero as a key;
- another case you can justify.

## Standard collections

After completing the lab, compare your implementation with:

```csharp
Dictionary<int, TValue>
HashSet<int>
```

In real application/game code, prefer the standard library unless you have a specific reason not to. We build a small hash table here because seeing the representation makes dictionaries and sets easier to reason about.

## Unity Bridge

The **Frontier Dispatch** Hash Lookup scene contains many entities with integer IDs. You can compare a visible linear scan with a dictionary lookup.

The Unity scene is an application/visualization layer. Completing the headless lab is sufficient for the course exercise.
