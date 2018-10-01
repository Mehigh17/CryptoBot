using CryptoBot.Core.General;
using CryptoBot.Core.General.Interface;
using CryptoBot.Core.General.Logger;
using System;
using System.Threading.Tasks;
using Unity;

namespace CryptoBot.Core
{
    class Program
    {
        static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        private async Task MainAsync(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<IConfig, Config>();
            container.RegisterType<ILogger, ConsoleLogger>();

            var bot = container.Resolve<Bot>();
            await bot.Start();

            await Task.Delay(-1);
        }

    }
}
