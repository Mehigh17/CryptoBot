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

        [Command("test")]
        public async Task PrintToConsole(string[] args, SocketMessage socketMsg)
        {
            if(socketMsg != null)
            {
                await socketMsg.Channel.SendMessageAsync("Yes it is working.");
            }
        }

    }
}
