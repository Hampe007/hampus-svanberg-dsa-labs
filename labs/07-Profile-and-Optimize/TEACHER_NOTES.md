# Teacher Notes — Lab 7

The intended improvement is to prepare a `HashSet<int>` of active IDs once per scan and use expected O(1) membership inside the repeated loop. Do not require a specific speedup because machines vary. Correctness + same workload + reduced repeated membership work + sensible interpretation is the goal.
