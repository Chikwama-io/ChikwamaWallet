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
    public partial class PassphraseViewModel: ChikwamaBaseViewModel
    {
        IChikwamaNavService _navService;
        public string Words { get; set; }

        public ICommand ContinueCommand { get; set; }
        readonly NewWalletController controller;

        public PassphraseViewModel(NewWalletController controller, IChikwamaNavService navService): base(navService)
        {
            this.controller = controller;
            _navService = navService;

            Words = string.Join(" ", controller.GetSeedWords());
            ContinueCommand = new Command(async () => await ExecuteContinueCommand());
        }

        async Task ExecuteContinueCommand()
        {
            await _navService.PushAsync(new NavigationPage(new PassphraseConfirmationPage(controller, _navService)));
        }
        public override async Task Init()
        {
            
        }
    }
}
