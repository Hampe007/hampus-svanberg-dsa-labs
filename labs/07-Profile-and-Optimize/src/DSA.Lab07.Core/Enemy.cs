namespace DSA.Lab07
{
    public readonly struct Enemy
    {
        public Enemy(int id, int threat)
        {
            Id = id;
            Threat = threat;
        }

        public int Id { get; }
        public int Threat { get; }
    }
}
