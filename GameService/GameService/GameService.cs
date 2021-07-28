using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    public class GameService
    {
        private Dictionary<Guid, Game> games = new Dictionary<Guid, Game>();

        public void EndGame(Guid gameId)
        {
            games.Remove(gameId);
        }

        public Game JoinGame(string playerId, string name)
        {
            var game = FindOrCreateGame();
            game.AddPlayer(playerId, name);
            return game;
        }

        public void LeaveGame(string playerId)
        {
            var game = FindGameByPlayer(playerId);
            if (game != null)
            {
                game.RemovePlayer(playerId);
                if (game.PlayerCount() < 1)
                {
                    EndGame(game.Id);
                }
            }
        }

        public void UpdatePlayerInput(string playerId, Component.Input playerInput)
        {
            var game = FindGameByPlayer(playerId);

            if (game != null)
            {
                game.UpdatePlayerInput(playerId, playerInput);
            }

            throw new Exception("No game exists with that player");
        }

        public Game FindGameByPlayer(string playerId)
        {
            foreach (var (_, game) in games)
            {
                if (game.HasPlayer(playerId))
                {
                    return game;
                }
            }

            return null;
        }

        public Game FindOrCreateGame()
        {
            foreach (var game in this.List())
            {
                if (game.IsJoinable())
                {
                    return game;
                }
            }

            return CreateGame();
        }

        public Game CreateGame()
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
