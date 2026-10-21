using System;
using System.Diagnostics;
using System.Linq;
using DSA.Lab01;

Console.WriteLine("DSA Lab 1 - Search, Sort and Scaling");
Console.WriteLine();

int[] sorted = { 2, 5, 8, 12, 16, 23, 38, 41, 56, 72, 91 };
int target = 41;

Console.WriteLine($"Linear search for {target}: index {SearchAlgorithms.LinearSearch(sorted, target)}");
Console.WriteLine($"Binary search for {target}: index {SearchAlgorithms.BinarySearch(sorted, target)}");

int[] unsorted = { 7, 2, 9, 1, 5, 4 };
Console.WriteLine();
Console.WriteLine("Before insertion sort: " + string.Join(", ", unsorted));
SortAlgorithms.InsertionSort(unsorted);
Console.WriteLine("After insertion sort:  " + string.Join(", ", unsorted));

Console.WriteLine();
Console.WriteLine("Rough search timing (run several times; exact numbers will vary):");
RunBenchmark(10_000);
RunBenchmark(100_000);
RunBenchmark(1_000_000);

static void RunBenchmark(int count)
{
    int[] values = Enumerable.Range(0, count).ToArray();
    int missingTarget = -1;

    // Repeat enough times to make the difference easier to observe.
    int repetitions = count <= 10_000 ? 500 : count <= 100_000 ? 100 : 10;

    Stopwatch sw = Stopwatch.StartNew();
    for (int i = 0; i < repetitions; ++i)
    {
        SearchAlgorithms.LinearSearch(values, missingTarget);
    }
    sw.Stop();
    double linearMicroseconds = sw.Elapsed.TotalMicroseconds / repetitions;

    sw.Restart();
    for (int i = 0; i < repetitions; ++i)
    {
        SearchAlgorithms.BinarySearch(values, missingTarget);
    }
    sw.Stop();
    double binaryMicroseconds = sw.Elapsed.TotalMicroseconds / repetitions;

    Console.WriteLine($"n={count,9:N0} | linear ~{linearMicroseconds,10:F2} us | binary ~{binaryMicroseconds,10:F2} us");
}
