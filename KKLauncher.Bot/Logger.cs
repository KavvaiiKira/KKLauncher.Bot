namespace KKLauncher.Bot
{
    public static class Logger
    {
        private static string _logFilePath;

        private static readonly string _errorConst = "[ERROR]";
        private static readonly string _infoConst = "[INFORMATION]";
        private static readonly string _logPattern = "\nType: {0}\tDateAndTime: {1}\tMessage: {2}";

        static Logger()
        {
            var logDir = Path.Combine(Environment.CurrentDirectory, "logs");

            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            _logFilePath = Path.Combine(logDir, "kk-bot.log");

            if (!File.Exists(_logFilePath))
            {
                File.Create(_logFilePath).Close();
            }

            File.AppendAllText(
                _logFilePath,
                string.Format(
                    "Type: {0}\tDateAndTime: {1}\tMessage: {2}",
                    _infoConst,
                    DateTime.Now.ToString(),
                    "Bot logging started.")
                );
        }

        public static async Task WriteError(string message)
        {
            await File.AppendAllTextAsync(_logFilePath, string.Format(_logPattern, _errorConst, DateTime.Now.ToString(), message));
        }

        public static async Task WriteInfo(string message)
        {
            await File.AppendAllTextAsync(_logFilePath, string.Format(_logPattern, _infoConst, DateTime.Now.ToString(), message));
        }
    }
}
