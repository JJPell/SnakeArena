using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Games;
using Games.Component;

namespace ConnectionService.Hubs
{
    public class GameHub : Hub
    {
        public GameService gameService;

        public GameHub(GameService gameService)
        {
            this.gameService = gameService;
        }

        public async Task Input(string message)
        {
            var connectionId = Context.ConnectionId;
            var input = InputMessageToInput(message);
            gameService.UpdatePlayerInput(connectionId, input);
        }

        public async Task JoinGame(string playerName)
        {
            var connectionId = Context.ConnectionId;
            gameService.JoinGame(connectionId, playerName);
        }

        public async Task OnDisconnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var game = gameService.FindGameByPlayer(connectionId);

            if (game != null)
            {
                gameService.LeaveGame(connectionId);
            }
        }

        private Input InputMessageToInput(string message)
        {
            throw new NotImplementedException();
        }
    }
}
