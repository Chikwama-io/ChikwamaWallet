using System;
using System.Collections.Generic;
using System.Text;


using ChikwamaWallet.Services;
using System.Windows.Input;
using ChikwamaWallet.ViewModels;
using Xamarin.Forms;
using Acr.UserDialogs;
using Prism;
using ChikwamaWallet.Views;

namespace ChikwamaWallet.ViewModels
{

    public partial class UnlockViewModel : ChikwamaBaseViewModel
    {
        string _Passcode;
        public string Passcode
        {
            get { return _Passcode; }
            set {
                _Passcode = value;
            }
        }

        readonly IWalletManager walletManager;
        readonly IUserDialogs userDialogs;
        readonly INavigation navigation;
        public UnlockViewModel(
            IWalletManager walletManager
        )
        {
            this.walletManager = walletManager;

        }

        ICommand _UnlockCommand;
        public ICommand UnlockCommand
        {
            get { return (_UnlockCommand = _UnlockCommand ?? new Command<object>(ExecuteUnlockCommand, CanExecuteUnlockCommand)); }
        }
        bool CanExecuteUnlockCommand(object obj) => true;
        async void ExecuteUnlockCommand(object obj)
        {
            var unlocked = await walletManager.UnlockWalletAsync(Passcode);

            if (unlocked == false)
            {
                userDialogs.Toast("Invalid PIN.");

                return;
            }

            await navigation.PushAsync(new WalletPage());
            //await navigator.NavigateAsync(NavigationKeys.UnlockWallet);
        }
    }

    partial class UnlockViewModel
    {
        ICommand _CreateCommand;
        public ICommand CreateCommand
        {
            get { return (_CreateCommand = _CreateCommand ?? new Command<object>(ExecuteCreateCommand, CanExecuteCreateCommand)); }
        }
        bool CanExecuteCreateCommand(object obj) => true;
        async void ExecuteCreateCommand(object obj)
        {
            await walletManager.CreateWalletAsync();
            //var result = await navigator.NavigateAsync(NavigationKeys.CreateWallet);
        }

        ICommand _RecoverCommand;
        public ICommand RecoverCommand
        {
            get { return (_RecoverCommand = _RecoverCommand ?? new Command<object>(ExecuteRecoverCommand, CanExecuteRecoverCommand)); }
        }
        bool CanExecuteRecoverCommand(object obj) => true;
        async void ExecuteRecoverCommand(object obj)
        {
            //walletManager.RestoreWallet(walletManager.);
           // await navigator.NavigateAsync(NavigationKeys.RecoverWallet);
        }
    }
}
