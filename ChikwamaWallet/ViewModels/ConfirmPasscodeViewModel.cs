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
    public class ConfirmPasscodeViewModel: ChikwamaBaseViewModel
    {
        private IChikwamaNavService _navService { get; set; }
        public string Passcode { get; set; }
        public ICommand ContinueCommand { get; set; }

        readonly NewWalletController controller;
        public ConfirmPasscodeViewModel(NewWalletController controller, IChikwamaNavService navService) : base(navService)
        {
            this.controller = controller;
            _navService = navService;

            ContinueCommand = new Command(async () => await ExecuteContinueCommand(Passcode));
        }

        async Task ExecuteContinueCommand(string passcode)
        {
            var verified = controller.VerifyPasscode(passcode);

            if (false == verified)
            {
                await _navService.DisplayAlert("Invalid Pin", "Invalid PIN confirmation","ok","cancel");
                //userDialogs.Toast("Invalid PIN confirmation");

                return;
            }
            await controller.CreateWalletAsync();
            await _navService.PushAsync(new NavigationPage(new PassphrasePage(controller, _navService)));
        }

        public override async Task Init()
        {
        }
    }
}
