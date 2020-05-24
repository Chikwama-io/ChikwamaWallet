using ChikwamaWallet.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Nethereum.HdWallet;
using ChikwamaWallet.Views;
using Nethereum.KeyStore.Model;

namespace ChikwamaWallet.ViewModels
{
    public partial class LoginViewModel : ChikwamaBaseViewModel
    {
        string _Passcode;
        public string Passcode
        {
            get { return _Passcode; }
            set { _Passcode = value; OnPropertyChanged(); }
        }

        private NewWalletController controller;

        private IWalletManager walletManager;

        private Wallet wallet;

        private IChikwamaNavService _navService { get; set; }
        public ICommand UnlockCommand { get; set; }
        public ICommand CreateCommand { get; set; }


        
        public LoginViewModel(IChikwamaNavService navService, NewWalletController controller) : base(navService)
        {
            this.controller = controller;           
            _navService = navService;
            walletManager = new WalletManager();
            CreateCommand = new Command(async () => await ExecuteCreateWallet());
            UnlockCommand = new Command(async () => await ExecuteUnlockCommand());
        }


        public async Task ExecuteCreateWallet()
        {
            controller = new NewWalletController(walletManager);

            await _navService.PushAsync(new NavigationPage(new PasscodePage(controller, _navService)));
        }
           

        public async Task ExecuteUnlockCommand()
        {
            if (IsProcessBusy)
            {

                return;

            }

            IsProcessBusy = true;

                controller = new NewWalletController(walletManager);

                var unlocked = await controller.UnlockWallet(Passcode);

                if (unlocked == false)
                {
                    await _navService.DisplayAlert("Invalid PIN", "Invalid PIN, Have you created a wallet on this device?", "Retry", "Cancel");
                    IsProcessBusy = false;
                    return;
                }

                try
                {
                    await _navService.PushAsync(new NavigationPage(new MainPage(_navService, controller)));
                }
                finally
                {
                    IsProcessBusy = false;
                }
            }    
        

        public override async Task Init()
        {
        }
    }
	
}

