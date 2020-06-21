using ChikwamaWallet.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChikwamaWallet.ViewModels
{
    class BecomeCashPointViewModel: ChikwamaBaseViewModel
    {
        readonly IAccountManager accountsManager;
        readonly IChikwamaLocationCoords myCoords;
        readonly IChikwamaLocationService myLocation;
        readonly NewWalletController controller;
        readonly IChikwamaNavService navService;

        string _CashPointName;
        public string CashPointName
        {
            get { return _CashPointName; }
            set { _CashPointName = value; OnPropertyChanged(); }
        }

        uint _Rate;
        public uint Rate
        {
            get { return _Rate; }
            set { _Rate = value; OnPropertyChanged(); }
        }

        uint _Phone;
        public uint Phone
        {
            get { return _Phone; }
            set { _Phone = value; OnPropertyChanged(); }
        }

        decimal _Cost;
        public decimal Cost
        {
            get { return _Cost; }
            set { _Cost = value; OnPropertyChanged(); }
        }

        double MyLat;
        double MyLong;
        public string DefaultAccountAddress => accountsManager.DefaultAccountAddress;

        public ICommand BecomeCashPointCommand { get; set; }

        public BecomeCashPointViewModel(IChikwamaNavService navService, NewWalletController controller) : base(navService)
        {
            this.controller = controller;
            this.accountsManager = new AccountManager(controller);
            this.navService = navService;

            myLocation = DependencyService.Get<IChikwamaLocationService>();
            myLocation.GetMyLocation();
            BecomeCashPointCommand = new Command(async () => await ExecuteBecomeCashPointCommand());
        }


        async Task ExecuteBecomeCashPointCommand()
        {
            var sec = (int)Math.Round(MyLat * 3600);
            var latdeg = (int)Math.Round(MyLat);
              var latsec = (uint)Math.Abs(sec % 3600);
            var latmin = (uint)latsec /60;

            sec = (int)Math.Round(MyLong * 3600);
            var longdeg = (int)Math.Round(MyLong);
            var longsec = (uint)Math.Abs(sec % 3600);
            var longmin = (uint)longsec /60;


            var result = await accountsManager.BecomeCashPointAsync(DefaultAccountAddress, CashPointName, latdeg, latmin, latsec, longdeg, longmin, longsec,Phone,Rate,2);
            await navService.DisplayAlert("Send Result", $"tx:{result}", "ok", "cancel");

        }
        public override async Task Init()
        {
        }
    }
}
