using System;
using System.Collections.Generic;
using System.Text;

namespace ThirtyOne.Models
{
    public abstract class Player
    {
        public List<Card> Hand { get; set; }

        public string Name { get; set; }

        public bool HasKnocked { get; set; }

        /// <summary>
        /// Must be implemented in child classes.
        /// </summary>
        /// <param name="g"></param>
        public abstract void Turn(Game g);

        public Player()
        {
            Hand = new List<Card>();
            HasKnocked = false;
        }

        public Player(string Name) : this()
        {
            this.Name = Name;
        }

        /// <summary>
        /// Draw a new card from the deck
        /// </summary>
        /// <param name="g"></param>
        public void DrawFromDeck(Game g)
        {
            Hand.Add(g.Deck.DrawCard());
        }

        /// <summary>
        /// Draw a card from the table to the hand
        /// </summary>
        /// <param name="g"></param>
        public void DrawFromTable(Game g)
        {
            Hand.Add(g.Table[g.Table.Count - 1]);
            g.Table.RemoveAt(g.Table.Count - 1);
        }

        /// <summary>
        /// Drops the specified card onto the table
        /// </summary>
        /// <param name="g"></param>
        /// <param name="idx"></param>
        public void DropCard(Game g, int idx)
        {
            g.Table.Add(Hand[idx]);
            Hand.RemoveAt(idx);
        }
    }

}
