using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThirtyOne.Helpers;

namespace ThirtyOne.Models
{
    /// <summary>
    /// Computer AI Player
    /// </summary>
    public class ComputerPlayer : Player
    {
        private Random _random;

        /// <summary>
        /// Event to fire, when done with turn
        /// </summary>
        public event Action<Card> Done;

        public ComputerPlayer(string Name) : base(Name)
        {
            this._random = new Random();
        }

        public ComputerPlayer()
        {
            this._random = new Random();
        }

        public override void Turn(Game g)
        {
            //First, decide on action: Draw from deck, draw from table, knock
            if (Hand.CalculateScore() > 25 && !g.Players.Any(p => p.HasKnocked) && _random.Next(3) == 1)
            {
                this.HasKnocked = true;
                return;
            }
            else
            {
                //Decide if I should draw from table or from deck
                if (g.Table.Any() && g.Table.Last().Value >= 10 && _random.Next(2) == 1) DrawFromTable(g);
                else DrawFromDeck(g);

                //Drop card that'll give highest score
                List<Tuple<Card, int>> lst = new List<Tuple<Card, int>>();
                foreach (var c in Hand)
                {
                    lst.Add(new Tuple<Card, int>(c, Hand.Except(new Card[] { c }).CalculateScore()));
                }
                int idx = Hand.IndexOf(lst.OrderByDescending(l => l.Item2).First().Item1);
                DropCard(g, idx);
                //Invoking Done event with the card we dropped
                Done?.Invoke(lst.OrderByDescending(l => l.Item2).First().Item1);
            }
        }
    }
}
