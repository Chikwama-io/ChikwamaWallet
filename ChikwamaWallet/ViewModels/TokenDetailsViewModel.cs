using ChikwamaWallet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ChikwamaWallet.Services;
using System.Threading.Tasks;

namespace ChikwamaWallet.ViewModels
{
    public class TokenDetailsViewModel : ChikwamaBaseViewModel<WalletModel>
    {
		WalletModel _token;

		public WalletModel Token
		{
			get { return _token; }
			set
			{
				_token = value;
				OnPropertyChanged();
			}
		}

		public TokenDetailsViewModel(IChikwamaNavService navService) :	base(navService)
		{
		}

		public override async Task Init(WalletModel chikwamaDetails)
		{
			await Task.Factory.StartNew(() =>
			{

				Token = chikwamaDetails;

			});

		}

		

	}
}
