using Newtonsoft.Json.Linq;
using Telegram.Bot;

namespace KKLauncher.Bot.Commands.CallbackCommands
{
    public interface ICallbackCommand
    {
        Task ExecuteAsync(ITelegramBotClient kkBot, JObject callbackObj, long chatId, int messageId);
    }
}
