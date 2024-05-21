using KKLauncher.Bot.Factories;
using Telegram.Bot;

namespace KKLauncher.Bot.Services
{
    public class BotUpdateHandlerService
    {
        private MessageTypeFactory _messgaeTypeFactory;

        public BotUpdateHandlerService()
        {
            _messgaeTypeFactory = new MessageTypeFactory();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient kkBot, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            try
            {
                var updateObj = Newtonsoft.Json.JsonConvert.SerializeObject(update);

                await Logger.WriteInfo($"Bot handle update. Update: {updateObj}");
                Console.WriteLine(updateObj);

                await _messgaeTypeFactory.Create(update.Type).Invoke(kkBot, update);
            }
            catch (Exception ex)
            {
                await Logger.WriteError($"Bot handle update error! Error message: {ex.Message}. Exception object: {Newtonsoft.Json.JsonConvert.SerializeObject(ex)}");
                Console.WriteLine(ex.Message);
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient kkBot, Exception exception, CancellationToken cancellationToken)
        {
            var errorObj = Newtonsoft.Json.JsonConvert.SerializeObject(exception);
            await Logger.WriteError($"Bot error message: {exception.Message}. Exception object: {errorObj}");
            Console.WriteLine(errorObj);
        }
    }
}
