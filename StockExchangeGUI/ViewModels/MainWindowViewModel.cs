using Extensibility;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using StockExchangeGUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using WebSocket4Net;

namespace StockExchangeGUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region fields

        private WebSocket _websocketClient;
        public static string URL = "ws://localhost:65055/chat";
        
        private string _selectedSymbol;
        private string _connectionLabel;
        private bool _isEnableSubscribedButton;

        private ObservableCollection<StockExchange> _stockDatas;
        private ObservableCollection<string> _symbolsCollection;

        private readonly INotificationService _notificationService;
        
        #endregion

        #region Properties

        public MainWindowViewModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
            _stockDatas = new ObservableCollection<StockExchange>();
            Connection();
        }

        public string ConnectionLabel
        {
            get { return _connectionLabel; }
            set { SetProperty(ref _connectionLabel, value); }
        }
        
        public bool IsEnableSubscribedButton
        {
            get { return _isEnableSubscribedButton; }
            set { SetProperty(ref _isEnableSubscribedButton, value); }
        }

        public string SelectedSymbol
        {
            get { return _selectedSymbol; }
            set { SetProperty(ref _selectedSymbol, value); }
        }

        public ObservableCollection<StockExchange> StockDatas
        {
            get { return _stockDatas; }
            set { SetProperty(ref _stockDatas, value); }
        }

        public ObservableCollection<string> SymbolsCollection
        {
            get { return _symbolsCollection; }
            set { SetProperty(ref _symbolsCollection, value); }
        }

        #endregion

        #region Commands

        public ICommand SubscribedToTheSymbolCommand => new DelegateCommand(OnSubscribed);
        public ICommand ExitCommand => new DelegateCommand<Window>(OnExitCommand);
        
        #endregion

        #region Methods
        
        private async void Connection()
        {
            SocketHandler(URL);
            _websocketClient.Open();
            IsEnableSubscribedButton = true;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:65055/");
                client.DefaultRequestHeaders.Accept.Clear();

                try
                {
                    var response = await client.GetAsync("api/symbols");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonDatas = await response.Content.ReadAsStringAsync();

                        var deserializeSymbols = JsonConvert.DeserializeObject<List<string>>(jsonDatas);

                        SymbolsCollection = new ObservableCollection<string>(deserializeSymbols);
                    }
                }
                catch (Exception)
                {
                    ConnectionLabel = "Please connect to the server ;)";
                    IsEnableSubscribedButton = false;
                }
            }
        }

        private void OnSubscribed()
        {
            var symbol = SelectedSymbol;
            if (symbol != null)
            {
                _websocketClient.Send(symbol);
                SymbolsCollection.Remove(SelectedSymbol);
            }
            else if(SymbolsCollection.Count == 0)
            {
                IsEnableSubscribedButton = false;
                _notificationService.SocketError("You choose all symbols");
            }
            else
            {
                _notificationService.WrongInput();
            }
        }
        
        private void OnExitCommand(Window window)
        {
            _websocketClient.Close();
            if (window != null)
            {
                window.Close();
            }
        }
            
        private void AddOrUpdateStockExchange(StockExchange stock)
        {
            if (StockDatas.Any(item => item.Symbol == stock.Symbol))
            {
                UpdateStockItem(stock);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(delegate ()
                {
                    StockDatas.Add(stock);
                });
            }
        }

        private void UpdateStockItem(StockExchange stock)
        {
            var addedStock = StockDatas.FirstOrDefault(item => item.Symbol == stock.Symbol);
            if (addedStock != null)
            {
                int index = StockDatas.IndexOf(addedStock);
                StockDatas[index].Price = stock.Price;
                StockDatas[index].Volume = stock.Volume;
                StockDatas[index].DateTime = stock.DateTime;
            }
        }

        #endregion

        #region WebSocket

        public void SocketHandler(string url)
        {
            _websocketClient = new WebSocket(url);

            _websocketClient.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocketClient_Error);
            _websocketClient.Opened += new EventHandler(websocketClient_Opened);
            _websocketClient.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocketClient_MessageReceived);
        }

        private void websocketClient_Opened(object sender, EventArgs e)
        {
            ConnectionLabel = "You are successfull connected to the server ;)";   
        }

        private void websocketClient_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var stockData = JsonConvert.DeserializeObject<StockExchange>(e.Message);
            var deserializeStockData = JsonConvert.DeserializeObject<StockExchange>(e.Message);

            AddOrUpdateStockExchange(deserializeStockData);
        }

        private void websocketClient_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            _notificationService.SocketError(e.Exception.Message);
        }

        #endregion             
    }
}