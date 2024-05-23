using KKLauncher.Bot.Commands.CallbackCommands;
using KKLauncher.Bot.Constants;
using Newtonsoft.Json.Linq;

namespace KKLauncher.Bot.Factories
{
    public class CallbackCommandFactory
    {
        private readonly Dictionary<string, Dictionary<string, ICallbackCommand>> _commands;

        public CallbackCommandFactory()
        {
            _commands = new Dictionary<string, Dictionary<string, ICallbackCommand>>()
            {
                {
                    CallbackConstants.PC.PCKey,
                    new Dictionary<string, ICallbackCommand>()
                    {
                        { CallbackConstants.Keys.Page, new PCsListCallbackCommand() }
                    }
                }
            };
        }

        public ICallbackCommand Create(JObject callbackObj)
        {
            var callbackType = callbackObj[CallbackConstants.Keys.Key]?.Value<string>() ?? string.Empty;
            if (!_commands.ContainsKey(callbackType))
            {
                throw new NotImplementedException($"Unsupported type of callback! Callback was: \"{callbackType}\".");
            }

            var callbackCommand = callbackObj.Children().ElementAtOrDefault(1) as JProperty;
            if (callbackCommand == null)
            {
                throw new ArgumentException("Callback data must have more than 1 element!");
            }

            if (!_commands[callbackType].ContainsKey(callbackCommand.Name))
            {
                throw new NotImplementedException($"Unsupported type of callback! Callback was: \"{callbackCommand}\".");
            }

            return _commands[callbackType][callbackCommand.Name];
        }
    }
}
