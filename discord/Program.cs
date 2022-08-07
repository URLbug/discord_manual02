using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Discord;
using Discord.WebSocket;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Discord.Addons.Hosting;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using discord_manual.Modules;
using discord_manual.modules;

namespace discord_manual
{
    class Program
    {
        static async Task Main()
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration(x =>
                {
                    var configuation = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("discord/appsettings.json", false, true)
                    .Build();
                    x.AddConfiguration(configuation);
                })
                .ConfigureLogging(x =>
                {
                    x.AddConsole();
                    x.SetMinimumLevel(LogLevel.Debug);
                })
                .ConfigureDiscordHost((context, config) =>
                {
                    config.SocketConfig = new DiscordSocketConfig()
                    {
                        LogLevel = LogSeverity.Debug,
                        AlwaysDownloadUsers = false,
                        MessageCacheSize = 200,
                    };

                    config.Token = context.Configuration["Token"];
                }).UseCommandService((context, config) =>
                {
                    config.CaseSensitiveCommands = false;
                    config.DefaultRunMode = RunMode.Async;
                })
                .ConfigureServices((context, services) =>
                {
                    services
                    .AddHostedService<CommandHandler>();

                    //services.
                    //AddHostedService<InteractionHandler>();
                });

            var host = builder.Build();
            await host.RunAsync();
      
        }
    }
}
