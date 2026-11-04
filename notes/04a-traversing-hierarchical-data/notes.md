# Lab 4A - Traversing Hierarchical Data

## Task 1 - Read the hierarchy

```text
Survival
|- Movement
|  |- Dash
|  `- Climb
`- Combat
   |- Sword
   `- Bow
```

- **Root:** `Survival`, the top node.
- **Parent:** `Movement` is the parent of `Dash` and `Climb`.
- **Child:** `Dash` is a child of `Movement`.
- **Leaves:** `Dash`, `Climb`, `Sword`, and `Bow`, because they have no children.
- **Subtree rooted at Movement:** `Movement` together with its children `Dash` and `Climb`.
- **Depth of Dash:** 2, because the path `Survival -> Movement -> Dash` has two edges. The root has depth 0.

## Task 2 - Recognize the recursive shape

Each `HierarchyNode<T>` stores a `Value` of type `T` and a collection of `Children`. Each child is also a `HierarchyNode<T>`, so it has its own value and children. A tree is therefore one node plus zero or more child trees.

For example, `Survival` has the child `Movement`, and `Movement` has the children `Dash` and `Climb`. The same node structure is repeated at every level, which makes recursion a natural way to work through the hierarchy.

A recursive algorithm can:

1. Do the work for the current node.
2. Call the same algorithm for each child subtree.
3. Combine or return the results.

For example, counting the nodes under `Movement` means counting `Movement` itself, then counting the subtrees rooted at `Dash` and `Climb`: `1 + 1 + 1 = 3`.

A leaf has no children (`IsLeaf` is true). Recursion naturally stops there because there are no child calls to make, and the leaf returns its own result to its parent.

## Task 3 - Implement CountNodes

Before coding, trace this hierarchy:

```text
A
|- B
`- C
```

1. `CountNodes(A)` starts with 1 for A itself.
2. `CountNodes(B)` returns 1 because B has no children. A's count becomes 2.
3. `CountNodes(C)` returns 1 because C has no children. A's count becomes 3.
4. `CountNodes(A)` returns 3.

The implementation starts each call with `count = 1`, then adds `CountNodes(child)` for every child subtree. A leaf returns 1 naturally because its loop has no iterations. A null root throws `ArgumentNullException`.

Every node is counted once, so the time complexity is O(n). Recursive calls use O(h) additional call-stack space, where h is the tree height.

## Task 4 - Implement Contains

`Contains` checks whether any node in the hierarchy has a value equal to the target.

1. Compare the current node's value with the target using `EqualityComparer<T>.Default`. This uses the normal equality rules for T without assuming a specific type.
2. If the value matches, return `true` immediately.
3. Otherwise, recursively search each child subtree. If any child call returns `true`, return `true` immediately.
4. Return `false` only after every child subtree has been searched without a match.

For example, searching for `Bow` checks `Survival`, then `Movement`, `Dash`, `Climb`, `Combat`, `Sword`, and finally `Bow`. Finding `Bow` returns `true` back through `Combat` to `Survival`. Searching for `Magic` checks all seven nodes and returns `false`.

A leaf either matches and returns `true`, or has no children to search and returns `false`. A null root throws `ArgumentNullException`.

The best case is O(1) when the root matches. The worst case is O(n) because every node may need to be checked. Recursive calls use O(h) additional call-stack space, where h is the tree height. These time costs assume each equality comparison takes constant time.

## Task 5 - Depth-first pre-order traversal

`DepthFirstPreOrder` returns the values in the hierarchy by visiting the current node before its children. Each child subtree is visited completely before moving to the next sibling, from left to right.

1. Create a list to collect the visited values and pass it to the recursive helper `VisitPreOrder`.
2. Add the current node's value to the list.
3. Recursively visit each child subtree in the order stored in `Children`, using the same list.
4. Return the collected values as an `IReadOnlyList<T>` after all nodes have been visited.

For example, trace this hierarchy:

```text
A
|- B
|  `- D
`- C
```

The traversal visits `A`, then `B`, then `D`, and finally `C`. The result is `A, B, D, C`, because B's subtree is completed before moving to C. For the skill hierarchy, the result is `Survival, Movement, Dash, Climb, Combat, Sword, Bow`.

A leaf adds its own value but has no children to visit, so the recursive call returns naturally. A null root throws `ArgumentNullException`.

Every node is visited once, so the time complexity is O(n). The returned list uses O(n) space, and recursive calls use O(h) additional call-stack space, where h is the tree height. Sharing one list avoids copying the results of each child subtree into separate lists.

## Task 6 - Implement MaxDepth

`MaxDepth` finds the greatest number of edges from the root to any leaf. A single root has depth 0, a root with one child has maximum depth 1, and a root with a grandchild has maximum depth 2.

1. Start with `maxDepth = 0` for the current node.
2. Recursively find the maximum depth of each child subtree using `MaxDepth(child)`.
3. Add 1 for the edge from the current node to that child.
4. Use `Math.Max` to keep the greatest child depth and return it after checking every child subtree.

For example, the skill hierarchy has maximum depth 2. Adding `Air Dash` as a child of `Dash` creates the path `Survival -> Movement -> Dash -> Air Dash`, which has three edges. `Air Dash` returns 0, `Dash` returns 1, `Movement` returns 2, and `Survival` returns 3. The shorter Combat branch does not change the result because only the deepest branch determines the maximum depth.

A leaf returns 0 naturally because it has no children and its loop has no iterations. A null root throws `ArgumentNullException`.

Every node is visited once, so the time complexity is O(n). Recursive calls use O(h) additional call-stack space, where h is the tree height.

## Task 7 - Reason about recursion

Each recursive method solves the problem for the current node and delegates the same problem to each child subtree. Each child call works on a smaller part of the hierarchy.

1. `CountNodes` starts with 1 for the current node, then asks each child subtree for its node count. A leaf has no children, so it returns 1. Each parent adds the returned counts and returns the total for its subtree.
2. `Contains` compares the current value with the target, then asks each child subtree whether it contains the target. A match returns `true` immediately, skipping any remaining children. A leaf without a match returns `false`, and a parent returns `false` only if none of its child subtrees contains the target.
3. `DepthFirstPreOrder` creates the result list, and its recursive helper `VisitPreOrder` adds the current value before visiting each child subtree. A leaf adds its value and returns because it has no children. The helper does not return a value; it accumulates values in the shared list, which the public method returns after traversal.
4. `MaxDepth` starts with depth 0 and asks each child subtree for its maximum depth. A leaf returns 0. Each parent adds 1 for the edge to each child and returns the greatest depth found.

Recursive calls use the program's call stack. When a method calls itself for a child, the parent call pauses while the child call runs. Once the child returns, the parent continues from where it paused, using the returned result or moving to the next child. This follows last-in, first-out order, like the stack from Lab 2, without manually using `Stack<T>`.

For example, `CountNodes(Survival)` pauses while `CountNodes(Movement)` runs, and that call pauses while `CountNodes(Dash)` runs. Dash is a leaf and returns 1, allowing Movement to continue with Climb before returning its total to Survival.

The number of active recursive calls depends on the tree height h, so additional call-stack space is O(h). A broad, shallow tree uses fewer active calls than a long chain with the same number of nodes. A very deep hierarchy can exhaust the call stack and cause a stack overflow.

## Task 8 - Add one meaningful test

`DepthFirstPreOrder_PreservesDuplicateValues` checks that separate nodes with equal values are all included in the traversal result.

The test builds this hierarchy:

```text
Root
|- Skill
|  `- Upgrade
`- Skill
   `- Upgrade
```

The expected result is `Root, Skill, Upgrade, Skill, Upgrade`. Each Skill node and each Upgrade node must appear, even though their values are equal. The exact sequence also checks that the first child subtree is completed before moving to the second.

The supplied traversal tests use unique values, so they would not catch an implementation that incorrectly skips nodes whose values have already been visited. This test protects the assumption that traversal processes every node, including nodes with duplicate values.

All 11 tests pass, including the new duplicate-value test.
