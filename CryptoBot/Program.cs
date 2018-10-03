using CryptoBot.Commands.Service;
using CryptoBot.Core.General;
using CryptoBot.Core.General.Interface;
using CryptoBot.Core.General.Logger;
using CryptoBot.Core.General.Module;
using CryptoBot.Core.General.Tracker;
using System.Threading.Tasks;
using Unity;

namespace CryptoBot.Core
{
    class Program
    {
        static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        private async Task MainAsync(string[] args)
        {
            var config = new Config();
            await config.Load();

            var commandService = new CommandHandlerService();

            var container = new UnityContainer();
            container.RegisterType<ILogger, ConsoleLogger>();

            container.RegisterInstance(commandService);
            container.RegisterInstance<IConfig>(config);

            var apiTracker = container.Resolve<CoinApiTracker>();

            container.RegisterInstance<ITracker>(apiTracker);

            commandService.RegisterModule(new GeneralModule());
            var trackerModule = container.Resolve<CurrencyTrackerModule>();
            commandService.RegisterModule(trackerModule);
            
            var bot = container.Resolve<Bot>();

            await bot.Start();
            await Task.Delay(-1);
        }

    }
}
