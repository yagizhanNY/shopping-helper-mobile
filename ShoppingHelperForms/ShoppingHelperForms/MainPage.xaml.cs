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
using Xamarin.Forms;

namespace ShoppingHelperForms
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Item> _itemList = new ObservableCollection<Item>();
        IShoppingItemService _shoppingItemService;

        public MainPage()
        {
            InitializeComponent();
            _shoppingItemService = new ShoppingItemApiService();

            _itemList = Task.Run(async () =>
            {
                return await _shoppingItemService.GetAllAsync();
            }).Result; 

            itemListView.ItemsSource = _itemList;
        }

        private void itemListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Item selectedItem = e.Item as Item;
            selectedItem.IsChecked = !selectedItem.IsChecked;
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
                await Navigation.PushAsync(new AddItemPage(_itemList));
            }
            else if(action == "Add Manually")
            {
                await Navigation.PushAsync(new AddManuallyPage(_itemList));
            }
            
        }
    }
}
