using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockPrice.WebServices.Models;

namespace StockPrice.WebServices.Interface
{
    public interface IStockCallerService
    {
        Task<StockInfo?> GetStock(string stock);
    }
}