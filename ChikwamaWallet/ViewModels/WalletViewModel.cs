using ChikwamaWallet.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using ChikwamaWallet.Services;
using Xamarin.Forms;
using ChikwamaWallet.Views;
using Nethereum.HdWallet;

namespace ChikwamaWallet.ViewModels
{
	public class WalletViewModel : ChikwamaBaseViewModel
	{
		decimal _Balance;
		public decimal Balance
		{
			get { return _Balance; }
			set
			{
				_Balance = value;
				OnPropertyChanged();
			}
		}

		decimal _BalanceInETH;
		public decimal BalanceInETH
		{
			get { return _BalanceInETH; }
			set
			{
				_BalanceInETH = value;
				OnPropertyChanged();
			}
		}

		bool _Sendable;
		public bool Sendable
		{
			get { return _Sendable; }
			set
			{
				_Sendable = value;
				OnPropertyChanged();
			}
		}

		public string DefaultAccountAddress => accountManager.DefaultAccountAddress;
		
		IAccountManager accountManager;
		IWalletManager walletManager;
		IChikwamaLocationService locationService;

		ObservableCollection<WalletModel> _tokens;


		public ObservableCollection<WalletModel> tokens
		{
			get { return _tokens; }
			set
			{
				_tokens = value;
				OnPropertyChanged();
			}

		}

		public WalletViewModel(IChikwamaNavService navService) : base(navService)
		{
			//locationService = DependencyService.Get<IChikwamaLocationService>();
			//locationService.GetMyLocation();
			//accountManager = DependencyService.Get<IAccountManager>();
			//walletManager = DependencyService.Get<IWalletManager>();
			tokens = new ObservableCollection<WalletModel>();
		}

		public override async Task Init()
		{
			// Check if we have logged in and that we are running our device on iOS
			
				await LoadWalletDetails();
				NavService.ClearAllViewsFromStack();
			
		}

		public async Task LoadWalletDetails()
		{
			// Check to see if we are already processing our Walk Trail Items
			if (IsProcessBusy)
			{
				return;
			}

			// If we aren't currently processing, we need to initialise our variable to true.
			IsProcessBusy = true;

			// Add a temporary timer, so that we can see our progress indicator working

			await Task.Delay(1000);
			await Task.Factory.StartNew(() =>
			{

				tokens = new ObservableCollection<WalletModel>() {
				new WalletModel
				{
					TokenName = "Ether",
					TokenAddress = DefaultAccountAddress,
					TokenSymbol = "Eth",
					TokenBalance = 10
				},
				new WalletModel
				{
					TokenName   =   "MakerDAI",
					TokenAddress =DefaultAccountAddress,
					TokenSymbol = "DAI",
					TokenBalance =  200
				},
				new WalletModel
				{
					TokenName   =   "ChikwamaToken",
					TokenAddress = DefaultAccountAddress,
					TokenSymbol = "CT",
					TokenBalance =  200
				},
			};
			});

			// Re-initialise our process busy value back to false
			IsProcessBusy = false;
		}

		Command _createNewToken;
		public Command CreateNewToken
		{
			get
			{
				return _createNewToken ?? (_createNewToken = new Command(async () =>
								await NavService.NavigateToViewModel<TokenEntryViewModel, WalletModel>(null)));
			}
		}

		Command<WalletModel> _tokenDetails;
		public Command<WalletModel> ChikwamaTokenDetails
		{
			get
			{
				
				return _tokenDetails ?? (_tokenDetails = new Command<WalletModel>(async(tokenDetails) =>
							await NavService.NavigateToViewModel<TokenDetailsViewModel, WalletModel>(tokenDetails)));

			}
		}

	}
}
