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

        TransactionModel[] _Sent;
        public TransactionModel[] Sent
        {
            get { return _Sent; }
            set { _Sent = value; OnPropertyChanged(); }
        }

        TransactionModel[] _Received;
        public TransactionModel[] Received
        {
            get { return _Received; }
            set { _Received = value; OnPropertyChanged(); }
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

            var receiving = new List <TransactionModel>();
            var sending = new List<TransactionModel>();

            Transactions = await accountsManager.GetTransactionsAsync();
            foreach (var trans in Transactions)
            {
                if (trans.Inward)
                {

                    var mytrans = new TransactionModel
                    {
                        Sender = trans.Sender,
                        Receiver = trans.Receiver,
                        Amount = trans.Amount,
                        Inward = trans.Inward,
                        Timestamp = trans.Timestamp
                    };
                    receiving.Add(mytrans);
                }
                else 
                {

                    var mytrans = new TransactionModel
                    {
                        Sender = trans.Sender,
                        Receiver = trans.Receiver,
                        Amount = trans.Amount,
                        Inward = trans.Inward,
                        Timestamp = trans.Timestamp
                    };

                    sending.Add(mytrans);
                }
                
            }

            Sent = sending.ToArray();
            Received = receiving.ToArray();
            IsFetching = false;

        }
        public override async Task Init()
        {
        }



    }
}
