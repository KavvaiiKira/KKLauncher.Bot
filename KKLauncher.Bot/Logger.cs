namespace KKLauncher.Bot
{
    public class Logger
    {
        private string _logFilePath;

        private readonly string _errorConst = "[ERROR]";
        private readonly string _infoConst = "[INFORMATION]";
        private readonly string _logPattern = "\nType: {0}\tDateAndTime: {1}\tMessage: {2}";

        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;

            File.AppendAllText(
                _logFilePath,
                string.Format(
                    "Type: {0}\tDateAndTime: {1}\tMessage: {2}",
                    _infoConst,
                    DateTime.Now.ToString(),
                    "Bot logging started.")
                );
        }

        public void WriteError(string message)
        {
            File.AppendAllText(_logFilePath, string.Format(_logPattern, _errorConst, DateTime.Now.ToString(), message));
        }

        public void WriteInfo(string message)
        {
            File.AppendAllText(_logFilePath, string.Format(_logPattern, _infoConst, DateTime.Now.ToString(), message));
        }
    }
}
