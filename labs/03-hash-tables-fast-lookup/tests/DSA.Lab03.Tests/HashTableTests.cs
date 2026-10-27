using NUnit.Framework;

namespace DSA.Lab03.Tests
{
    public sealed class HashTableTests
    {
        [Test]
        public void LinearMap_CanFindStoredValue()
        {
            var map = new LinearIdMap<string>();
            map.Set(734, "North Gate");
            Assert.That(map.TryGetValue(734, out string? value), Is.True);
            Assert.That(value, Is.EqualTo("North Gate"));
        }

        [Test]
        public void HashTable_CanFindStoredValue()
        {
            var table = new IntHashTable<string>(5);
            table.Set(734, "North Gate");
            Assert.That(table.TryGetValue(734, out string? value), Is.True);
            Assert.That(value, Is.EqualTo("North Gate"));
        }

        [Test]
        public void HashTable_CollidingKeysRemainDistinct()
        {
            var table = new IntHashTable<string>(5);
            table.Set(1, "A"); table.Set(6, "B"); table.Set(11, "C");
            Assert.That(table.TryGetValue(1, out string? a), Is.True);
            Assert.That(table.TryGetValue(6, out string? b), Is.True);
            Assert.That(table.TryGetValue(11, out string? c), Is.True);
            Assert.That(a, Is.EqualTo("A"));
            Assert.That(b, Is.EqualTo("B"));
            Assert.That(c, Is.EqualTo("C"));
        }

        [Test]
        public void HashTable_MissingKeyReturnsFalse()
        {
            var table = new IntHashTable<string>(5);
            table.Set(1, "A");
            Assert.That(table.TryGetValue(99, out _), Is.False);
        }

        [Test]
        public void HashTable_ContainsKeyUsesStoredKeys()
        {
            var table = new IntHashTable<int>(3);
            table.Set(4, 40);
            Assert.That(table.ContainsKey(4), Is.True);
            Assert.That(table.ContainsKey(5), Is.False);
        }

        [Test]
        public void HashTable_SetExistingKeyReplacesValueWithoutGrowing()
        {
            var table = new IntHashTable<string>(5);
            table.Set(6, "Old");
            table.Set(6, "New");
            Assert.That(table.Count, Is.EqualTo(1));
            Assert.That(table.TryGetValue(6, out string? value), Is.True);
            Assert.That(value, Is.EqualTo("New"));
        }
    }
}
