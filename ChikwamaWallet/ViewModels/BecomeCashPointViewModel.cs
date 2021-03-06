﻿using ChikwamaWallet.Services;
using Nethereum.Hex.HexTypes;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChikwamaWallet.ViewModels
{
    class BecomeCashPointViewModel: ChikwamaBaseViewModel
    {
        readonly IAccountManager accountsManager;
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


            BecomeCashPointCommand = new Command(async () => await ExecuteBecomeCashPointCommand());
        }


        async Task ExecuteBecomeCashPointCommand()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            MyLat = location.Latitude;
            MyLong = location.Longitude;

            BigInteger latitude = Nethereum.Util.UnitConversion.Convert.ToWei(MyLat);
            BigInteger longitude = Nethereum.Util.UnitConversion.Convert.ToWei(MyLong);



            var result = await accountsManager.BecomeCashPointAsync(DefaultAccountAddress, CashPointName, latitude, longitude,Phone,Rate,2);
            await navService.DisplayAlert("Send Result", $"tx:{result}", "ok", "cancel");

        }
        public override async Task Init()
        {
        }
    }
}
