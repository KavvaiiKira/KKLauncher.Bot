using KKLauncher.Bot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KKLauncher.Bot.Factories
{
    public class MessageTypeFactory
    {
        private MessageHandleService _messageHandleService;
        private CallbackQueryHandleService _callbackQueryHandleService;
        private Dictionary<UpdateType, Func<ITelegramBotClient, Update, Task>> _messageTypes;

        public MessageTypeFactory()
        {
            _messageHandleService = new MessageHandleService();
            _callbackQueryHandleService = new CallbackQueryHandleService();

            _messageTypes = new Dictionary<UpdateType, Func<ITelegramBotClient, Update, Task>>()
            {
                { UpdateType.Message, _messageHandleService.HandleAsync },
                { UpdateType.CallbackQuery, _callbackQueryHandleService.HandleAsync }
            };
        }

        public Func<ITelegramBotClient, Update, Task> Create(UpdateType updateType)
        {
            return _messageTypes.ContainsKey(updateType) ?
                _messageTypes[updateType] :
                throw new NotImplementedException("Unsupported type of data!");
        }
    }
}
