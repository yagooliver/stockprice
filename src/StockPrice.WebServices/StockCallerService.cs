using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockPrice.WebServices.Interface;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Polly;
using Microsoft.Extensions.Logging;
using StockPrice.WebServices.Models;

namespace StockPrice.WebServices
{
    public class StockCallerService : IStockCallerService
    {
        private const string PREFIX = ".us&f=sd2t2ohlcv&h&e=csv";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<StockCallerService> _logger;

        public StockCallerService(IHttpClientFactory httpClientFactory, ILogger<StockCallerService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<StockInfo?> GetStock(string stock)
        {
            var result = await Policy.Handle<Exception>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync(
                    3,
                    retryAttempt => TimeSpan.FromSeconds(2),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogError(exception, $"Message: {exception.Message} - retryCount: {retryCount}");
                    })
                    .ExecuteAsync(() => GetStockInfo(stock));

            if(result is not null)
                return new StockInfo(result.Split(','));

            return null;
        }

        private async Task<string?> GetStockInfo(string stock)
        {
            var client = _httpClientFactory.CreateClient("stockInfo");
            client.DefaultRequestHeaders.Accept.Clear();

            HttpResponseMessage result = await client.GetAsync($"l/?s={stock}{PREFIX}");

            var response = await result.Content.ReadAsStringAsync();

            return response;
        }
    }
}