using System.Collections.Generic;
using System.Linq;

namespace Nancy.Simple
{
    public class HandAnalyzer
    {
        public int Analyze(List<Card> communityCards, List<Card> holeCards)
        {
            var allCards = communityCards.Union(holeCards).ToList();
            var groups = allCards.GroupBy(c => c.rank);
            
            var hasTriple  = groups.Any(g => g.Count() == 3);
            var numPairs = groups.Count(g => g.Count() == 2);

            if (groups.Any(g => g.Count() >= 4))
            {
                return 7;
            }
            
            var hasFullHouse = numPairs > 0 && hasTriple;
            if (hasFullHouse)
            {
                return 6;
            }
            
            var hasFlush = allCards.GroupBy(c => c.suit).Any(g => g.Count() >= 5);
            if (hasFlush)
            {
                return 5;
            }

            if (hasTriple)
            {
                return 3;
            }

            if (numPairs > 1)
            {
                return 2;
            }

            if (numPairs > 0)
            {
                return 1;
            }

            var highest = allCards.OrderBy(c => c.rank).Last();
            return 0;
        }
    }
}