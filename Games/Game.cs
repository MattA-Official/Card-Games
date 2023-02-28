using System.Collections.Generic;
using Cards;

namespace Games
{
    abstract class Game
    {
        protected List<Player> players;
        protected Deck deck;

        public Game()
        {
            players = new List<Player>();
            deck = new Deck();
        }

        public abstract void Play();

        internal abstract class Player
        {
            public string Name { get; set; }
            public EmptyDeck Hand { get; set; }

            public Player(string name)
            {
                Name = name;
                Hand = new EmptyDeck();
            }

            public abstract int Score { get; }

            public override string ToString()
            {
                string hand = "";
                foreach (Card card in Hand)
                {
                    hand += card.ToString() + ", ";
                }
                hand = hand.Substring(0, hand.Length - 2);
                return string.Format($"{Name} has the cards: {hand}");
            }
        }
    }
}
