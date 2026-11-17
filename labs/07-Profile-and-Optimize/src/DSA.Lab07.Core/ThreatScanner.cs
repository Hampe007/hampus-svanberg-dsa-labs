using System;
using System.Collections.Generic;

namespace DSA.Lab07
{
    public sealed class ThreatScanner
    {
        public ScanResult Scan(IReadOnlyList<Enemy> enemies, IReadOnlyList<int> activeEnemyIds, int repetitions)
        {
            if (enemies == null) throw new ArgumentNullException(nameof(enemies));
            if (activeEnemyIds == null) throw new ArgumentNullException(nameof(activeEnemyIds));
            if (repetitions < 0) throw new ArgumentOutOfRangeException(nameof(repetitions));

            long visited = 0;
            long membershipChecks = 0;
            long totalThreat = 0;

            for (int r = 0; r < repetitions; r++)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    visited++;
                    Enemy e = enemies[i];
                    bool isActive = false;

                    for (int j = 0; j < activeEnemyIds.Count; j++)
                    {
                        membershipChecks++;
                        if (activeEnemyIds[j] == e.Id)
                        {
                            isActive = true;
                            break;
                        }
                    }

                    if (isActive) totalThreat += e.Threat;
                }
            }

            return new ScanResult(totalThreat, visited, membershipChecks);
        }
    }
}
