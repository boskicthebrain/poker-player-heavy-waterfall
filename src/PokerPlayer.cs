using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "Super secret tactic which is totally not all in.";

		public static int BetRequest(JObject gameState)
		{
			var player = gameState["players"]
				.First(x => x["name"].Value<string>() == "Heavy Waterfall");
			var cards = player["hole_cards"];
			if (cards[0]["rank"] == cards[1]["rank"]){
				var stack = player["stack"]
					.Value<int>();
				return stack;
			}

			return 0;
		}

		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}
	}
}

