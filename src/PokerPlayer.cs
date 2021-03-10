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
			var maxBet = ourPlayer.stack;
			
			var cardValue = handAnalyzer.Analyze(betterGameState.community_cards, ourPlayer.hole_cards);

			if (cardValue >= 2)
			{
				return maxBet;
			}
			
			if (betterGameState.round >= 2 )
			{
				return maxBet/2;
			}

			return 0;
		}
		
		public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }
    }
}