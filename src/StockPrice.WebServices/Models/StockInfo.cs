using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPrice.WebServices.Models
{
    public class StockInfo
    {
        public StockInfo(string[] result)
        {
            Symbol = result[7].Replace("Volume\r\n","");
            Date = DateTime.Parse(result[8]);
            Time = TimeSpan.Parse(result[9]);
            Open = Decimal.Parse(result[10]);
            High = Decimal.Parse(result[11]);
            Low = Decimal.Parse(result[12]);
            Close = Decimal.Parse(result[13]);
            Volume = Int32.Parse(result[14]);
        }
        public StockInfo(string? symbol, DateTime date, TimeSpan time, decimal open, decimal high, decimal low, decimal close, int volume)
        {
            Symbol = symbol;
            Date = date;
            Time = time;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        public string? Symbol { get; }
        public DateTime Date { get; }
        public TimeSpan Time { get; }
        public decimal Open { get; }
        public decimal High { get; }
        public decimal Low { get; }
        public decimal Close { get; }
        public int Volume { get; }
    }
}