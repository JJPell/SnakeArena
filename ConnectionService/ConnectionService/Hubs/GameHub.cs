using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Games;
using Games.Component;
using Newtonsoft.Json;
using ConnectionService.Network.Game;

namespace ConnectionService.Hubs
{
    public class GameHub : Hub
    {
        public GameService gameService;

        public GameHub(GameService gameService)
        {
            this.gameService = gameService;
            System.Console.WriteLine("Game Hub Init");
        }

        public async Task Input(string message)
        {
            var connectionId = Context.ConnectionId;
            var input = InputMessageToInput(message);
            gameService.UpdatePlayerInput(connectionId, input);
        }

        public async Task JoinGame(string playerName)
        {
            Console.WriteLine("JoinGame: " + playerName);
            var connectionId = Context.ConnectionId;
            var game = gameService.JoinGame(connectionId, playerName);
            await Groups.AddToGroupAsync(connectionId, game.Id.ToString());
        }

        public async Task SendStateUpdate()
        {
            Console.WriteLine("SendStateUpdate");
            foreach (var game in gameService.List())
            {
                string jsonString = game.GetState().ToString();
                await Clients.Group(game.Id.ToString()).SendAsync("state-update", jsonString);
            }
        }

        public async Task OnDisconnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var game = gameService.FindGameByPlayer(connectionId);

            if (game != null)
            {
                await Groups.RemoveFromGroupAsync(connectionId, game.Id.ToString());
                gameService.LeaveGame(connectionId);
            }
        }

        private void CompressGameState(Game game)
        {
            throw new NotImplementedException();
        }

        private Input InputMessageToInput(string message)
        {
            throw new NotImplementedException();
        }
    }
}
