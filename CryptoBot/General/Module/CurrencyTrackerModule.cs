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

        private readonly ITracker _tracker;

        public CurrencyTrackerModule(ITracker tracker)
        {
            _tracker = tracker;
        }

        [Command("price")]
        public async Task GetPrice(string[] args, SocketMessage msg)
        {
            if(msg != null)
            {
                if(args.Length == 1)
                {
                    var baseCurrency = args[0].ToUpper();
                    var quoteCurrency = "USD";

                    var price = await _tracker.GetCurrencyPrice(baseCurrency, quoteCurrency);
                    await msg.Channel.SendMessageAsync($"The conversion rate is {price} {quoteCurrency} per {baseCurrency}");
                }
                else if(args.Length == 2)
                {
                    var baseCurrency = args[0].ToUpper();
                    var quoteCurrency = args[1].ToUpper();

                    var price = await _tracker.GetCurrencyPrice(baseCurrency, quoteCurrency);
                    await msg.Channel.SendMessageAsync($"The conversion rate is {price} {quoteCurrency} per {baseCurrency}");
                }
                else if(args.Length == 3)
                {
                    var baseCurrency = args[0].ToUpper();
                    var quoteCurrency = args[1].ToUpper();
                    double amount;

                    if(double.TryParse(args[2], out amount))
                    {
                        var price = await _tracker.GetCurrencyPrice(baseCurrency, quoteCurrency);
                        double convertedAmount = amount * price;


                        await msg.Channel.SendMessageAsync($"{amount} {baseCurrency} converts to {convertedAmount} {quoteCurrency}");
                    }
                    else
                    {
                        await msg.Channel.SendMessageAsync($"Your numbers are incorrectly formatted.");
                    }
                }
                else
                {
                    await msg.Channel.SendMessageAsync("Invalid arguments.");
                }
            }
        }

    }
}
