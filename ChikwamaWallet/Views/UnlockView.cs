using Acr.UserDialogs;
using ChikwamaWallet.Services;
using ChikwamaWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Plugin.SecureStorage.Abstractions;

namespace ChikwamaWallet.Views
{
    public class UnlockView : ContentPage
    {
        readonly IUserDialogs userDialogs;
        readonly INavigation navigation;
        public UnlockView()
        {
            var walletManager = DependencyService.Get<IWalletManager>() as WalletManager;

            Title = "Chikwama";
        
            //	Declare	and	initialize	our	Model	Binding	Context
            BindingContext = new UnlockViewModel(walletManager);
            /*
            Uri logo = new Uri("https://drive.google.com/open?id=1x0jzO_a4-w0m9WeVfPPISD4v73Rcyy-4");

            var logoImage = new Image
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Source = FileImageSource.FromUri(logo),
                
            };
            */
            var pinEntry = new Entry
            {
                Placeholder = "Enter Your Pin",
            };
            pinEntry.SetBinding(Entry.TextProperty, "PassCode");

            var unlockButton = new Button
            {
                Text = "Unlock"
            };
            unlockButton.SetBinding(MenuItem.CommandProperty, "UnlockCommand");

           


            this.Content = new ScrollView
            {
                Padding = 10,
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children =
                    {
                        //logoImage,
                        pinEntry,
                        unlockButton
                    }
                }
            };
            
            
        }

     
    }
}