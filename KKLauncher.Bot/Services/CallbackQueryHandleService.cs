using KKLauncher.Bot.Factories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KKLauncher.Bot.Services
{
    public class CallbackQueryHandleService
    {
        private readonly CallbackCommandFactory _callbackCommandFactory;

        public CallbackQueryHandleService()
        {
            _callbackCommandFactory = new CallbackCommandFactory();
        }

        public async Task HandleAsync(ITelegramBotClient kkBot, Update updateData)
        {
            if (updateData.CallbackQuery == null || updateData.CallbackQuery.Message == null)
            {
                return;
            }

            var callbackData = updateData.CallbackQuery.Data;
            var userTelegramId = updateData.CallbackQuery.From?.Id;
            var chatId = updateData.CallbackQuery.Message.Chat.Id;

            if (string.IsNullOrEmpty(callbackData) || userTelegramId == null)
            {
                return;
            }

            var callbackObject = JsonConvert.DeserializeObject<JObject>(callbackData);
            if (callbackObject == null)
            {
                return;
            }

            await _callbackCommandFactory
                .Create(callbackObject)
                .ExecuteAsync(kkBot, callbackObject, chatId, updateData.CallbackQuery.Message.MessageId);
        }
    }
}
