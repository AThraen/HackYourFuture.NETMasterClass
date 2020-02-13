using System.Collections.Generic;

namespace ThirtyOne.Shared.Models
{
    public abstract class Player
    {
        public List<Card> Hand { get; set; }

        public string Name { get; set; }

        public bool HasKnocked { get; set; }

        public string LastAction { get; set; }

        /// <summary>
        /// Must be implemented in child classes.
        /// </summary>
        /// <param name="game"></param>
        public abstract void Turn(Game game);

        public Player()
        {
            Hand = new List<Card>();
            HasKnocked = false;
        }

        public Player(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// Draw a new card from the deck
        /// </summary>
        /// <param name="game"></param>
        public void DrawFromDeck(Game game)
        {
            Hand.Add(game.Deck.DrawCard());
        }

        /// <summary>
        /// Draw a card from the table to the hand
        /// </summary>
        /// <param name="game"></param>
        public void DrawFromTable(Game game)
        {
            Hand.Add(game.Table[game.Table.Count - 1]);
            game.Table.RemoveAt(game.Table.Count - 1);
        }

        /// <summary>
        /// Drops the specified card onto the table
        /// </summary>
        /// <param name="game"></param>
        /// <param name="index"></param>
        public void DropCard(Game game, int index)
        {
            game.Table.Add(Hand[index]);
            Hand.RemoveAt(index);
        }
    }
}