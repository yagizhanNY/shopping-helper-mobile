using ShoppingHelperForms.Entities;
using ShoppingHelperForms.Model;
using ShoppingHelperForms.Services.Abstract;
using ShoppingHelperForms.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingHelperForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddQuantityPage : ContentPage
    {
        BarcodeItem _barcodeItem;
        ObservableCollection<Item> _items;
        IShoppingItemService _shoppingItemService;
        string _loggedUser;

        public AddQuantityPage(BarcodeItem item, ObservableCollection<Item> items, string loggedUser)
        {
            _items = items;
            _loggedUser = loggedUser;
            _barcodeItem = item;
            InitializeComponent();
            codeEntry.Text = _barcodeItem.Code;
            _shoppingItemService = new ShoppingItemApiService();
        }

        private async void addItemBtn_Clicked(object sender, EventArgs e)
        {
            Item item = new Item()
            {
                IsChecked = false,
                Name = _barcodeItem.Name,
                Quantity = Convert.ToInt32(quantityEntry.Text),
                Owner = _loggedUser
            };

            _items.Add(item);

            _ = await _shoppingItemService.AddItem(item);

            await Navigation.PopAsync();
        }
    }
}