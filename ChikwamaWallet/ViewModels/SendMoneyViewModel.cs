using ChikwamaWallet.Services;
using ChikwamaWallet.Views;
using Plugin.Share.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ChikwamaWallet.ViewModels;

namespace ChikwamaWallet.ViewModels
{
    public class SendMoneyViewModel: ChikwamaBaseViewModel
    {
        decimal _Balance;
        public decimal Balance
        {
            get { return _Balance; }
            set { _Balance = value; OnPropertyChanged(); }
        }

        decimal _BalanceInETH;
        public decimal BalanceInETH
        {
            get { return _BalanceInETH; }
            set { _BalanceInETH = value; OnPropertyChanged(); }
        }

        bool _Sendable;
        public bool Sendable
        {
            get { return _Sendable; }
            set { _Sendable = value; OnPropertyChanged(); }
        }

        string _RecipientAddress;
        public string RecipientAddress
        {
            get { return _RecipientAddress; }
            set { _RecipientAddress = value; OnPropertyChanged(); }
        }

        decimal _SendingAmount;
        public decimal SendingAmount
        {
            get { return _SendingAmount; }
            set { _SendingAmount = value; OnPropertyChanged(); }
        }

        public string DefaultAccountAddress => accountsManager.DefaultAccountAddress;

        readonly IAccountManager accountsManager;
        readonly NewWalletController controller;
        readonly IChikwamaNavService navService;
        IShare share;
        public ICommand ViewHistoryCommand { get; set; }
        public ICommand RefreshBalanceCommand { get; set; }
        public ICommand ScanQRCommand { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand ShareCommand { get; set; }

        public SendMoneyViewModel(IChikwamaNavService navService, NewWalletController controller): base (navService)
        {
            this.controller = controller;
            this.accountsManager = new AccountManager(controller);
            this.navService = navService;
            var radd = controller.GetReciever();
            if (radd != "")
            {
                RecipientAddress = radd.Substring(9);
            }
            ViewHistoryCommand = new Command(async () => await ExecuteViewHistoryCommand());
            RefreshBalanceCommand = new Command(async () => await ExecuteRefreshBalanceCommand());
            ScanQRCommand = new Command(async () => await ExecuteScanQRCommand());
            SendCommand = new Command(async () => await ExecuteSendCommand());
            ShareCommand = new Command(async () => await ExecuteShareCommand());
        }

        async Task UpdateBalance()
        {
            if (IsProcessBusy)
            {

                return;

            }

            IsProcessBusy = true;
            await Task.Delay(100);

            await Task.Run(async delegate
            {
                Balance = await accountsManager.GetTokensAsync(accountsManager.DefaultAccountAddress);
                BalanceInETH = await accountsManager.GetBalanceInETHAsync(accountsManager.DefaultAccountAddress);
            });

            Sendable = BalanceInETH >= 0.01m;
            //Sendable = Balance > 0 && BalanceInETH >= 0.01m;

            IsProcessBusy = false;
        }

  
        
       
        async Task ExecuteShareCommand()
        {
            await share.Share(new ShareMessage
            {
                Title = "My Ethereum Address",
                Text = $"ethereum:{DefaultAccountAddress}"
            });
        }
        

        
  
        async Task ExecuteRefreshBalanceCommand()
        {
            await UpdateBalance();
        }

        public override async Task Init()
        {
        }

 
   

        void ExtractAccountAddress(string qr)
        {
            if (string.IsNullOrWhiteSpace(qr)) return;

            var fragments = qr.Split(new char[] { ':', '?' }, StringSplitOptions.RemoveEmptyEntries);

            RecipientAddress = fragments.Length == 1
                                        ? fragments[0]
                                        : fragments[1];
        }

        
    
        async Task ExecuteScanQRCommand()
        {
            await navService.PushAsync(new NavigationPage(new ScannerPage(navService, controller)));
        }

        
        async Task ExecuteSendCommand()
        {
            if (string.IsNullOrWhiteSpace(RecipientAddress) || SendingAmount <= 0) return;
            var toAddress = RecipientAddress.Trim();

            if (toAddress.Length != DefaultAccountAddress.Length) return;

            //userDialogs.ShowLoading("Sending");
            var result = await accountsManager.TransferAsync(DefaultAccountAddress, toAddress, SendingAmount);
            await navService.DisplayAlert("Send Result",$"tx:{result}", "ok","cancel");
            //userDialogs.HideLoading();
            await UpdateBalance();
            
        }

 
        
    
        async Task ExecuteViewHistoryCommand()
        {
            await navService.PushAsync(new NavigationPage(new TransactionHistoryPage(navService, controller)));
        }
    }
}
