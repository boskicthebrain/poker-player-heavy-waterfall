using Nancy.Simple;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests
{
    public class AnalyzerTests
    {
        [Test]
        [TestCase(new[] {"A",}, false)]
        [TestCase(new[] {"A", "A"}, false)]
        [TestCase(new[] {"A", "A", "A"}, false)]
        [TestCase(new[] {"2", "3", "4", "5", "6"}, true)]
        [TestCase(new[] {"7", "8", "9", "A", "J"}, false)]
        [TestCase(new[] {"7", "8", "9", "10", "J"}, true)]
        [TestCase(new[] {"A", "A", "3", "4", "5", "J", "K"}, false)]
        [TestCase(new[] {"A", "2", "3", "4", "5", "J", "Q"}, true)]
        [TestCase(new[] {"2", "3", "4", "5", "6", "A", "J"}, true)]
        [TestCase(new[] {"3", "4", "5", "6", "7", "A", "J"}, true)]
        [TestCase(new[] {"4", "5", "6", "7", "8", "A", "J"}, true)]
        [TestCase(new[] {"7", "8", "9", "10", "J", "K", "A"}, true)]
        [TestCase(new[] {"8", "9", "10", "J", "Q", "A", "A"}, true)]
        [TestCase(new[] {"3", "4", "10", "J", "Q", "K", "A"}, true)]
        [TestCase(new[] {"2", "3", "3", "4", "5", "6", "10"}, true)]
        [TestCase(new[] {"2", "4", "6", "7", "8", "A", "J"}, false)]
        public void IsStraight_MultipleDataSets(string[] cardRanks, bool isFlush)
        {
            var analyzer = new HandAnalyzer();
            var cards = GetCards(cardRanks);

            Assert.AreEqual(isFlush, analyzer.IsStraight(cards));
        }

        private List<Card> GetCards(string[] cardStrings)
        {
            var cards = new List<Card>();
            foreach (var cardString in cardStrings)
            {
                if (!string.IsNullOrEmpty(cardString))
                {
                    var parts = cardString.Split(' ');
                    var rank = parts[0];
                    var suit = parts.Length > 1 ? parts[1] : "Hearts";
                    cards.Add(new Card {rank = rank, suit = suit});
                }
            }

            return cards;
        }
        
        [Test]
        [TestCase(new[] {"2 Hearts", "4 Clubs", "6 Hearts", "7 Clubs", "8 Diamonds", "A Spades", "J Spades"}, 0)] // high card
        [TestCase(new[] {"2 Hearts", "2 Clubs", "6 Hearts", "7 Clubs", "8 Diamonds", "A Spades", "J Spades"}, 1)] // pair
        [TestCase(new[] {"2 Hearts", "2 Clubs", "6 Hearts", "6 Clubs", "8 Diamonds", "A Spades", "J Spades"}, 2)] // two pairs
        [TestCase(new[] {"2 Hearts", "2 Clubs", "2 Hearts", "7 Clubs", "8 Diamonds", "A Spades", "J Spades"}, 3)] // trips
        [TestCase(new[] {"2 Hearts", "3 Clubs", "4 Hearts", "5 Clubs", "6 Diamonds", "A Spades", "J Spades"}, 4)] // straight
        [TestCase(new[] {"2 Hearts", "3 Hearts", "4 Hearts", "5 Hearts", "10 Hearts", "A Spades", "J Spades"}, 5)] // flush
        [TestCase(new[] {"2 Hearts", "2 Hearts", "2 Hearts", "5 Clubs", "5 Hearts", "A Spades", "J Spades"}, 6)] // full house
        [TestCase(new[] {"2 Hearts", "2 Hearts", "2 Hearts", "2 Clubs", "5 Hearts", "A Spades", "J Spades"}, 7)] // quads
        [TestCase(new[] {"2 Clubs", "3 Clubs", "4 Clubs", "5 Clubs", "6 Clubs", "A Spades", "J Spades"}, 8)] // straight flush
        [TestCase(new[] {"A Clubs", "K Clubs", "Q Clubs", "J Clubs", "10 Clubs", "2 Spades", "3 Spades"}, 9)] // straight flush
        public void Rank_MultipleDataSets(string[] cardStrings, int value)
        {
            var cards = GetCards(cardStrings);
            var analyzer = new HandAnalyzer();
            
            Assert.AreEqual(value, analyzer.Analyze(cards, new List<Card>()));
        }
    }
}