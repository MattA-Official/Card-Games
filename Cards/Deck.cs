using System;
using System.Collections.Generic;

namespace Cards
{
    class Deck
    {
        protected List<Card> cards;
        private Random random;

        public Deck()
        {
            cards = new List<Card>();
            random = new Random();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    cards.Add(new Card((Suit)i, (Rank)j));
                }
            }
        }

        public int Count
        {
            get { return cards.Count; }
        }

        public Card this[int i]
        {
            get { return cards[i]; }
            set { cards[i] = value; }
        }

        public void Add(Card card)
        {
            cards.Add(card);
        }

        public Card Deal()
        {
            if (cards.Count != 0)
            {
                Card cardToDeal = cards[0];
                cards.RemoveAt(0);
                return cardToDeal;
            }
            else
            {
                return null;
            }
        }

        public Card Deal(int index)
        {
            Card cardToDeal = cards[index];
            cards.RemoveAt(index);
            return cardToDeal;
        }

        public void Shuffle()
        {
            List<Card> shuffledCards = new List<Card>();
            while (cards.Count > 0)
            {
                int cardToMove = random.Next(cards.Count);
                shuffledCards.Add(cards[cardToMove]);
                cards.RemoveAt(cardToMove);
            }
            cards = shuffledCards;
        }

        public string[] GetCardNames()
        {
            string[] cardNames = new string[cards.Count];
            for (int i = 0; i < cards.Count; i++)
            {
                cardNames[i] = cards[i].ToString();
            }
            return cardNames;
        }
    }
}
