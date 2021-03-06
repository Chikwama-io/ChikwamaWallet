﻿using ChikwamaWallet.Models;
using ChikwamaWallet.Services;
using ChikwamaWallet.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChikwamaWallet.ViewModels
{
    public class MainViewModel : ChikwamaBaseViewModel
    {

        private IChikwamaNavService navService;
        private NewWalletController controller;

        private ObservableCollection<MenuItems> items;
        public ObservableCollection<MenuItems> Items
        {
            get { return items; }
            set
            {

                items = value;
            }
        }

        private MenuItems _selectedMenuItem { get; set; }
        public MenuItems SelectedMenuItem 
        {
            get { return _selectedMenuItem; }
            set 
            {
                if (_selectedMenuItem != value)
                {
                    _selectedMenuItem = value;

                    HandleSelectedItem();
                }
            }
        }

        private void HandleSelectedItem()
        {
            if (SelectedMenuItem.Title == "Send Money")
            {
                navService.PushAsync(new NavigationPage(new SendMoneyPage(navService, controller)));
            }
            else if (SelectedMenuItem.Title == "Transactions")
            {
                navService.PushAsync(new NavigationPage(new TabbedTransactionHistoryPage(navService, controller)));
            }
            else if (SelectedMenuItem.Title == "Find Cash Point")
            {
                navService.PushAsync(new NavigationPage(new CashPointPage(navService, controller)));
            }
            else if (SelectedMenuItem.Title == "Become Cash Point")
            {
                navService.PushAsync(new NavigationPage(new BecomeCashPointPage(navService, controller)));
            }
        }
        public MainViewModel(IChikwamaNavService navService, NewWalletController controller): base(navService)
        {

            this.controller = controller;
            this.navService = navService;

            Items = new ObservableCollection<MenuItems>() {
                new MenuItems()
                {
                    Title = "Send",
                    ItemDetails = "Transfer Money to another account",
                    Icon = ""
                },
                  new MenuItems()
                {
                     Title = "Get Money",
                    ItemDetails = "Recieve to my account",
                    Icon = ""
                },
                  new MenuItems()
                {
                     Title = "Transactions",
                    ItemDetails = "View statistic info",
                    Icon = ""
                },
                  new MenuItems()
                {
                     Title = "Find Cash Point",
                    ItemDetails = "Find a Cash Point near you",
                    Icon = ""
                },
                  new MenuItems()
                {
                     Title = "Become Cash Point",
                    ItemDetails = "Become a cashpoint",
                    Icon = ""
                },

            };



        }


        
        public override async Task Init()
        {
        }
    }
}
