using KKLauncher.Bot.Constants;
using KKLauncher.Bot.Services;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace KKLauncher.Bot
{
    public class KKBot
    {
        private ITelegramBotClient _bot;
        private string _botToken;
        private BotUpdateHandlerService _botUpdateHandlerService;

        private List<Telegram.Bot.Types.BotCommand> _botMainMenu = new List<Telegram.Bot.Types.BotCommand>()
        {
            new Telegram.Bot.Types.BotCommand()
            {
                Command = CommandConstants.StartCommand,
                Description = CommandConstants.StartCommandDescription
            },
            new Telegram.Bot.Types.BotCommand()
            {
                Command = CommandConstants.PCsCommand,
                Description = CommandConstants.PCsCommandDescription
            },
            new Telegram.Bot.Types.BotCommand()
            {
                Command = CommandConstants.AppsCommand,
                Description = CommandConstants.AppsCommandDescription
            }
        };

        public KKBot(string botToken)
        {
            _botToken = botToken;
            _bot = new TelegramBotClient(botToken);
            _botUpdateHandlerService = new BotUpdateHandlerService();
        }

        public async Task SetBotPropertiesAsync()
        {
            await _bot.SetMyCommandsAsync(_botMainMenu);
        }

        public void StartBot()
        {
            if (_bot == null)
            {
                _bot = new TelegramBotClient(_botToken);
            }

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            _bot.StartReceiving(
                _botUpdateHandlerService.HandleUpdateAsync,
                _botUpdateHandlerService.HandleErrorAsync,
                receiverOptions
            );
        }
    }
}
