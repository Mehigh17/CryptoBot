using CryptoBot.Core.General.Interface;
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace CryptoBot.Core.General
{
    public class Config : IConfig
    {

        [JsonRequired]
        public string BotToken { get; set; }

        [JsonRequired]
        public string CoinApiToken { get; set; }

        public Config()
        {
            
        }

        public async Task Load()
        {
            var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CryptoBot");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var json = await File.ReadAllTextAsync(Path.Combine(directoryPath, "config.json"));

            var config = JsonConvert.DeserializeObject<Config>(json);

            BotToken = config.BotToken;
            CoinApiToken = config.CoinApiToken;
        }

    }
}
