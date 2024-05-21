using Newtonsoft.Json.Linq;

namespace KKLauncher.Bot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = JObject.Parse(File.ReadAllText(Path.Combine("config", "kk-bot.json")));

            var kkBot = new KKBot(config["BotToken"].ToString());

            var botCreationMessage = "Telegram KKBot created.";
            await Logger.WriteInfo(botCreationMessage);
            Console.WriteLine(botCreationMessage);

            kkBot.StartBot();

            var botStartedCommand = "Telegram KKBot started.";
            await Logger.WriteInfo(botStartedCommand);
            Console.WriteLine(botStartedCommand);

            await Task.Delay(-1);
        }
    }
}
