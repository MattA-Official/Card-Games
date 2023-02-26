using System;
using System.Collections.Generic;

namespace Cards
{
    class Deck
    {
        private List<Card> cards;
        private Random random;

        public Deck()
        {
            cards = new List<Card>();
            random = new Random();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    cards.Add(new Card(i, j));
                }
            }
        }

        public Deck(IEnumerable<Card> initialCards)
        {
            cards = new List<Card>(initialCards);
        }

        public int Count
        {
            get { return cards.Count; }
        }

        public void Add(Card card)
        {
            cards.Add(card);
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

        public IEnumerable<string> GetCardNames()
        {
            string[] cardNames = new string[cards.Count];
            for (int i = 0; i < cards.Count; i++)
            {
                cardNames[i] = cards[i].Name;
            }
            return cardNames;
        }

        public void Sort()
        {
            // cards.Sort( TODO: Add a comparison method here );
        }

        public void SortByValue()
        {
            // cards.Sort( TODO: Add a comparison method here );
        }
    }
}
