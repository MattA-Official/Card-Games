using System.Collections.Generic;

namespace Cards
{
    class EmptyDeck : Deck
    {
        public EmptyDeck()
        {
            cards = new List<Card>();
        }

        public IEnumerator<Card> GetEnumerator()
        {
            foreach (Card card in cards)
            {
                yield return card;
            }
        }
    }
}
