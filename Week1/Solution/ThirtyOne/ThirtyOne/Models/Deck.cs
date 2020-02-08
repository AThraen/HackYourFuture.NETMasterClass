using System;
using System.Collections.Generic;
using System.Text;

namespace ThirtyOne.Models
{
    /// <summary>
    /// A deck of cards
    /// </summary>
    public class Deck
    {
        private const int SHUFFLETIMES = 20000;

        /// <summary>
        /// The list cards
        /// </summary>
        public List<Card> Cards { get; set; }

        public int CardsLeft
        {
            get
            {
                return Cards.Count;
            }
        }

        /// <summary>
        /// Add all cards to deck
        /// </summary>
        public void Initialize()
        {
            foreach (Suits suit in (Suits[])Enum.GetValues(typeof(Suits)))
            {
                for (int i = 1; i < 14; i++)
                {
                    Card c = new Card() { Rank = i, Suit = suit };
                    Cards.Add(c);
                }
            }
        }

        /// <summary>
        /// Draw a card from the deck
        /// </summary>
        /// <returns>Card drawn</returns>
        public Card DrawCard()
        {
            if (Cards.Count == 0) return null;
            Card c = Cards[0];
            Cards.RemoveAt(0);
            return c;
        }

        /// <summary>
        /// Shuffle the cards
        /// </summary>
        /// <param name="R"></param>
        public void Shuffle(Random R)
        {
            for (int i = 0; i < SHUFFLETIMES; i++)
            {
                int from = R.Next(Cards.Count);
                int to = R.Next(Cards.Count);
                Card c = Cards[to];
                Cards[to] = Cards[from];
                Cards[from] = c;
            }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Deck()
        {
            Cards = new List<Card>();
        }
    }
}
