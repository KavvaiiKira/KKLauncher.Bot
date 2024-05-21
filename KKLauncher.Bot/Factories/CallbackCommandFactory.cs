using KKLauncher.Bot.Commands.CallbackCommands;

namespace KKLauncher.Bot.Factories
{
    public class CallbackCommandFactory
    {
        private readonly Dictionary<string, ICallbackCommand> _commands;

        public CallbackCommandFactory()
        {
            _commands = new Dictionary<string, ICallbackCommand>()
            {
            };
        }

        public ICallbackCommand Create(string callback)
        {
            return _commands.ContainsKey(callback) ?
                _commands[callback] :
                throw new NotImplementedException($"Unsupported type of callback! Callback was: \"{callback}\".");
        }
    }
}
