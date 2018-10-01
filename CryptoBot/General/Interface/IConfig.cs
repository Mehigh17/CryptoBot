using System.Threading.Tasks;

namespace CryptoBot.Core.General.Interface
{
    public interface IConfig
    {

        string BotToken { get; set; }

        Task Load();

    }
}
