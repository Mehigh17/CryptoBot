using CryptoBot.Commands.Attribute;
using CryptoBot.Commands.Interface;
using CryptoBot.Core.General.Interface;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBot.Core.General.Module
{
    public class CurrencyTrackerModule : IModule
    {

        private ITracker _tracker;

        public CurrencyTrackerModule(ITracker tracker)
        {
            _tracker = tracker;
        }

        [Command("price")]
        public async Task GetPrice(string[] args, SocketMessage msg)
        {
            if(msg != null)
            {
                if(args.Length == 2)
                {
                    var baseCurrency = args[0];
                    var quoteCurrency = args[1];

                    var price = await _tracker.GetCurrencyPrice(baseCurrency, quoteCurrency);
                    await msg.Channel.SendMessageAsync($"The conversion rate between {baseCurrency} and {quoteCurrency} is {price}");
                }
                else
                {
                    await msg.Channel.SendMessageAsync("Too many args.");
                }
            }
        }

    }
}
