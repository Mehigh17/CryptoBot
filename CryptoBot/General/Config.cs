using CryptoBot.Core.General.Interface;
using System.Threading.Tasks;

namespace CryptoBot.Core.General
{
    public class Config : IConfig
    {

        public string BotToken { get; set; }

        public Config()
        {
            
        }

        public Task Load()
        {
            return Task.CompletedTask;
        }

    }
}
