﻿using ChikwamaWallet.Services;
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
    public partial class TransactionHistoryPage : ContentPage
    {
        public TransactionHistoryPage(IChikwamaNavService navService, NewWalletController controller)
        {
            InitializeComponent();

           BindingContext = new TransactionHistoryViewModel(navService, controller);
        }
    }
}