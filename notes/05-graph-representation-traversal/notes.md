# Lab 5 - Graph Representation and Traversal

## Task 1 - Read the graph

The dungeon is a directed graph. Each room is a node, and each arrow is a one-way edge. A reverse connection exists only if it is explicitly shown.

### Nodes and directed edges

There are 7 nodes: Entrance, Hall, StoreRoom, Armory, Courtyard, BossRoom, and SecretRoom.

The 8 directed edges are:

```text
Entrance -> Hall
Entrance -> StoreRoom
Hall -> Armory
Hall -> Courtyard
StoreRoom -> Courtyard
Armory -> BossRoom
Courtyard -> BossRoom
Courtyard -> Hall
```

SecretRoom is disconnected: it has no incoming or outgoing edges.

### Answers

- **Where is the cycle?** `Hall -> Courtyard -> Hall`. Following these two directed edges returns to the starting room.
- **Can Entrance reach BossRoom?** Yes. One valid path is `Entrance -> Hall -> Armory -> BossRoom`.
- **Can Entrance reach SecretRoom?** No. SecretRoom has no incoming edges, so no path from Entrance leads to it.
- **Can BossRoom reach Entrance?** No. BossRoom has no outgoing edges, so it cannot move to another room.

## Task 2 - Inspect the edge-list baseline

`EdgeListGraph` stores every directed edge in one global list. To find the outgoing neighbors of `Hall`, `GetNeighbors("Hall")` scans that list and keeps the destinations of edges whose `From` is `Hall`. In the dungeon graph, those edges are `Hall -> Armory` and `Hall -> Courtyard`, so the result is `Armory, Courtyard`.

The method checks all `E` edges for each call, giving `GetNeighbors(node)` a time cost of **O(E)**, even when the node has only a few outgoing edges or none. This implementation is correct. A traversal repeatedly asks for neighbors, so scanning unrelated edges each time motivates grouping outgoing neighbors by node in Task 3.

## Task 3 - Prepare adjacency

`AdjacencyGraph` creates an empty neighbor list for every supplied node, then processes each directed edge by appending its destination to the list for the source node. This keeps outgoing neighbors in input order and retains nodes with no outgoing edges.

```text
Entrance   -> [Hall, StoreRoom]
Hall       -> [Armory, Courtyard]
StoreRoom  -> [Courtyard]
Armory     -> [BossRoom]
Courtyard  -> [Hall, BossRoom]
BossRoom   -> []
SecretRoom -> []
```

Building the dictionary and grouping the edges takes **O(V + E)** time and **O(V + E)** additional storage. After preparation, finding the neighbor list for a node is an expected **O(1)** dictionary lookup. Processing its neighbors takes **O(degree(v))** time. This prepares the graph once for the repeated neighbor lookups needed by traversal.

## Task 4 - Breadth-First Search

`BreadthFirstOrder` uses a `Queue<string>` to explore the graph one layer at a time. A `HashSet<string>` records discovered nodes, and a list records the order in which nodes are removed from the queue. The start node is marked visited and enqueued before the loop. Each loop iteration removes one node, adds it to the result, and enqueues neighbors that have not been visited.

Starting at `Entrance` produces this order:

```text
Entrance -> Hall -> StoreRoom -> Armory -> Courtyard -> BossRoom
```

Neighbors are marked visited when they are enqueued. This prevents the same node from entering the queue through multiple routes and stops the `Hall -> Courtyard -> Hall` cycle from repeating. `SecretRoom` is not visited because no path from `Entrance` reaches it. An unknown start node returns an empty order.

With the adjacency representation, BFS inspects each reachable node and outgoing edge at most once, giving **O(V + E)** time. The visited set, queue, and result each use up to **O(V)** space.
