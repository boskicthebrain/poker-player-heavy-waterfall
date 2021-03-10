using System.Collections.Generic;
using System.Linq;

namespace Nancy.Simple
{
    public class PreflopStrategyAnalyzer
    {
        public int Analyze(List<Card> playerCards)
        {
            // Pairs
            if (playerCards.GroupBy(c => c.rank).Count() == 1)
            {
                switch (RankToInt(playerCards.First().rank))
                {
                    case 2:
                    case 3:
                    case 4:
                        return 7;
                    case 5:
                    case 6:
                        return 6;
                    case 7:
                        return 5;
                    case 8:
                        return 4;
                    case 9:
                        return 3;
                    case 10:
                        return 2;
                    default:
                        return 1;
                }
            }

            // not pair
            var firstCard = playerCards.OrderByDescending(c => RankToInt(c.rank)).First();
            var secondCard = playerCards.OrderBy(c => RankToInt(c.rank)).First();
            switch (RankToInt(firstCard.rank))
            {
                // Ass
                case 14:
                    switch (RankToInt(secondCard.rank))
                    {
                        case 13:
                            return 2;
                        case 12:
                            return 3;
                        case 11: 
                            return 4;
                        case 10:
                            return 6;
                        case 9:
                            return 8;
                        default:
                            return 1000;
                    }
                case 13:
                    switch (RankToInt(secondCard.rank))
                    {
                        case 12:
                            return 4;
                        case 11:
                            return 5;
                        case 10:
                            return 6;
                        case 9:
                            return 8;
                        default:
                            return 1000;
                    }
                case 12:
                    switch (RankToInt(secondCard.rank))
                    {
                        case 11:
                            return 5;
                        case 10:
                            return 6;
                        case 9:
                            return 8;
                        default:
                            return 1000;
                    }
                case 11:
                    switch (RankToInt(secondCard.rank))
                    {
                        case 10:
                            return 5;
                        case 9:
                            return 6;
                        case 8:
                            return 8;
                        default:
                            return 1000;
                    }       
                case 10:
                    switch (RankToInt(secondCard.rank))
                    {
                        case 9:
                            return 7;
                        case 8:
                            return 8;
                        default:
                            return 1000;
                    }
                case 9:
                    switch (RankToInt(secondCard.rank))
                    {
                        case 8:
                            return 7;
                        default:
                            return 1000;
                    }
                case 8:
                    switch (RankToInt(secondCard.rank))
                    {
                        case 7:
                            return 8;
                        default:
                            return 1000;
                    } 
                case 7:
                    switch (RankToInt(secondCard.rank))
                    {
                        case 6:
                            return 8;
                        default:
                            return 1000;
                    } 
                case 6:
                    switch (RankToInt(secondCard.rank))
                    {
                        case 5:
                            return 8;
                        default:
                            return 1000;
                    }
                case 5:
                    switch (RankToInt(secondCard.rank))
                    {
                        case 4:
                            return 8;
                        default:
                            return 1000;
                    }
                case 4:
                    switch (RankToInt(secondCard.rank))
                    {
                        case 3:
                            return 7;
                        default:
                            return 1000;
                    }
                case 3:
                    switch (RankToInt(secondCard.rank))
                    {
                        case 2:
                            return 7;
                        default:
                            return 1000;
                    }
                    default: 
                        return 1000;
            }
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