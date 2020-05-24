using ChikwamaWallet.Services;
using ChikwamaWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChikwamaWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasscodePage : ContentPage
    {
        private NewWalletController controller;
        public PasscodePage(NewWalletController controller, IChikwamaNavService navService)
        {

            InitializeComponent();
            this.controller = controller;
            BindingContext = new PasscodeViewModel(controller, navService);
        }
    }
}