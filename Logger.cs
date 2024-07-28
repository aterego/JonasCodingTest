using System;
using System.IO;

namespace Logger
{
    public class Logger : ILogger
    {
        private readonly string _logFilePath;

        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public void LogError(string message, Exception ex)
        {
            Log("ERROR", $"{message} - Exception: {ex.Message}");
        }

        private void Log(string logLevel, string message)
        {
            try
            {
                var logMessage = $"{DateTime.Now.ToString("o")} [{logLevel}] {message}";
                File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
            }
            catch
            {
                // Handle any exceptions that occur during logging
            }
        }
    }
}
