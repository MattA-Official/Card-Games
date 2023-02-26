namespace Cards
{
    class Card
    {
        public int Suit { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }

        public Card(int suit, int rank)
        {
            if (suit < 0 || suit > 3)
                throw new System.ArgumentException("Invalid suit. Must be between 0 and 3", "suit");
            if (rank < 1 || rank > 13)
                throw new System.ArgumentException("Invalid rank. Must be between 1 and 13", "rank");

            Suit = suit;
            Rank = rank;
        }

        public override string ToString()
        {
            return string.Format($"{Rank} of {Suit}");
        }
    }
}
