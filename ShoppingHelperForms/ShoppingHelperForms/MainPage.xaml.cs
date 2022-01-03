using ShoppingHelperForms.Entities;
using ShoppingHelperForms.Model;
using ShoppingHelperForms.Services.Abstract;
using ShoppingHelperForms.Services.Concrete;
using ShoppingHelperForms.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShoppingHelperForms
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Item> _itemList = new ObservableCollection<Item>();
        IShoppingItemService _shoppingItemService;
        string _loggedUser;

        public MainPage(string loggedUser)
        {
            _loggedUser = loggedUser;
            InitializeComponent();
            LoggedUserInfoLbl.Text = _loggedUser;
            _shoppingItemService = new ShoppingItemApiService();

            _itemList = Task.Run(async () =>
            {
                return await _shoppingItemService.GetAllAsync(_loggedUser);
            }).Result; 

            itemListView.ItemsSource = _itemList;
        }

        private void itemListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Item selectedItem = e.Item as Item;
            selectedItem.IsChecked = !selectedItem.IsChecked;

            if (selectedItem.IsChecked)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(500);
                    _itemList.Remove(selectedItem);
                    await _shoppingItemService.DeleteItemAsync(selectedItem);
                });
                
            }
        }

        private void deleteItemBtn_Clicked(object sender, EventArgs e)
        {
            List<Item> selectedItemList = _itemList.Where(i => i.IsChecked).ToList();

            foreach (Item selectedItem in selectedItemList)
            {
                _itemList.Remove(selectedItem);
                _shoppingItemService.DeleteItemAsync(selectedItem);
            }
        }

        private async void itemAddBtn_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Select the add method", "Cancel", null, "Add via Barcode", "Add Manually");


            if(action == "Add via Barcode")
            {
                await Navigation.PushAsync(new AddItemPage(_itemList, _loggedUser));
            }
            else if(action == "Add Manually")
            {
                await Navigation.PushAsync(new AddManuallyPage(_itemList, _loggedUser));
            }
            
        }

        private void LogoutBtn_Clicked(object sender, EventArgs e)
        {
            UserStatus user = new UserStatus()
            {
                Username = _loggedUser,
                LoggedIn = false
            };

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await App.Database.ChangeUserStatus(user);
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            });
        }
    }
}
