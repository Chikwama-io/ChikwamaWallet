using ChikwamaWallet.Services;
using ChikwamaWallet.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChikwamaWallet.ViewModels
{

    public class PasscodeViewModel : ChikwamaBaseViewModel
    {
        public string Passcode { get; set; }
        private IChikwamaNavService _navService { get; set; }

        private IWalletManager walletManager;

        readonly NewWalletController controller;
        public ICommand ContinueCommand { get; set; }

        public PasscodeViewModel(NewWalletController controller, IChikwamaNavService navService) : base(navService)
        {
            walletManager = new WalletManager();
            _navService = navService;
            this.controller = controller;
            
            ContinueCommand = new Command(async () => await ExecuteContinue());
        }

        public async Task ExecuteContinue()
        {
            controller.SetPasscode(Passcode);
            await _navService.PushAsync(new NavigationPage(new ConfirmPasscodePage(controller, _navService)));
        }
        public override async Task Init()
        {
        }
    }
}
