using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChikwamaWallet.Views;
using ChikwamaWallet.ViewModels;
using ChikwamaWallet.Services;

namespace ChikwamaWallet
{
    public partial class App : Application
    {
        public App()
        {
            //InitializeComponent();

            var loginPage = new NavigationPage(new LogInPage()
            {
                Title = "Chikwama"

            });

            var navService = DependencyService.Get<IChikwamaNavService>() as ChikwamaNavService;
            //var accountManager = DependencyService.Get<IAccountManager>() as AccountManager;
            //var walletManager = DependencyService.Get<IAccountManager>() as WalletManager;

            navService.navigation = loginPage.Navigation;

            navService.RegisterViewMapping(typeof(PasscodeViewModel), typeof(PasscodePage));
            navService.RegisterViewMapping(typeof(PassphraseViewModel), typeof(PassphraseConfirmationPage));
            navService.RegisterViewMapping(typeof(WalletViewModel), typeof(WalletPage));
            navService.RegisterViewMapping(typeof(LoginViewModel), typeof(LogInPage));
            navService.RegisterViewMapping(typeof(AccountViewModel), typeof(AccountPage));
            navService.RegisterViewMapping(typeof(TokenEntryViewModel), typeof(TokenEntryPage));
            navService.RegisterViewMapping(typeof(TokenDetailsViewModel), typeof(TokenDetailsPage));

            //walletManager.CreateWalletAsync();
            MainPage = new LogInPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
