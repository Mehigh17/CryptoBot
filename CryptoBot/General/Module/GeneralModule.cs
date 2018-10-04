using CryptoBot.Commands.Attribute;
using CryptoBot.Commands.Interface;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBot.Core.General.Module
{
    public class GeneralModule : IModule
    {

        [Command("source")]
        public async Task PrintSource(string[] args, SocketMessage socketMsg)
        {
            if(socketMsg != null)
            {
                await socketMsg.Channel.SendMessageAsync("Prices updates are provided by https://www.coinapi.io/");
            }
        }

    }
}
