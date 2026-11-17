using System.Collections.Generic;
using NUnit.Framework;
using DSA.Lab07;

namespace DSA.Lab07.Tests
{
    public sealed class ThreatScannerTests
    {
        [Test]
        public void Scan_ReturnsCorrectThreatSum()
        {
            var e = new List<Enemy>
            {
                new Enemy(1, 4),
                new Enemy(2, 7),
                new Enemy(3, 10)
            };
            var a = new List<int> { 1, 3 };
            var r = new ThreatScanner().Scan(e, a, 2);

            Assert.That(r.TotalThreat, Is.EqualTo(28));
        }

        [Test]
        public void Scan_VisitsEveryEnemyPerRepetition()
        {
            var r = new ThreatScanner().Scan(
                DemoData.Enemies(10),
                DemoData.ActiveIds(10, 2),
                3);

            Assert.That(r.EnemiesVisited, Is.EqualTo(30));
        }

        [Test]
        public void Scan_ZeroRepetitions_DoesNoWork()
        {
            var r = new ThreatScanner().Scan(
                DemoData.Enemies(10),
                DemoData.ActiveIds(10, 2),
                0);

            Assert.That(r.TotalThreat, Is.Zero);
            Assert.That(r.EnemiesVisited, Is.Zero);
        }
    }
}
