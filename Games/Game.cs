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

        internal class Player
        {
            public string Name { get; set; }
            public List<Card> Hand { get; set; }

            public Player(string name)
            {
                Name = name;
                Hand = new List<Card>();
            }

            public void Draw(Deck deck)
            {
                Hand.Add(deck.Deal(0));
            }

            public Card Flip()
            {
                Card cardToFlip = Hand[0];
                Hand.RemoveAt(0);
                return cardToFlip;
            }

            public void ReceiveCard(Card card)
            {
                Hand.Add(card);
            }

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
