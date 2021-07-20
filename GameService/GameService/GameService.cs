using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService
{
    public class GameService
    {
        private Dictionary<Guid, Game> games = new Dictionary<Guid, Game>();

        public Guid JoinRandomGame(string name)
        {
            
        }

        public Game Create()
        {
            var game = new Game();
            games[game.Id] = game;
            return game;
        }

        public Game[] List()
        {
            return games.Values.ToArray();
        }

        public int Count()
        {
            return games.Count();
        }
    }
}
