using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "We smarter now.";

        public static int BetRequest(JObject gameState)
        {
            try
            {
                var betterGameState = gameState.ToObject<GameState>();
                var handAnalyzer = new HandAnalyzer();
                var ourPlayer = betterGameState.players.First(p => p.name == "Heavy Waterfall");

                var cardValue = handAnalyzer.Analyze(betterGameState.community_cards, ourPlayer.hole_cards);


                if (betterGameState.community_cards.Count == 0)
                {
                    if (cardValue >= 1)
                    {
                        return RaiseMinimum(betterGameState, ourPlayer);
                    }

                    if (cardValue > 0.73f // At least Jack
                        && betterGameState.current_buy_in <= betterGameState.small_blind * 2)
                    {
                        return RaiseMinimum(betterGameState, ourPlayer);
                    }

                    return Fold();
                }

                if (betterGameState.community_cards.Count >= 3)
                {
                    var relativeHandValue =
                        cardValue - handAnalyzer.Analyze(betterGameState.community_cards, new List<Card>());
                    if (cardValue >= 4)
                    {
                        return Raise(betterGameState, ourPlayer, RaiseStep.AllIn);
                    }

                    if (cardValue >= 3)
                    {
                        if (betterGameState.bet_index > 1)
                        {
                            return Call(betterGameState, ourPlayer);
                        }

                        return Raise(betterGameState, ourPlayer, RaiseStep.HalfStack);
                    }

                    if (cardValue >= 2)
                    {
                        if (betterGameState.bet_index > 1)
                        {
                            return Call(betterGameState, ourPlayer);
                        }

                        return Raise(betterGameState, ourPlayer, RaiseStep.ThirdStack);
                    }

                    if (cardValue >= 1)
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
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return 0;
            }
        }

        private static bool IsBetTooHigh(GameState betterGameState, Player ourPlayer)
        {
            return betterGameState.current_buy_in - ourPlayer.bet > (ourPlayer.stack / 2);
        }
        
        private enum RaiseStep { Minimum, ThirdStack, HalfStack, AllIn}

        private static int Raise(GameState state, Player we, RaiseStep step)
        {
            switch (step)
            {
                case RaiseStep.Minimum: return RaiseMinimum(state, we);
                case RaiseStep.ThirdStack: return we.stack / 3;
                case RaiseStep.HalfStack: return we.stack / 2;
                case RaiseStep.AllIn: return we.stack;
            }
            
            return RaiseMinimum(state, we);
        }

        private static int RaiseMinimum(GameState betterGameState, Player ourPlayer)
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