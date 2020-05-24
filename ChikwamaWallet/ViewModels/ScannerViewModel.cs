using Android.App;
using ChikwamaWallet.Models;
using ChikwamaWallet.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChikwamaWallet.ViewModels
{
    public class ScannerViewModel: ChikwamaBaseViewModel
    {
		readonly IChikwamaNavService navigationService;
		NewWalletController controller;
		public ICommand _ScanCommand;

	

		public ScannerViewModel(IChikwamaNavService navService, NewWalletController controller) : base(navService)
		{
			this.navigationService = navService;
			this.controller = controller;
		}

		public ICommand ScanCommand
		{
			get { return (_ScanCommand = _ScanCommand ?? new Command<Result>(ExecuteScanCommand, CanExecuteScanCommand)); }
		}
		bool CanExecuteScanCommand(Result obj) => true;
		async void ExecuteScanCommand(Result obj)
		{
			controller.SetRecipient(obj.ToString());
			await navigationService.PreviousPage();
		
		}

		public override async Task Init()

		{
		

		}

	}
}
