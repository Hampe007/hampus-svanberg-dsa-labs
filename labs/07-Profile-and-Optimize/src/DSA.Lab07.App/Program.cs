using System;
using System.Diagnostics;
using DSA.Lab07;

internal static class Program
{
    private static void Main()
    {
        var enemies = DemoData.Enemies(5000);
        var active = DemoData.ActiveIds(5000, 3);
        var scanner = new ThreatScanner();

        for (int i = 0; i < 2; i++)
        {
            scanner.Scan(enemies, active, 10);
        }

        var sw = Stopwatch.StartNew();
        ScanResult r = scanner.Scan(enemies, active, 40);
        sw.Stop();

        Console.WriteLine($"Threat sum: {r.TotalThreat}");
        Console.WriteLine($"Enemies visited: {r.EnemiesVisited:n0}");
        Console.WriteLine($"Membership comparisons/checks: {r.MembershipChecks:n0}");
        Console.WriteLine($"Elapsed: {sw.Elapsed.TotalMilliseconds:F2} ms");
        Console.WriteLine("Run several times; compare same workload before/after.");
    }
}
