using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ThirtyOne.Shared.Models
{
    /// <summary>
    /// A deck of cards
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// The list cards
        /// </summary>
        public List<Card> Cards { get; }

        [JsonIgnore]
        public int CardsLeft
        {
            get { return Cards.Count; }
        }

        /// <summary>
        /// Add all cards to deck
        /// </summary>
        public void Initialize()
        {
            foreach (Suits suit in (Suits[]) Enum.GetValues(typeof(Suits)))
            {
                for (int i = 1; i < 14; i++)
                {
                    Card card = new Card() {Rank = i, Suit = suit};
                    Cards.Add(card);
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
            Card card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }

        /// <summary>
        /// Shuffle the cards
        /// </summary>
        /// <param name="randomNumberGenerator"></param>
        public void Shuffle(Random randomNumberGenerator)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                int from = i;
                int to = randomNumberGenerator.Next(Cards.Count);
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