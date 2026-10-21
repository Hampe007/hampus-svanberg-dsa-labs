using System;

namespace DSA.Lab01
{
    public static class SortAlgorithms
    {
        public static void InsertionSort(int[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            for (int i = 1; i < values.Length; i++)
            {
                int valueToInsert = values[i];
                int j = i - 1;

                while (j >= 0 && values[j] > valueToInsert)
                {
                    values[j + 1] = values[j];
                    j--;
                }
                
                values[j + 1] = valueToInsert;
            }
        }
    }
}
