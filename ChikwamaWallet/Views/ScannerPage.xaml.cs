using Android.Arch.Lifecycle;
using ChikwamaWallet.Models;
using ChikwamaWallet.Services;
using ChikwamaWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;

namespace ChikwamaWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {
        IChikwamaNavService navService;
        NewWalletController controller;
        public ScannerPage(IChikwamaNavService navService, NewWalletController controller)
        {
            Title = "QRCode Scanner";
            this.controller = controller;
            this.navService = navService;

            InitializeComponent();
            BindingContext = new ScannerViewModel(navService, controller);

            scannerView.Options = new MobileBarcodeScanningOptions()
            {
                UseFrontCameraIfAvailable = false, //update later to come from settings
                PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
                TryHarder = true,
                AutoRotate = false,
                TryInverted = true,
                DelayBetweenContinuousScans = 2000
            };
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            scannerView.IsScanning = true;
            scannerView.AutoFocus();
            scannerView.OnScanResult += ScannerView_OnScanResult;
        }

        private void ScannerView_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(delegate
            {
                scannerView.IsAnalyzing = false;
                scannerView.IsScanning = false;

                controller.SetRecipient(result.Text);
                navService.PushAsync(new NavigationPage(new SendMoneyPage(navService, controller)));
            });
        }

        protected override void OnDisappearing()
        {
            scannerView.IsScanning = false;
            scannerView.OnScanResult -= ScannerView_OnScanResult;
            base.OnDisappearing();
        }

    




    }
}