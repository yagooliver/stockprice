using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockPrice.WebServices;
using StockPrice.WebServices.Interface;

namespace StockPrice.Api
{
    public static class DependencyInjectionResolver
    {
        public static void RegisterServices(this IServiceCollection services, ConfigurationManager config)
        {
            services.AddHttpClient("stockInfo", c=>
            {
                c.BaseAddress = new Uri(config.GetSection("Url").Value);
            });
            services.AddScoped<IStockCallerService, StockCallerService>();
        }        
    }
}