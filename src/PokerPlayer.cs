using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "Super secret tactic which is totally not all in.";

        public static int BetRequest(JObject gameState)
        {
            var betterGameState = gameState.ToObject<GameState>();
            var stack = gameState["players"]
                .First(x => x["name"].Value<string>() == "Heavy Waterfall")["stack"]
                .Value<int>();
            return stack;
        }

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }
    }
}