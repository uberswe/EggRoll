using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Egg_roll.Server
{
    public class ScoreTable
    {
        public List<string> players;
        public List<string> scores;

        public ScoreTable()
        {
            players = new List<string>();
            scores = new List<string>();
        }

        public void Add(string player, string score)
        {
            players.Add(player);
            scores.Add(score);
        }
    }
}
