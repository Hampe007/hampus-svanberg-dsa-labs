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
