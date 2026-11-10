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

## Task 5 - Depth-First Search

`DepthFirstOrder` uses a `Stack<string>` as its frontier and a `HashSet<string>` to track discovered nodes. It marks the start visited, pushes it, then repeatedly pops a node and adds it to the result. Newly discovered neighbors are marked visited when pushed, so cycles and multiple routes do not add duplicate visits.

The adjacency lists keep neighbors in input order. Because a stack removes the most recently pushed item first, DFS examines each neighbor list from the final item to the first item when pushing. This makes the first listed neighbor the next node processed. Starting at `Entrance` gives:

```text
Entrance -> Hall -> Armory -> BossRoom -> Courtyard -> StoreRoom
```

DFS explores a branch before returning to a later neighbor. `SecretRoom` stays outside this traversal because it is disconnected. An unknown start node returns an empty order. With adjacency lists, DFS takes **O(V + E)** time and up to **O(V)** space for the visited set, stack, and result.

## Task 6 - Why visited is essential

The directed edges `Hall -> Courtyard` and `Courtyard -> Hall` form a cycle. Without a visited set, a traversal could keep adding `Hall` and `Courtyard` to its frontier and never finish:

```text
Hall -> Courtyard -> Hall -> Courtyard -> ...
```

Different routes can also reach the same node. For example, `Courtyard` can be reached from `Hall` or `StoreRoom`. The `HashSet<string>` answers whether a node has already been discovered. BFS and DFS mark each node visited as soon as it enters the queue or stack. If `visited.Add(neighbor)` returns false, that node is already scheduled or processed, so it is not added again. This keeps each reachable node in the frontier at most once and allows traversal to finish even when the graph has cycles.

## Task 7 - Reachability with early exit

`IsReachableBfs` uses a queue and visited set to explore from the start node. When it removes the target from the queue, it returns `true` immediately. If the queue becomes empty first, it returns `false`. It does not build a traversal order because the answer only needs a yes or no result.

For the dungeon graph, `Entrance` can reach `BossRoom`, while it cannot reach the disconnected `SecretRoom`. A node can reach itself, so a valid start that equals the target returns `true` on the first dequeue. If either node is missing from the graph, the method returns `false`.

The search may stop before visiting every reachable node. In the worst case it still takes **O(V + E)** time and **O(V)** space for the queue and visited set.

## Task 8 - Add one meaningful test

`BreadthFirstOrder_IsolatedNodeReturnsOnlyItself` starts BFS at `SecretRoom`, which has no outgoing edges. The expected result is just `SecretRoom`.

This checks that BFS includes the starting node even when it cannot reach any other room.

All 14 tests pass, with 0 failures and 0 skipped tests.
