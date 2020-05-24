using ChikwamaWallet.Services;
using ChikwamaWallet.ViewModels;
using ChikwamaWallet.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChikwamaWallet
{
    public partial class App : Application
    {
        public App()
        {
            //InitializeComponent();
            var navService = DependencyService.Get<IChikwamaNavService>() as ChikwamaNavService;
            var controller = DependencyService.Get<NewWalletController>() as NewWalletController;
            var walletManager = DependencyService.Get<WalletManager>() as WalletManager;
            var loginPage = new NavigationPage(new LogInPage(navService, controller)
            {
                Title = "Chikwama"

            });

            
            //var accountManager = DependencyService.Get<IAccountManager>() as AccountManager;
            //var walletManager = DependencyService.Get<IAccountManager>() as WalletManager;

            navService.navigation = loginPage.Navigation;
            walletManager = new WalletManager();
            controller = new NewWalletController(walletManager);
       
            //walletManager.CreateWalletAsync();
            MainPage = new LogInPage(navService,controller);
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
