using ChikwamaWallet.Services;
using ChikwamaWallet.ViewModels;
using ChikwamaWallet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ChikwamaWallet.Views;
using System.Collections.ObjectModel;

namespace ChikwamaWallet.ViewModels
{
	public class AccountViewModel : ChikwamaBaseViewModel
	{
		IChikwamaLocationService locationService;
		IAccountManager accountManager;
		IWalletManager walletManager;
		public double myLongitude;
		public double myLatitude;

		ObservableCollection<AccountModel> _accounts;

		public ObservableCollection<AccountModel> accounts
		{
			get { return _accounts; }
			set
			{
				_accounts = value;
				OnPropertyChanged();
			}

		}

		public AccountViewModel(IChikwamaNavService navService) : base(navService)
		{
			//	Get	our	Location	Service
			locationService = DependencyService.Get<IChikwamaLocationService>();
			accounts = new ObservableCollection<AccountModel>();
			this.walletManager = walletManager;
			// Check   to ensure  that we  have a   value for	our	object
			if (locationService != null)
			{
				locationService.MyLocation += (object sender, IChikwamaLocationCoords e) =>
				{
					//	Obtain	our	Latitude	and	Longitude	
					//	coordinates
					myLatitude = e.latitude;
					myLongitude = e.longitude;
				};
			}
			//	Call	our	Service	to	get	our	GPS	location
			locationService.GetMyLocation();
		}

		public override async Task Init()
		{
			// Check if we have logged in and that we are running our device on iOS
			
				await LoadAccountDetails();
				NavService.ClearAllViewsFromStack();
			
		}

		public async Task LoadAccountDetails()
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
				accounts = new ObservableCollection<AccountModel>() {
				new AccountModel
				{
					AccountName = "ChikwamaMain",
					DefaultPubKey = accountManager.DefaultAccountAddress,
					isAgent = false,
					isFullAgent = false,
					Longitude = myLatitude,
					Latitude = myLongitude
				}
				};

			});

			// Re-initialise our process busy value back to false
			IsProcessBusy = false;
		}
	}
}
