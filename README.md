# GP26 — Data Structures and Algorithms Labs

This repository is for your **ungraded course labs** in Futuregames Data Structures and Algorithms.

You will keep using the **same repository throughout the course**. New lab starter folders will be released during the course and can be added under `labs/`.

The graded individual assignment is separate and will use its own repository.

## Recommended repository name

`gp26-dsa-labs`

## First-time setup

1. Create a new empty GitHub repository, for example `gp26-dsa-labs`.
2. Download this starter package.
3. Extract the contents directly into your repository root.
4. Open a terminal in the repository root.
5. Check your .NET installation:

```bash
dotnet --info
```

6. Build and test Lab 0:

```bash
dotnet restore labs/00-problem-solving-workflow/DSA.Lab00.sln
dotnet build labs/00-problem-solving-workflow/DSA.Lab00.sln
dotnet run --project labs/00-problem-solving-workflow/src/DSA.Lab00.App
dotnet test labs/00-problem-solving-workflow/tests/DSA.Lab00.Tests
```

7. Make an initial Git commit before you begin changing the lab.

## Course workflow

A useful development loop for this course is:

**Understand → Predict → Plan → Implement → Test → Inspect → Commit → Continue**

You are encouraged to discuss lab problems and work together. Pair programming is welcome, but working independently is equally valid.

## Reference implementations

The teacher course repository contains completed/reference versions of the labs.

Try to solve each task yourself first. If you use a reference implementation to get unstuck, compare it against your own work and make sure you understand:

- what changed;
- why it works;
- which condition or invariant it depends on.

The point of the labs is understanding, not merely producing completed code.

## Structure

```text
gp26-dsa-labs/
├── README.md
├── .gitignore
├── labs/
│   └── 00-problem-solving-workflow/
│       ├── README.md
│       ├── DSA.Lab00.sln
│       ├── src/
│       └── tests/
└── notes/
```

Future lab folders will be added under `labs/`.
