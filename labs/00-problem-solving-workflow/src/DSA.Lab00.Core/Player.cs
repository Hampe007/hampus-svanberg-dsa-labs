namespace DSA.Lab00
{
    public sealed class Player
    {
        public Player(int id, string name, int score)
        {
            Id = id;
            Name = name;
            Score = score;
        }

        public int Id { get; }
        public string Name { get; }
        public int Score { get; }
    }
}
