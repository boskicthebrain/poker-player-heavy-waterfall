using System.Collections.Generic;

namespace Nancy.Simple
{
    public class Player
    {
        public string name { get; set; }
        public int stack { get; set; }
        public string status { get; set; }
        public int bet { get; set; }
        public List<Card> hole_cards { get; set; }
        public string version { get; set; }
        public int id { get; set; }
    }
}