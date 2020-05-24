using ChikwamaWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ChikwamaWallet.Views
{
    public class AccountPage : ContentPage
    {
		AccountViewModel _viewModel
		{

			get
			{
				return BindingContext as AccountViewModel;
			}

		}

		public AccountPage()
		{ 

		}
	}
}