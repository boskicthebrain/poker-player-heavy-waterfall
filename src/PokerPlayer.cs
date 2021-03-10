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


            if (betterGameState.community_cards.Count == 0)
            {
                if (cardValue > 0)
                {
                    return Raise(betterGameState, ourPlayer);
                }

                if (cardValue > 0.73f // At least Jack
                    && betterGameState.current_buy_in <= betterGameState.small_blind * 2) 
                {
                    return Call(betterGameState, ourPlayer);
                }

                return Fold();
            }

            if (betterGameState.community_cards.Count >= 3)
            {
                if (cardValue >= 3)
                {
                    return ourPlayer.stack;
                }

                if (cardValue >= 2)
                {
                    return Raise(betterGameState, ourPlayer);
                }

                if (cardValue == 1)
                {
                    if (!IsBetTooHigh(betterGameState, ourPlayer))
                    {
                        return Call(betterGameState, ourPlayer);
                    }
                }

                return Fold();
            }

            return Fold();
        }

        private static bool IsBetTooHigh(GameState betterGameState, Player ourPlayer)
        {
            return betterGameState.current_buy_in - ourPlayer.bet > (ourPlayer.stack / 2);
        }

        private static int Raise(GameState betterGameState, Player ourPlayer)
        {
            return betterGameState.current_buy_in - ourPlayer.bet + betterGameState.minimum_raise;
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