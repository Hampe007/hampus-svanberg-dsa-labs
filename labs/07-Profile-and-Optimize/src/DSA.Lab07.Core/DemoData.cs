using System.Collections.Generic;

namespace DSA.Lab07
{
    public static class DemoData
    {
        public static List<Enemy> Enemies(int count)
        {
            var xs = new List<Enemy>(count);
            for (int i = 0; i < count; i++)
            {
                xs.Add(new Enemy(i, 1 + (i % 17)));
            }

            return xs;
        }

        public static List<int> ActiveIds(int count, int every)
        {
            var xs = new List<int>();
            for (int i = 0; i < count; i += every)
            {
                xs.Add(i);
            }

            return xs;
        }
    }
}
