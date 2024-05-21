using Telegram.Bot;

namespace KKLauncher.Bot.Commands.MenuCommands
{
    public interface IMenuCommand
    {
        Task ExecuteAsync(ITelegramBotClient kkBot, long chatId);
    }
}
