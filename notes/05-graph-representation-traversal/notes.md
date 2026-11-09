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

The method checks all `E` edges for each call, giving `GetNeighbors(node)` a time cost of **O(E)**, even when the node has only a few outgoing edges or none. This is correct, but a traversal repeatedly asks for neighbors, repeatedly scanning unrelated edges motivates grouping outgoing neighbors by node in Task 3.
