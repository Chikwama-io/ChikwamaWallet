using ChikwamaWallet.Models;
using ChikwamaWallet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChikwamaWallet.ViewModels
{
    public class TransactionHistoryViewModel : ChikwamaBaseViewModel
    {
        bool _IsFetching;
        public bool IsFetching
        {
            get { return _IsFetching; }
            set { _IsFetching = value; OnPropertyChanged(); }
        }

        TransactionModel[] _Transactions;
        public TransactionModel[] Transactions
        {
            get { return _Transactions; }
            set { _Transactions = value; OnPropertyChanged();}
        }


        readonly IAccountManager accountsManager;
        readonly NewWalletController controller;
        readonly IChikwamaNavService navService;


        public TransactionHistoryViewModel(IChikwamaNavService navService, NewWalletController controller) : base(navService)
        {
            this.controller = controller;
            this.accountsManager = new AccountManager(controller);
            this.navService = navService;
            LoadData();
        }

        async void LoadData()
        {
            IsFetching = true;

            //var receiving = await accountsManager.GetTransactionsAsync();
            //var sending = await accountsManager.GetTransactionsAsync(true);

             Transactions = await accountsManager.GetTransactionsAsync();
           
            IsFetching = false;
        }
        public override async Task Init()
        {
        }



    }
}
