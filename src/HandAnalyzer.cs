using System;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.Simple
{
    public class HandAnalyzer
    {
        public float Analyze(List<Card> communityCards, List<Card> holeCards)
        {
            var allCards = communityCards.Union(holeCards).ToList();
            var groups = allCards.GroupBy(c => c.rank).ToList();

            var hasTriple = groups.Any(g => g.Count() == 3);
            var numPairs = groups.Count(g => g.Count() == 2);

            if (IsStraight(allCards) && IsFlush(allCards))
            {
                return 8;
            }

            if (groups.Any(g => g.Count() >= 4))
            {
                return 7;
            }

            var hasFullHouse = numPairs > 0 && hasTriple;
            if (hasFullHouse)
            {
                return 6;
            }

            if (IsFlush(allCards))
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
            return RankToInt(highest.rank, aceValue: 14) / 15f;
        }

        private static bool IsFlush(List<Card> allCards)
        {
            return allCards.GroupBy(c => c.suit).Any(g => g.Count() >= 5);
        }

        public bool IsStraight(List<Card> cards)
        {
            return IsStraight(cards, s => RankToInt(s, aceValue: 1)) ||
                   IsStraight(cards, s => RankToInt(s, aceValue: 14));
        }

        private bool IsStraight(List<Card> cards, Func<string, int> rankToInt)
        {
            var orderedIntRanks = cards.Select(c => rankToInt(c.rank)).Distinct().OrderBy(i => i).ToList();
            for (var startPos = 0; startPos < cards.Count - 4; startPos++)
            {
                var currentValue = orderedIntRanks[startPos];
                var straightCount = 1;
                if (orderedIntRanks.Count - startPos >= 5)
                {
                    for (var i = 1; i < 5; i++)
                    {
                        if (orderedIntRanks[startPos + i] == currentValue + 1)
                        {
                            currentValue += 1;
                            straightCount += 1;
                        }
                        
                        if (straightCount >= 5)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        
        private int RankToInt(string rank, int aceValue)
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
                case "A": return aceValue;
            }

            return 0;
        }
    }
}