# Lab 6 - Weighted Shortest Paths: Dijkstra → A*

## Task 1 - Understand the graph representation

The graph is **weighted**: each edge has a cost. It is also **directed**: `A -> B` does not automatically allow `B -> A`.

### Nodes and edges

`WeightedNode` represents a node with an `Id`, such as A. It also stores coordinates (`X`, `Y`), which are useful later for A*. Dijkstra only needs the ID.

`WeightedEdge` stores:

- `From`: the node we leave.
- `To`: the node we travel to.
- `Cost`: the cost of that step.

For example, `A -> B : 5` means travelling from A to B costs 5. Costs can be zero but cannot be negative.

### How the graph is stored

`WeightedGraph` uses two dictionaries:

- `_nodesById`: node ID -> node information.
- `_outgoingByNode`: node ID -> list of edges leaving that node.

Grouping outgoing edges by node is called an **adjacency list**. The constructor prepares these lists when the graph is created.

For example, `graph.GetOutgoing("A")` returns:

```text
A -> B : 5
A -> C : 2
A -> G : 20
```

This lets us find A's possible next steps directly, without scanning every edge in the graph.

### What Dijkstra needs from it

When processing a node, we use `GetOutgoing(current)`. For each returned edge, `edge.To` identifies a neighbor and `edge.Cost` tells us the cost of reaching it from the current node.

`ContainsNode(id)` checks whether a node exists. A node can exist without outgoing edges: X is in the graph, but `GetOutgoing("X")` returns an empty list.
