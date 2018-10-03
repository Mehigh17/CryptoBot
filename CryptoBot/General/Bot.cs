using CryptoBot.Commands.Service;
using CryptoBot.Core.General.Interface;
using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace CryptoBot.Core.General
{
    public class Bot
    {

        public IConfig Config { get; }

        private readonly DiscordSocketClient _client;
        private readonly ILogger _logger;
        private readonly CommandHandlerService _commandHandler;

        public Bot(IConfig config, ILogger logger, CommandHandlerService service)
        {
            Config = config;

            _logger = logger;
            _client = new DiscordSocketClient();
            _commandHandler = service;

            _client.Log += _logger.Log;
            _client.MessageReceived += async (msg) =>
            {
                await _commandHandler.ExecuteCommandAsync(msg);
            };
        }

        public async Task Start()
        {
            await _client.LoginAsync(TokenType.Bot, Config.BotToken);
            await _client.StartAsync();
        }

        public async Task Stop()
        {
            await _client.StopAsync();
        }

    }
}
