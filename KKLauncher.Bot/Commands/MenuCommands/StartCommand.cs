using Telegram.Bot;

namespace KKLauncher.Bot.Commands.MenuCommands
{
    public class StartCommand : IMenuCommand
    {
        public async Task ExecuteAsync(ITelegramBotClient kkBot, long chatId)
        {
            await kkBot.SendTextMessageAsync(
                chatId: chatId,
                text:
                    "Welcome!\n" +
                    "This tool will help you run applications on your PCs remotely.\n" +
                    "Write the name of the application or select from the list using the <b>/apps</b> command " +
                    "and indicate the PC on which you want to run this application.\n" +
                    "If an application with the same name is installed on several PCs, then you will be given a list of PCs to" +
                    "choose which one to run the application on.\n" +
                    "You can also view your PC list using the <b>/pcs</b> command.\n\n" +
                    "P.S.\n" +
                    "Adding PCs and applications occurs only through individual functions.\n\n" +
                    "P.S.S\n" +
                    "To launch applications remotely, you will also need to install the application on your PC.",
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
        }
    }
}
