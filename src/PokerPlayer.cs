using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "We smarter now.";

        public static int BetRequest(JObject gameState)
        {
            var betterGameState = gameState.ToObject<GameState>();
            var handAnalyzer = new HandAnalyzer();
            var ourPlayer = betterGameState.players.First(p => p.name == "Heavy Waterfall");

            var cardValue = handAnalyzer.Analyze(betterGameState.community_cards, ourPlayer.hole_cards);

            if (betterGameState.community_cards.Count < 3)
            {
                return Call(betterGameState, ourPlayer);
            }
            
            if (cardValue >= 2)
            {
                return ourPlayer.stack;
            }

            return Fold();
        }

        private static int Fold()
        {
            return 0;
        }

        private static int Call(GameState gameState, Player player)
        {
            return Math.Max(gameState.current_buy_in - player.bet, 0);
        }


        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }
    }
}