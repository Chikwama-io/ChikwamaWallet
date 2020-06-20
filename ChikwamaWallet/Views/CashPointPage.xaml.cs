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
        public CashPointPage()
        {
            InitializeComponent();
            Title = "Find a Cashpoint";
   
        }

    }
}