using ConnectionService.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Games;
using System.Timers;

namespace ConnectionService.Services
{
    public class BroadcastService
    {
        private readonly IHubContext<GameHub> hubContext;

        private readonly GameService gameService;

        private Timer interval;

        public BroadcastService(IHubContext<GameHub> hubContext, GameService gameService)
        {
            this.hubContext = hubContext;
            this.gameService = gameService;

            interval = new Timer(1000);
            interval.AutoReset = true;
            interval.Enabled = true;
            interval.Elapsed += (Object source, ElapsedEventArgs e) =>
            {
                foreach (var game in gameService.List())
                {
                    string jsonString = game.GetState().ToJson();
                    hubContext.Clients.Group(game.Id.ToString()).SendAsync("state-update", jsonString);
                }
            };
        }

        public void SetUpdateRate(int milliseconds)
        {
            interval.Interval = milliseconds;
        }
    }
}
