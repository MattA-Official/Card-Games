namespace Cards
{
    class Card
    {
        public int Suit { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }

        public Card(int suit, int rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public override string ToString()
        {
            return string.Format($"{Rank} of {Suit}");
        }
    }
}
