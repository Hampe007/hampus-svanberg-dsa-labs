using System;

namespace DSA.Lab01
{
    public static class SearchAlgorithms
    {
        public static int LinearSearch(int[] values, int target)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            for (int i = 0; i < values.Length; ++i)
            {
                if (values[i] == target)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int BinarySearch(int[] sortedValues, int target)
        {
            if (sortedValues == null)
            {
                throw new ArgumentNullException(nameof(sortedValues));
            }

            int low = 0;
            int high = sortedValues.Length - 1;

            while (low <= high)
            {
                int middle = (low + high) / 2;

                if (sortedValues[middle] == target)
                {
                    return middle;
                }

                if (sortedValues[middle] < target)
                {
                    low = middle + 1;
                }
                else if (sortedValues[middle] > target)
                {
                    high = middle - 1;
                }
            }
            
            return -1;
        }
    }
}
