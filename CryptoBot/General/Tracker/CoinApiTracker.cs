using CryptoBot.Core.General.Interface;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBot.Core.General.Tracker
{
    /// <summary>
    /// A tracker sourced by https://www.coinapi.io/
    /// </summary>
    public class CoinApiTracker : ITracker
    {

        private class ExchangeRateResponse
        {
            [JsonProperty("time")]
            public DateTime Time { get; set; }

            [JsonProperty("asset_id_base")]
            public string AssetIdBase { get; set; }

            [JsonProperty("asset_id_quote")]
            public string AssetIdQuote { get; set; }

            [JsonProperty("rate")]
            public double Rate { get; set; }
        }

        private const string _ExchangesUri = "https://rest.coinapi.io/v1/exchanges";
        private const string _ExchangeRateUri = "https://rest.coinapi.io/v1/exchangerate/{0}/{1}";

        private readonly Config _config;



        public CoinApiTracker(IConfig config)
        {
            _config = (Config) config;
        }

        public Task<double> GetCurrencyPrice(string currencyName, string referenceCurrency)
        {
            var uri = string.Format(_ExchangeRateUri, currencyName, referenceCurrency);
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-CoinAPI-Key", _config.CoinApiToken);

            var response = client.Execute(request);
            if(response.StatusCode == HttpStatusCode.OK)
            {
                var exchangeRate = JsonConvert.DeserializeObject<ExchangeRateResponse>(response.Content);
                exchangeRate.Rate = Math.Round(exchangeRate.Rate, 8);

                return Task.FromResult(exchangeRate.Rate);
            }
            else
            {
                return Task.FromResult(0.0);
            }
        }
    }
}
