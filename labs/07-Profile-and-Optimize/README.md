# Lab 7 — Profile and Optimize

## Purpose
This lab is about **evidence-based optimization**, not collecting tricks.

Use this loop:

```text
baseline → measure → identify repeated work → change one thing → measure again → explain
```

The supplied workload repeatedly checks whether each enemy ID is active. The baseline is intentionally correct but uses a representation that can make the repeated membership operation expensive.

## Run
```bash
dotnet restore DSA.Lab07.sln
dotnet test DSA.Lab07.sln
dotnet run --project src/DSA.Lab07.App
```

## Task
1. Run tests. Keep correctness green.
2. Run the app several times and record the workload, elapsed time and counters.
3. Inspect `ThreatScanner.cs`. Identify the repeated operation.
4. Make **one justified representation change** that targets that operation.
5. Run the same tests and workload again.
6. Write 4–6 sentences: baseline, change, evidence, trade-off.

### Hint
Ask whether `activeEnemyIds` is organized for the operation you perform thousands of times.

## Important
- Do not optimize unrelated arithmetic.
- Do not change the workload between before/after measurements.
- A smaller elapsed time once is not strong evidence by itself.
- Operation counts can explain *why* a change helps even when timings vary.

## Assignment bridge
Frontier Dispatch asks for before/after performance evidence. Transfer the **measurement process**, not this lab's domain or class names.
