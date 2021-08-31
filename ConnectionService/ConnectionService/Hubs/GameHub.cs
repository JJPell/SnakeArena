using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Games;
using Games.Component;

namespace ConnectionService.Hubs
{
    public class GameHub : Hub
    {
        public GameService gameService;

        public GameHub(GameService gameService, IHubContext<GameHub> hubContext)
        {
            this.gameService = gameService;
            System.Console.WriteLine("Game Hub Init");


        }

        public async Task Input(int message)
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

        private Input InputMessageToInput(int message)
        {
            // Decimal to binary string conversion
            string binary = Convert.ToString(message, 2);
            // Pad binary with 0's if below 4 chars
            binary = binary.PadLeft(4, '0');
            bool left = binary[0] == '1';
            bool right = binary[1] == '1';
            bool up = binary[2] == '1';
            bool down = binary[3] == '1';

            return new Input
            {
                Left = left,
                Right = right,
                Up = up,
                Down = down,
            };
        }
    }
}
