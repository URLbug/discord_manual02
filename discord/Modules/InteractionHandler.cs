using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Reflection;

namespace discord_manual.modules
{
    public class InteractionHandler 
    {
        private readonly IServiceProvider _provider;
        private readonly InteractionService _commandService;
        private readonly DiscordSocketClient _client;

        public InteractionHandler(DiscordSocketClient client, InteractionService commandService, IServiceProvider provider) 
        {
            _provider = provider;
            _commandService = commandService;
            _client = client;
        }

        public async Task IntaraciveServie() 
        {
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);

            _client.InteractionCreated += HandlerInteractive;
        }

        private async Task HandlerInteractive(SocketInteraction arg) 
        {
            try
            {
                var cxt = new SocketInteractionContext(_client, arg);
                await _commandService.ExecuteCommandAsync(cxt, _provider);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }


    }
}