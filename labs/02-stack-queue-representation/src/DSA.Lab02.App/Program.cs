using System;
using DSA.Lab02;

Console.WriteLine("DSA Lab 2 - Stack, Queue and Representation");
Console.WriteLine();

Console.WriteLine("Working baseline: ListQueue<T>");
var jobs = new ListQueue<string>();
jobs.Enqueue("Deliver medicine");
jobs.Enqueue("Repair relay");
jobs.Enqueue("Survey road");
while (jobs.Count > 0)
{
    Console.WriteLine($"Process: {jobs.Dequeue()}");
}

Console.WriteLine();
Console.WriteLine("Complete ArrayStack<T> and RingQueue<T>, then run the tests.");
