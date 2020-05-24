using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChikwamaWallet.Services;
using ChikwamaWallet.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(ChikwamaNavService))]
namespace ChikwamaWallet.Services
{    
    public class ChikwamaNavService : IChikwamaNavService
    {
        public INavigation navigation { get; set; }

        readonly IDictionary<Type, Type> _viewMapping = new Dictionary<Type, Type>();

        //	Register	our	ViewModel	and	View	within	our	Dictionary	
        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _viewMapping.Add(viewModel, view);
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await MainPage.DisplayAlert(title, message, ok, cancel);
        }

        //	Instance	method	that	allows	us	to	move	back	to	the	previous page.	
        public async Task PreviousPage()
        {
            //	Check	to	see	if	we	can	move	back	to	the	previous	page	
            if (navigation.NavigationStack != null && navigation.NavigationStack.Count > 0)
            {
                await navigation.PopAsync(true);
            }
        }

        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushModalAsync(page);
        }

     
        //	Instance	method	that	takes	us	back	to	the	main root	WalksPage	
        public async Task BackToMainPage()
        {
            await MainPage.Navigation.PopToRootAsync(true);
        }

		//	Instance	method	that	navigates	to	a	specific	ViewModel		
		//	within	our	dictionary	viewMapping	
		public async Task NavigateToViewModel<ViewModel, ChikwamaParam>(ChikwamaParam parameter) where ViewModel : ChikwamaBaseViewModel
		{
			Type viewType;

			if (_viewMapping.TryGetValue(typeof(ViewModel), out	viewType))
			{
				var constructor = viewType.GetTypeInfo()
				.DeclaredConstructors
				.FirstOrDefault(dc => dc.GetParameters()
				.Count() <= 0);

				var view = constructor.Invoke(null) as Page;
				await navigation.PushAsync(view, true);
			}

			if (navigation.NavigationStack.Last().BindingContext is	ChikwamaBaseViewModel<ChikwamaParam>)
				await ((ChikwamaBaseViewModel<ChikwamaParam>)(navigation.NavigationStack.Last().BindingContext)).Init(parameter);
		}

        // Instance method to remove all previously created views from
        // the Navigation Stack.
        public void ClearAllViewsFromStack()
        {
            // Check to see if any items have already been pushed
            // onto the NavigationStack.
            if (MainPage.Navigation.NavigationStack.Count <= 1)
                return;
            for (var i = 0; i < MainPage.Navigation.NavigationStack.Count - 1; i++)
                MainPage.Navigation.RemovePage(navigation.NavigationStack[i]);
        }

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }
    }
}
   
