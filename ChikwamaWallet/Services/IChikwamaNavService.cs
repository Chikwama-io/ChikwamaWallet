using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using ChikwamaWallet.ViewModels;
using Xamarin.Forms;

namespace ChikwamaWallet.Services
{
    public interface IChikwamaNavService
    {
		//Display Alert dialog
		Task<bool> DisplayAlert(string title, string message, string ok, string cancel);

		//	Navigate	back	to	the	Previous	page	in	the NavigationStack
		Task PreviousPage();

		Task PushAsync(Page page);

		//	Navigate	to	the	first	page	within	the NavigationStack
		Task BackToMainPage();


		//	Navigate	to	a	particular	ViewModel	within	our MVVM    Model,	
		//	and	pass	a	parameter	
		Task NavigateToViewModel<ViewModel,TParameter>(TParameter parameter) where ViewModel : ChikwamaBaseViewModel;

		// Clear all previously created views  from the NavigationStack
		void ClearAllViewsFromStack();
	}
}
