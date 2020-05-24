using Android.Arch.Lifecycle;
using ChikwamaWallet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using ChikwamaWallet.Services;

namespace ChikwamaWallet.ViewModels
{
	public class TokenEntryViewModel : ChikwamaBaseViewModel
	{		

		string _tokenName;
		public string TokenName
		{
			get { return _tokenName; }
			set
			{
				_tokenName = value;
				OnPropertyChanged();
				SaveCommand.ChangeCanExecute();
			}
		}

		string _tokenAddress;
		public string TokenAddress
		{
			get { return _tokenAddress; }
			set
			{
				_tokenAddress = value;
				OnPropertyChanged();
			}
		}

		string _tokenSymbol;
		public string TokenSymbol
		{
			get { return _tokenSymbol; }
			set
			{
				_tokenSymbol = value;
				OnPropertyChanged();
			}
		}


		public TokenEntryViewModel(IChikwamaNavService navService) : base(navService)
		{
			TokenName = "Ethereum";
			TokenAddress = "0x9265C494eCEe37A2a7Aa400810620c061981cEB8";
			TokenSymbol = "Eth";

		}

		Command _saveCommand;
		public Command SaveCommand
		{
			get
			{
				return _saveCommand ?? (_saveCommand =	new Command(async () => await ExecuteSaveCommand(), ValidateFormDetails));
			}
		}

		async Task ExecuteSaveCommand()
		{
			var newWalletItem = new WalletModel
			{
				TokenName = this.TokenName,
				TokenAddress = this.TokenAddress,
				TokenSymbol = this.TokenSymbol
			};


			//	Here,	we	will	save	the	details	entered	in	a	later	chapter.

			await NavService.PreviousPage();
		}


		bool ValidateFormDetails()
		{
			return !string.IsNullOrWhiteSpace(TokenAddress);
		}
		public override async Task Init()
		{
			await Task.Factory.StartNew(() =>
			{
				TokenName = "New Token";
				TokenAddress = "Contract Address";
				TokenSymbol = "XXX";
			});
		}

	}
}
	
