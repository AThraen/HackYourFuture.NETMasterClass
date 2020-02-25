namespace ThirtyOne.Shared.Models
{
    /// <summary>
    /// A playing card
    /// </summary>
    public class Card
    {
        /// <summary>
        /// The suit of the card
        /// </summary>
        public Suits Suit { get; set; }

        /// <summary>
        /// The rank of the card. 1 is Ace, 11 is Jack, 12 is Queen, 13 is King
        /// </summary>
        public int Rank { get; set; } //1-13

        /// <summary>
        /// The value for the game Thirty-One
        /// </summary>
        public int Value
        {
            get
            {
                return (Rank == 1) ? 11 :
                    (Rank >= 10 && Rank < 14) ? 10 : Rank;
            }
        }

        /// <summary>
        /// String representation of the card
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Rank + " of " + Suit;
        }
    }
}