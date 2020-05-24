using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChikwamaWallet.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;

namespace ChikwamaWallet.ViewModels
{
	public abstract class ChikwamaBaseViewModel : INotifyPropertyChanged
	{
		protected IChikwamaNavService NavService { get; private set; }
		bool _isProcessBusy;
		public bool IsProcessBusy
		{
			get { return _isProcessBusy; }
			set
			{
				_isProcessBusy = value;
				OnPropertyChanged();
				OnIsBusyChanged();
			}
		}

	

	
		protected ChikwamaBaseViewModel(IChikwamaNavService navService)
		{
			NavService = navService;
	
		}

		

		public abstract Task Init();

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		protected virtual void OnIsBusyChanged()
		{
			// We are processing our Token Details Information
		}
	}

	public abstract class ChikwamaBaseViewModel<ChikwamaParam> : ChikwamaBaseViewModel
	{
		protected ChikwamaBaseViewModel(IChikwamaNavService navService) : base(navService)
		{
		}

		public override async Task Init()
		{
			await Init(default(ChikwamaParam));
		}
		public abstract Task Init(ChikwamaParam chikwamaDetails);


	}
}

