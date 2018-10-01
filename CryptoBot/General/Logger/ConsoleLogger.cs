using CryptoBot.Core.General.Interface;
using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBot.Core.General.Logger
{
    public class ConsoleLogger : ILogger
    {
        public Task Log(LogMessage message)
        {
            Console.ForegroundColor = GetSeverityColor(message.Severity);
            Console.WriteLine($"[{DateTime.UtcNow}] {message.Message}");

            return Task.CompletedTask;
        }

        private ConsoleColor GetSeverityColor(LogSeverity severity)
        {
            switch(severity)
            {
                case LogSeverity.Critical:
                    return ConsoleColor.Red;
                case LogSeverity.Debug:
                    return ConsoleColor.Gray;
                case LogSeverity.Error:
                    return ConsoleColor.DarkRed;
                case LogSeverity.Info:
                    return ConsoleColor.White;
                case LogSeverity.Verbose:
                    return ConsoleColor.DarkGray;
                case LogSeverity.Warning:
                    return ConsoleColor.Yellow;
                default:
                    return ConsoleColor.Cyan;
            }
        }

    }
}
