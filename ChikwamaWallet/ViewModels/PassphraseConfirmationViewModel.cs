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
    public class PassphraseConfirmationViewModel: ChikwamaBaseViewModel
    {

        public ICommand ContinueCommand { get; set; }
        readonly NewWalletController controller;
        IChikwamaNavService _navService;

        string _SecondWord;
        public string SecondWord
        {
            get { return _SecondWord; }
            set { _SecondWord = value; OnPropertyChanged(); }
        }

        string _FifthWord;
        public string FifthWord
        {
            get { return _FifthWord; }
            set { _FifthWord = value; OnPropertyChanged(); }    
        }

        string _NinethWord;
        public string NinethWord
        {
            get { return _NinethWord; }
            set { _NinethWord = value; OnPropertyChanged(); }
        }
        public PassphraseConfirmationViewModel(NewWalletController controller, IChikwamaNavService navService) : base(navService)
        {
            this.controller = controller;
            _navService = navService;

            ContinueCommand = new Command(async () => await ExecuteContinueCommand());
        }


        async Task ExecuteContinueCommand()
        {
            if (IsProcessBusy)
            {

                return;

            }

            IsProcessBusy = true;

            var words = controller.GetSeedWords();
            var isValid = string.Equals(SecondWord, words[1], StringComparison.InvariantCultureIgnoreCase)
                                && string.Equals(FifthWord, words[4], StringComparison.InvariantCultureIgnoreCase)
                                && string.Equals(NinethWord, words[8], StringComparison.InvariantCultureIgnoreCase);

            if (false == isValid)
            {
                await _navService.DisplayAlert("Invalid","Invalid words confirmation.","Try Again","Cancel");
                return;
            }

            try
            {
                await controller.SaveWalletAsync();

                await _navService.PushAsync(new NavigationPage(new LogInPage(_navService, controller)));
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
