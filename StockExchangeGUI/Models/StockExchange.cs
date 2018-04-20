using Prism.Mvvm;
using System;

namespace StockExchangeGUI.Models
{
    public class StockExchange : BindableBase
    {
        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set { SetProperty(ref _symbol, value); }
        }

        private long _price;
        public long Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        private int _volume;
        public int Volume
        {
            get { return _volume; }
            set { SetProperty(ref _volume, value); }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { SetProperty(ref _dateTime, value); }
        }
    }
}
