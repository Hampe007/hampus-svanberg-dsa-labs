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

It has no decrease-key operation to change an existing entry's priority. A cheaper route is enqueued as a new entry. The old entry remains in the frontier.

## Task 3 - Track best-known total cost

`bestCost` stores the cheapest total cost discovered so far for each node, measured from the start. A cost can improve when we find another route.

The supplied code creates the dictionary and sets the starting cost:

```csharp
var bestCost = new Dictionary<string, int>();
bestCost[start] = 0;
```

Reaching the start costs nothing. A node missing from the dictionary has no known route yet. Its cost is not zero.

### Calculate and compare a candidate

When considering an outgoing edge, we add its cost to the known cost of reaching the current node:

```csharp
int candidate = bestCost[current] + edge.Cost;
```

For example, if reaching C costs 2 and `C -> B` costs 2, reaching B through C would cost 4. If B's current best cost is 5, this candidate is cheaper.

We can check whether the destination already has a known cost with:

```csharp
bool hasKnownCost = bestCost.TryGetValue(edge.To, out int knownCost);
```

The candidate is an improvement if `!hasKnownCost || candidate < knownCost`. An equal or higher cost does not improve the route. Updating the dictionary and frontier is called relaxation.

## Task 4 - Relax an edge

**Relaxation** means checking whether an edge gives us a cheaper route to its destination.

We remove the smallest-priority entry from the frontier and look at the current node's outgoing edges. For each edge:

```csharp
int candidate = bestCost[current] + edge.Cost;

if (!bestCost.TryGetValue(edge.To, out int knownCost) || candidate < knownCost)
{
    bestCost[edge.To] = candidate;
    cameFrom[edge.To] = current;
    frontier.Enqueue(edge.To, candidate);
}
```

If the destination has no known cost, or the candidate is cheaper, we update three things:

- `bestCost`: the improved total cost.
- `cameFrom`: the node we reached it from.
- The frontier: a new entry with the improved cost as its priority.

Equal or more expensive candidates are ignored. Being discovered earlier does not prevent a node from getting a cheaper route.

### Example: improving B

After processing A, the known costs and waiting entries are:

```text
bestCost: A=0, B=5, C=2, G=20
frontier (by priority): C:2, B:5, G:20
```

C is processed next. Its edge to B gives `2 + 2 = 4`, improving B from 5 to 4. Its edge to D gives `2 + 6 = 8`.

```text
bestCost: A=0, B=4, C=2, D=8, G=20
cameFrom[B] = C
frontier (by priority): B:4, B:5, D:8, G:20
```

The frontier now contains both B:4 and B:5, but bestCost[B] holds the improved cost of 4.
