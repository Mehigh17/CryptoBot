using CryptoBot.Core.General.Interface;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBot.Core.General
{
    public class Bot
    {

        public IConfig Config { get; }

        private readonly DiscordSocketClient _client;
        private readonly ILogger _logger;

        public Bot(IConfig config, ILogger logger)
        {
            Config = config;

            _logger = logger;
            _client = new DiscordSocketClient();
        }

        public async Task Start()
        {
            _client.Log += _logger.Log;
            _client.Ready += async () =>
            {

            };
            //_client.MessageReceived += OnMessageReceived;

            await _client.LoginAsync(TokenType.Bot, Config.BotToken);
            await _client.StartAsync();
        }

        public async Task Stop()
        {

        }


    }
}
