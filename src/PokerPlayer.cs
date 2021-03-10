using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "We smart now.";

		public static int BetRequest(JObject gameState)
		{
			var betterGameState = gameState.ToObject<GameState>();
			
			if (betterGameState.round < 2)
			{
				return 0;
			}

			return betterGameState.players.First(p => p.name == "Heavy Waterfall").stack;
		}
		
		public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }
    }
}