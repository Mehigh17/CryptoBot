using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBot.Core.General.Interface
{
    public interface ILogger
    {

        Task Log(LogMessage message);

    }
}
