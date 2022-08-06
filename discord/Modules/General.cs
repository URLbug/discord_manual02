using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.IO;
using System.Net;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace discord_manual.Modules
{
    public class General : ModuleBase<SocketCommandContext>
    {
        public Random random = new Random();

        [Command("Da")]
        [Alias("Yea", "Yes")]
        public async Task PingAsync()
        {
            await ReplyAsync("Pizda!");
        }

        [Command("info")]
        public async Task info(SocketGuildUser user = null)
        {

            if (user == null) user = Context.User as SocketGuildUser;
            else
            {
                var embed = new EmbedBuilder()
                .WithTitle($"Информация о {user.Username}")
                .AddField("Имя:", $"{user.Username}")
                .AddField("Когда создант аккаунт:", $"{user.CreatedAt}")
                .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                .Build();

                await ReplyAsync(embed: embed);
            }
        }

        [Command("Roll")]
        [Alias("Random")]
        public async Task Roll()
        {

            await ReplyAsync($"Вам выпало число {random.Next(1, 100)}");
        }

        [Command("8ball")]
        [Alias("ask")]
        public async Task EightBall([Remainder] string quest = null)
        {

            if (quest == null) await ReplyAsync("Я не понял ваш вопрос");
            else
            {
                string[] output = { "Да.", "Нет.", "Возможно.", "Задайте вопрос чуть позже.", "Духи говорят ДА.", "Будующее говорит вам НЕТ." };

                var embed = new EmbedBuilder()
                    .AddField("Ваш вопрос:", $"{quest}")
                    .AddField("Ответ:", $"{output[random.Next(0, 5)]}")
                    .Build();

                await ReplyAsync(embed: embed);
            }
        }

        [Command("google")]
        [Alias("Browser")]
        public async Task GoogleParsing([Remainder] string google = null)
        {
            if (google == null) await ReplyAsync("Вы не ввели поисковой запрос");
            else
            {
                var googleRequest1 = "https://www.google.com/search?q=";

                var button = new ButtonBuilder()
                {
                    Label = google,
                    Url = googleRequest1 + string.Join("+", google.Split().ToArray()),
                    Style = ButtonStyle.Link
                };

                var commpon = new ComponentBuilder()
                    .WithButton(button);

                await ReplyAsync($"Вот что я нашел по вашему запросу: {google}", components: commpon.Build());
            }
        }

        [Command("hug")]
        public async Task Hug(SocketGuildUser user = null, [Remainder] string pluse = null)
        {
            if (user == null) user = Context.User as SocketGuildUser;
            else
            {
                string[] hugGif =
                    {
                    "https://c.tenor.com/NGFif4dxa-EAAAAj/hug-hugs.gif",
                    "https://c.tenor.com/lZ14N0YmIjMAAAAM/happy-birthday.gif",
                    "https://c.tenor.com/XyMvYx1xcJAAAAAM/super-excited.gif",
                    "https://c.tenor.com/ha0ekhrwZ2MAAAAM/i-just-wanna-hug-you-ollie-hug.gif",
                    "https://c.tenor.com/pGBR8dRqaN8AAAAM/mocha-milk-bear.gif"
                    };

                var embed = new EmbedBuilder()
                    .WithTitle($"{Context.User.Username} обнял {user.Username}")
                    .AddField("Сообщение к объятиям:", pluse ?? "Нету")
                    .WithImageUrl(hugGif[random.Next(0, hugGif.Length - 1)]);

                await ReplyAsync(embed: embed.Build());
            }
        }


        [Command("Kiss")]
        public async Task Kiss(SocketGuildUser user = null, [Remainder] string pluse = null)
        {
            if (user == null) user = Context.User as SocketGuildUser;
            else
            {
                string[] kiss = { "https://c.tenor.com/217aKgnf16sAAAAM/kiss.gif", 
                    "https://c.tenor.com/U7h-gyy--akAAAAM/kiss.gif", 
                    "https://c.tenor.com/4wtQ-7iub7AAAAAM/ishupanda-happy.gif" };

                var embed = new EmbedBuilder()
                    .WithTitle($"{Context.User.Username} поцеловал {user.Username}")
                    .AddField("Сообщение к объятиям:", pluse ?? "Нету")
                    .WithImageUrl(kiss[random.Next(0,kiss.Length-1)])
                    .Build();

                await ReplyAsync(embed: embed);
            }

        }
    }
}

