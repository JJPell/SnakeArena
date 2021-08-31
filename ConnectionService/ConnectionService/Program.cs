using Games;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            Thread thread = new Thread(() => UpdateGames(gameService));

            thread.Start();
        }

        public static void UpdateGames(GameService gameService)
        {
            Console.WriteLine("Updating games");
            int tickRate = 8;
            int updateDuration = 1000 / tickRate;

            while (true)
            {
                var startTime = DateTime.Now.Millisecond;
                gameService.UpdateAll();
                var endTime = DateTime.Now.Millisecond;
                var duration = endTime - startTime;
                var sleepDuration = updateDuration - duration;

                if (sleepDuration > 0)
                {
                    Thread.Sleep(sleepDuration);
                }
            }
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
