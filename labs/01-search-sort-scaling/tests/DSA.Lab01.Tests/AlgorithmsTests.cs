using DSA.Lab01;
using NUnit.Framework;

namespace DSA.Lab01.Tests
{
    [TestFixture]
    public sealed class SearchAlgorithmsTests
    {
        [Test]
        public void LinearSearch_ReturnsMatchingIndex()
        {
            int[] values = { 4, 9, 12, 20 };

            int result = SearchAlgorithms.LinearSearch(values, 12);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void LinearSearch_ReturnsMinusOneWhenMissing()
        {
            int[] values = { 4, 9, 12, 20 };

            int result = SearchAlgorithms.LinearSearch(values, 7);

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void LinearSearch_ReturnsMinusOneForEmptyArray()
        {
            int[] values = { };

            int result = SearchAlgorithms.LinearSearch(values, 7);

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void BinarySearch_ReturnsMatchingIndex()
        {
            int[] values = { 2, 5, 8, 12, 16, 23, 38 };

            int result = SearchAlgorithms.BinarySearch(values, 16);

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void BinarySearch_FindsFirstElement()
        {
            int[] values = { 2, 5, 8, 12, 16 };

            int result = SearchAlgorithms.BinarySearch(values, 2);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void BinarySearch_FindsLastElement()
        {
            int[] values = { 2, 5, 8, 12, 16 };

            int result = SearchAlgorithms.BinarySearch(values, 16);

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void BinarySearch_ReturnsMinusOneWhenMissing()
        {
            int[] values = { 2, 5, 8, 12, 16 };

            int result = SearchAlgorithms.BinarySearch(values, 7);

            Assert.That(result, Is.EqualTo(-1));
        }
    }

    [TestFixture]
    public sealed class SortAlgorithmsTests
    {
        [Test]
        public void InsertionSort_SortsValuesAscending()
        {
            int[] values = { 7, 2, 9, 1, 5, 4 };

            SortAlgorithms.InsertionSort(values);

            Assert.That(values, Is.EqualTo(new[] { 1, 2, 4, 5, 7, 9 }));
        }

        [Test]
        public void InsertionSort_HandlesAlreadySortedValues()
        {
            int[] values = { 1, 2, 3, 4 };

            SortAlgorithms.InsertionSort(values);

            Assert.That(values, Is.EqualTo(new[] { 1, 2, 3, 4 }));
        }

        [Test]
        public void InsertionSort_HandlesDuplicates()
        {
            int[] values = { 3, 1, 3, 2, 1 };

            SortAlgorithms.InsertionSort(values);

            Assert.That(values, Is.EqualTo(new[] { 1, 1, 2, 3, 3 }));
        }

        [Test]
        public void InsertionSort_HandlesEmptyArray()
        {
            int[] values = { };

            SortAlgorithms.InsertionSort(values);

            Assert.That(values, Is.Empty);
        }
    }
}
