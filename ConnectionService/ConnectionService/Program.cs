using Games;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace ConnectionService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var gameService = new GameService();

            StartGameService(gameService);
            CreateHostBuilder(args, gameService).Build().Run();
        }

        public static void StartGameService(GameService gameService)
        {
            var interval = new Timer(125);
            interval.AutoReset = true;
            interval.Enabled = true;
            interval.Elapsed += (Object source, ElapsedEventArgs e) =>
            {
                gameService.UpdateAll();
            };
        }

        public static IHostBuilder CreateHostBuilder(string[] args, GameService gameService) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<GameService>(gameService);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
