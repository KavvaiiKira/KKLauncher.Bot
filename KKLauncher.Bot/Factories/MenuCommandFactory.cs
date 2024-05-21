using KKLauncher.Bot.Commands.MenuCommands;
using KKLauncher.Bot.Constants;

namespace KKLauncher.Bot.Factories
{
    public class MenuCommandFactory
    {
        private Dictionary<string, IMenuCommand> _commands;

        public MenuCommandFactory()
        {
            _commands = new Dictionary<string, IMenuCommand>()
            {
                { CommandConstants.StartCommand, new StartCommand() },
                { CommandConstants.PCsCommand, new PCsListCommand() },
                { CommandConstants.AppsCommand, new AppListCommand() }
            };
        }

        public IMenuCommand Create(string commandText)
        {
            return _commands.ContainsKey(commandText) ?
                _commands[commandText] :
                throw new NotImplementedException($"Unsupported type of Menu command! Command was: \"{commandText}\".");
        }
    }
}
