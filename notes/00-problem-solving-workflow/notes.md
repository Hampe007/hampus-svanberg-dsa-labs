# Lab 00 - Problem-Solving Workflow

## Understanding FindById
`FindById` goes through the players one at a time, comparing each players ID with the supplied ID. It returns the first matching player.
In the worst case, it checks every player. If the ID does not exist, it returns `null`.

## From failing tests to implementations

My initial run had six tests: four passed and two failed (which was to be expected). I saved the output in commit `ad57b89`.

### FindHighestScore

The test expected a player but received `null`. Looking at the implementation showed that the method still needed the logic for finding the highest score.

I chose to start with the first player and keep track of the highest-scoring player while scanning the collection. Whenever a higher score appears, that player becomes the current choice.

Starting with an actual player avoids assuming that scores are positive. There is no need to sort the collection because I only need the highest-scoring player.

For an empty collection, the method returns `null`.

Committed in `e45e1e8` -> `Implement FindHighestScore`.

### ContainsName

The test searched for `"Bo"`, which existed in the collection, but received `false`. The method needed to check the players names instead of always returning `false`.

I chose LINQs `Any` because I only need to know whether a matching player exists:

    return _players.Any(player => player.Name == name);

The comparison is case-sensitive, as was mentioned in the requirements. `Any` stops when it finds a match and returns `false` if none exists.

Committed in `f78fa0d` -> `Implement case-sensitive player name lookup`.

## Extra test

I added `FindHighestScore_ReturnsOnlyPlayer` to check a collection containing exactly one player.
`Is.SameAs(player)` checks that the returned result is the original player instance.

I chose this edge case to verify that the method works without needing another player to compare against.

Committed in `a5d71c9` -> `A test to cover FindHighestScore with a single player`.

## Testing and debugging notes

After the implementations and extra test, all seven tests passed through `dotnet test`.

## Complexity and checkpoint answers

### Which operations may inspect every player?

All three. `FindById` and `ContainsName` may scan the entire collection before finding a match or deciding none exists. `FindHighestScore` must inspect every player.

Their worst-case time complexity is O(n).

### What if the collection becomes 100 times larger?

All three may perform roughly 100 times as much work in the worst case.

ID and name searches can still stop early if a match appears near the beginning.

### How could frequent ID searches be improved?

A `Dictionary<int, Player>` keyed by unique player ID would provide average O(1) lookup time.

### Why were these commit checkpoints sensible?

I committed the highest-score implementation, name lookup, and extra test separately. Each commit recorded one focused change.

The final combined implementation was verified through the successful command-line test run.
