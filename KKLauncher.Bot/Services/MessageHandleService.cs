using KKLauncher.Bot.Factories;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KKLauncher.Bot.Services
{
    public class MessageHandleService
    {
        private MenuCommandFactory _menuCommandFactory;

        public MessageHandleService()
        {
            _menuCommandFactory = new MenuCommandFactory();
        }

        public async Task HandleAsync(ITelegramBotClient kkBot, Update updateData)
        {
            if (updateData.Message == null)
            {
                return;
            }

            var messageText = updateData.Message?.Text;
            var chatId = updateData.Message?.Chat?.Id;

            if (chatId == null ||
                string.IsNullOrEmpty(messageText) ||
                !messageText.StartsWith("/"))
            {
                //TODO: App search
                return;
            }

            await _menuCommandFactory
                .Create(messageText)
                .ExecuteAsync(kkBot, chatId.Value);
        }
    }
}
