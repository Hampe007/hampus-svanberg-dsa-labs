namespace DSA.Lab07
{
    public readonly struct ScanResult
    {
        public ScanResult(long totalThreat, long enemiesVisited, long membershipChecks)
        {
            TotalThreat = totalThreat;
            EnemiesVisited = enemiesVisited;
            MembershipChecks = membershipChecks;
        }

        public long TotalThreat { get; }
        public long EnemiesVisited { get; }
        public long MembershipChecks { get; }
    }
}
