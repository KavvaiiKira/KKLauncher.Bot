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
                //TODO: Logger
                //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

                await _messgaeTypeFactory.Create(update.Type).Invoke(kkBot, update);
            }
            catch (Exception ex)
            {
                //TODO: Logger
                Console.WriteLine(ex.Message);
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient kkBot, Exception exception, CancellationToken cancellationToken)
        {
            //TODO: Logger
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }
}
