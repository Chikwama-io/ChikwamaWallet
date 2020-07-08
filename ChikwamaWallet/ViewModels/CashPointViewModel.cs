using ChikwamaWallet.Models;
using ChikwamaWallet.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ChikwamaWallet.ViewModels
{
    class CashPointViewModel : ChikwamaBaseViewModel
    {
        readonly IAccountManager accountsManager;
        readonly NewWalletController controller;
        readonly IChikwamaNavService navService;

        public Xamarin.Forms.Maps.Map CashPointsMap { get; private set; }

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

        public Position Position { get; set; }

        AccountModel[] _CashPoints;
        public AccountModel[] CashPoints
        {
            get { return _CashPoints; }
            set { _CashPoints = value; OnPropertyChanged(); }
        }
        public string DefaultAccountAddress => accountsManager.DefaultAccountAddress;


        public CashPointViewModel(IChikwamaNavService navService, NewWalletController controller) : base(navService)
        {
            this.controller = controller;
            this.accountsManager = new AccountManager(controller);
            this.navService = navService;

            

            GetCashPoints();

        }

        async void GetCashPoints()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            MyLat = location.Latitude;
            MyLong = location.Longitude;

            CashPoints = await accountsManager.GetCashPointsAsync();

            CashPointsMap = new Xamarin.Forms.Maps.Map();

            if (CashPoints != null)
            {
                foreach (var cashpoint in CashPoints)
                {
                    // Place a pin on the map for each cash point
                    CashPointsMap.Pins.Add(new Pin
                    {
                        Type = PinType.Place,
                        Label = cashpoint.AccountName,
                        Position = new Position(cashpoint.Latitude, cashpoint.Longitude)
                    });
                }
            }
                // Center the map around the list of walks entry's location
                CashPointsMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(MyLat, MyLong), Distance.FromKilometers(1.0)));
            

        }

        public override async Task Init()
        {
        }
    }
}
