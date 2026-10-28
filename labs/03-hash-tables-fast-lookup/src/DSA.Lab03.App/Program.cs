using System;
using DSA.Lab03;

Console.WriteLine("DSA Lab 3 - Hash Tables and Fast Lookup");
Console.WriteLine();

var linear = new LinearIdMap<string>();
linear.Set(101, "Harbor");
linear.Set(205, "Relay");
linear.Set(734, "North Gate");

Console.WriteLine(linear.TryGetValue(734, out string? location)
    ? $"Linear lookup found: {location}"
    : "Not found");

Console.WriteLine();
Console.WriteLine("Complete IntHashTable<TValue>, then run the tests and collision examples.");
