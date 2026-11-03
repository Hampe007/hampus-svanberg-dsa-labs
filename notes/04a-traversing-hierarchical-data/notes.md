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