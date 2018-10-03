using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBot.Core.General.Interface
{
    public interface ITracker
    {

        Task<double> GetCurrencyPrice(string currencyName, string referenceCurrency);

    }
}
