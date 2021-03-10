using System.Collections.Generic;
using System.Linq;

namespace Nancy.Simple
{
    public class HandAnalyzer
    {
        public int Analyze(List<Card> communityCards, List<Card> holeCards)
        {
            var allCards = communityCards.Union(holeCards).ToList();
            var groups = allCards.GroupBy(c => c.rank).ToList();

            var hasTriple = groups.Any(g => g.Count() == 3);
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

            var isStraight = IsStraight(allCards);
            if (isStraight)
            {
                return 4;
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

        private bool IsStraight(List<Card> cards)
        {
            var orderedIntRanks = cards.Select(c => RankToInt(c.rank)).OrderBy(i => i).ToList();
            for (var startPos = 0; startPos < cards.Count - 4; startPos++)
            {
                var currentValue = orderedIntRanks[startPos];
                var straightCount = 1;
                for (var i = 1; i < 5; i++)
                {
                    if (orderedIntRanks[startPos + i] == currentValue + 1)
                    {
                        currentValue += 1;
                        straightCount += 1;
                        continue;
                    }

                    if (straightCount >= 5)
                    {
                        return true;
                    }

                    break;
                }
            }

            return false;
        }

        private int RankToInt(string rank)
        {
            int intRank;
            if (int.TryParse(rank, out intRank))
            {
                return intRank;
            }

            switch (rank)
            {
                case "J": return 11;
                case "Q": return 12;
                case "K": return 13;
                case "A": return 14;
            }

            return 0;
        }
    }
}