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
