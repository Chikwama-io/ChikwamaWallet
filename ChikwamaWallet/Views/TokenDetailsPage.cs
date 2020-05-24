using ChikwamaWallet.Models;
using ChikwamaWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using ChikwamaWallet.Services;

namespace ChikwamaWallet.Views
{
    public class TokenDetailsPage : ContentPage
    {
        TokenDetailsViewModel _viewModel
        {
            get { return BindingContext as TokenDetailsViewModel; }
        }


        ZXingBarcodeImageView barcode;
        public TokenDetailsPage()
        {

            Title = "Token Details";

            //	Declare	and	initialize	our	Model	Binding	Context

            BindingContext = new TokenDetailsViewModel(DependencyService.Get<IChikwamaNavService>());

            var tokenNameLabel = new Label()
            {
                FontSize = 28,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Accent
            };
            tokenNameLabel.SetBinding(Label.TextProperty, "Token.TokenName");

            
            barcode = new ZXingBarcodeImageView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = 10,
                AutomationId = "zxingBarcodeImageView",
                BarcodeValue = _viewModel.Token.TokenAddress
            };
            barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            barcode.BarcodeOptions.Width = 100;
            barcode.BarcodeOptions.Height = 100;
            barcode.BarcodeOptions.Margin = 0;


            var tokenAddressLabel = new Label()
            {
                FontSize = 14,
                FontAttributes = FontAttributes.None,
                TextColor = Color.Black
            };
            tokenAddressLabel.SetBinding(Label.TextProperty, "Token.TokenAddress");

            var tokenSymbolLabel = new Label()
            {
                FontSize = 14,
                FontAttributes = FontAttributes.None,
                TextColor = Color.Black
            };
            tokenSymbolLabel.SetBinding(Label.TextProperty, "Token.TokenSymbol");

            var tokenBalanceLabel = new Label()
            {
                FontSize = 14,
                FontAttributes = FontAttributes.None,
                TextColor = Color.Black
            };
            tokenBalanceLabel.SetBinding(Label.TextProperty, "Token.TokenBalance");


            this.Content = new ScrollView
            {
                Padding = 10,
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children =
                    {                                                            
                        tokenNameLabel,
                        barcode,
                        tokenAddressLabel,
                        tokenSymbolLabel,
                        tokenBalanceLabel,                                                              
                                                                
                    }
                }
            };



        }
     


    }
}