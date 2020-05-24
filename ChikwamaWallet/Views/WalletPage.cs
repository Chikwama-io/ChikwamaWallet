using ChikwamaWallet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using ChikwamaWallet.ViewModels;
using ChikwamaWallet.DataTemplates;
using ChikwamaWallet.Services;
using ChikwamaWallet.ValueConverters;

namespace ChikwamaWallet.Views
{
    public class WalletPage : ContentPage
    {

		WalletViewModel _viewModel
		{

			get
			{
				return BindingContext as WalletViewModel;
			}

		}

		public WalletPage()
        {
			var newToken = new ToolbarItem
			{
				Text = "Add	Token"
			};
			newToken.SetBinding(ToolbarItem.CommandProperty, "CreateNewToken");

			ToolbarItems.Add(newToken);
			//	Declare	and	initialize	our	Model	Binding	Context
			BindingContext = new WalletViewModel(DependencyService.Get<IChikwamaNavService>());

		
			var tokensList = new ListView

			{
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate(typeof(ChikwamaCellDataTemplate)),
				SeparatorColor = Color.FromHex("#ddd"),
			};

			//	Set	the	Binding	property for	our tokens
			tokensList.SetBinding(ItemsView<Cell>.ItemsSourceProperty, "tokens");
			tokensList.SetBinding(ItemsView<Cell>.IsVisibleProperty, "IsProcessBusy", converter: new BooleanConverter());

			//	Set	up	our	event	handler
			tokensList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
			{
				var item = (WalletModel)e.Item;
				if (item == null) return;
				_viewModel.ChikwamaTokenDetails.Execute(item);
				item = null;
			};
			Content = tokensList;

			// Declare our Progress Label
			var progressLabel = new Label()
			{
				FontSize = 14,
				FontAttributes = FontAttributes.Bold,
				TextColor = Color.Black,
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "Loading Chikwama Tokens..."
			};

			// Apply PlatformEffects to our Progress Label
			progressLabel.Effects.Add(Effect.Resolve("com.geniesoftstudios.LabelShadowEffect"));

			// Instantiate and initialise our Activity Indicator.
			var activityIndicator = new ActivityIndicator()
			{
				IsRunning = true
			};
			var progressIndicator = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {
								activityIndicator,progressLabel
							}
			};
			progressIndicator.SetBinding(StackLayout.IsVisibleProperty, "IsProcessBusy");



			// Set the Binding property for our walks Entries
			tokensList.SetBinding(ItemsView<Cell>.IsVisibleProperty, "IsProcessBusy", converter: new BooleanConverter());
			
			

			var mainLayout = new StackLayout
			{
				Children =
				{
					tokensList,
					progressIndicator
				}
			};

			Content = mainLayout;

		}

		protected override async void OnAppearing()

		{

			base.OnAppearing();
			//	Initialize	our	WalletViewModel
			if (_viewModel != null)
				await _viewModel.Init();

		}
	}
}