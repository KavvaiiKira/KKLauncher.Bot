using KKLauncher.Bot.Enums;
using KKLauncher.Bot.Factories;
using KKLauncher.Bot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KKLauncher.Bot.Services
{
    public class MessageHandleService
    {
        private CommandMessageCommandFactory _commandMessageCommandFactory;
        private DialogStatusCommandFactory _dialogStatusCommandFactory;

        public MessageHandleService()
        {
            _commandMessageCommandFactory = new CommandMessageCommandFactory();
            _dialogStatusCommandFactory = new DialogStatusCommandFactory();
        }

        public async Task HandleAsync(ITelegramBotClient kkAppsBot, Update updateData)
        {
            if (updateData.Message == null)
            {
                return;
            }

            var messageText = updateData.Message?.Text;
            var chatId = updateData.Message?.Chat?.Id;

            if (chatId == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(messageText) && messageText.StartsWith("/"))
            {
                await _commandMessageCommandFactory
                    .Create(messageText)
                    .ExecuteAsync(kkAppsBot, updateData, chatId.Value);

                return;
            }

            var session = BotSessionUtils.GerSessinByChatId(chatId.Value);
            if (session == null || session.Status == (int)DialogStatus.FreeText)
            {
                return;
            }

            await _dialogStatusCommandFactory
                .Create((DialogStatus)session.Status)
                .ExecuteAsync(kkAppsBot, updateData, session);
        }
    }
}
