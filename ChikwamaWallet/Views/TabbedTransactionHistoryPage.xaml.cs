using ChikwamaWallet.ViewModels;
using ChikwamaWallet.Services;
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
    public partial class TabbedTransactionHistoryPage : TabbedPage
    {
        public TabbedTransactionHistoryPage(IChikwamaNavService navService, NewWalletController controller)
        {
            InitializeComponent();

            BindingContext = new TransactionHistoryViewModel(navService, controller);
        }
    }
}