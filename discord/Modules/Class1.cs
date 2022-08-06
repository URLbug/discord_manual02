using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_manual.Modules
{
    public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
    {

        [SlashCommand("hi", "hello")]
        public async Task HiAsync(string input) 
        {
            await RespondAsync(input);
        }
    }
}
