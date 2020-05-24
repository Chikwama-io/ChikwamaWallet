using ChikwamaWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZXing;
using Xamarin.Forms;
using ChikwamaWallet.Models;
using Android.Arch.Lifecycle;
using ChikwamaWallet.Services;

namespace ChikwamaWallet.Views
{
    public class TokenEntryPage : ContentPage
    {
		public string scannedAddress;

		TokenEntryViewModel _viewModel

		{

			get
			{
				return BindingContext as TokenEntryViewModel;
			}

		}

		public TokenEntryPage()
        {
			//	Set	the	Content	Page	Title	
			Title = "New Token Entry";

			//	Declare	and	initialize	our	Model	Binding	Context
			BindingContext = new TokenEntryViewModel(DependencyService.Get<IChikwamaNavService>());

			//	Define	our	New	Token	fields
			var tokenName = new EntryCell
			{
				Label = "Name:",
				Placeholder = "Ether"
			};
			tokenName.SetBinding(EntryCell.TextProperty, "TokenName", BindingMode.TwoWay);

			var tokenAddress = new EntryCell
			{
				Label = "Account:",
				Text = scannedAddress
			};
			tokenAddress.SetBinding(EntryCell.TextProperty, "TokenAddress", BindingMode.TwoWay);

			var qrCode = new TextCell
			{
				Text = "Scan QRCode"
			};

			qrCode.Tapped += ScanCustomPage;

			async void ScanCustomPage(object sender, EventArgs e)
			{
			}

			var tokenSymbol = new EntryCell
			{
				Label = "Symbol:",
				Placeholder = "Eth",
			};
			tokenSymbol.SetBinding(EntryCell.TextProperty, "TokenSymbol", BindingMode.TwoWay);

			//	Define	our	TableView
			Content = new TableView
			{
				Intent = TableIntent.Form,
				Root = new TableRoot
									{
									new TableSection()
									{
									tokenName,
									tokenAddress,
									qrCode,
									tokenSymbol								
									}
									}
			};

			
			var saveToken = new ToolbarItem
			{
				Text = "Save"
			};
			saveToken.SetBinding(MenuItem.CommandProperty,"SaveCommand");

			ToolbarItems.Add(saveToken);

		}

		protected override void OnAppearing()
		{
		}
	}

	

}