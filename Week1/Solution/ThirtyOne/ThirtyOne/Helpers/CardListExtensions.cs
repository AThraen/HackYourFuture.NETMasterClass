using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThirtyOne.Models;

namespace ThirtyOne.Helpers
{ /// <summary>
  /// Extension methods to list of cards
  /// </summary>
    public static class CardListExtensions
    {

        /// <summary>
        /// Calculate score for a list of cards
        /// </summary>
        /// <param name="Cards"></param>
        /// <returns></returns>
        public static int CalculateScore(this IEnumerable<Card> Cards)
        {
            return Cards.GroupBy(c => c.Suit).OrderByDescending(grp => grp.Sum(c => c.Value)).First().Sum(c => c.Value);
        }

        /// <summary>
        /// Create a string list of all cards
        /// </summary>
        /// <param name="Cards"></param>
        /// <returns></returns>
        public static string ToListString(this IEnumerable<Card> Cards)
        {
            return string.Join(",", Cards);
        }
    }
}
