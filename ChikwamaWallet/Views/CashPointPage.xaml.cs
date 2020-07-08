using ChikwamaWallet.Services;
using ChikwamaWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChikwamaWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CashPointPage : ContentPage
    {
        public CashPointPage(IChikwamaNavService navService, NewWalletController controller)
        {
            InitializeComponent();
            BindingContext = new CashPointViewModel(navService, controller);


        }

    }
}