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

## Task 2 - Understand the supplied frontier

The **frontier** holds candidates waiting to be processed. BFS uses a queue to process the oldest candidate first. Dijkstra uses `MinFrontier<T>` to process the candidate with the smallest known total cost first.

### Operations and entries

- `Enqueue(value, priority)` adds a candidate with a priority.
- `Dequeue()` removes and returns the entry with the smallest priority.
- `Count` tells us how many entries are waiting. Calling `Dequeue()` when empty throws an exception.

Each returned entry contains `Value` and `Priority`. In Dijkstra, the value is a node ID and the priority is the best-known total cost from the start, rather than just the cost of one edge.

For example:

```csharp
frontier.Enqueue("B", 5);
frontier.Enqueue("C", 2);
frontier.Enqueue("G", 20);

var entry = frontier.Dequeue();
// entry.Value is "C", entry.Priority is 2.
```

C is removed first even though B was added first. B and G remain in the frontier.

### How it works

The supplied frontier uses a **binary min-heap**, which keeps the smallest priority at the root. Adding and removing entries takes O(log n) time, where n is the number of queued entries. Reading `Count` takes O(1).

It has no decrease-key operation to change an existing entry's priority. A cheaper route is enqueued as a new entry; handling the old entry is covered in Task 5.
