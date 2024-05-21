using Telegram.Bot;
using Telegram.Bot.Types;

namespace KKLauncher.Bot.Commands.CallbackCommands
{
    public interface ICallbackCommand
    {
        Task ExecuteAsync(ITelegramBotClient kkBot, Update updateData, long chatId, int? messageId = null);
    }
}
