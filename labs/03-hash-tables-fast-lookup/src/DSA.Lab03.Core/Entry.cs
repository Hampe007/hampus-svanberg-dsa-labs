namespace DSA.Lab03
{
    public readonly struct Entry<TValue>
    {
        public Entry(int key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public int Key { get; }
        public TValue Value { get; }
    }
}
