using ShoppingHelperForms.Model;
using ShoppingHelperForms.Services;
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
    public partial class AddNewItemPage : ContentPage
    {
        string _code;
        string _loggedUser;
        ObservableCollection<Item> _itemList;
        IBarcodeApi _barcodeApi;
        IShoppingItemService _shoppingItemService;

        public AddNewItemPage(string code, ObservableCollection<Item> itemList, string loggedUser)
        {
            _code = code;
            _itemList = itemList;
            _loggedUser = loggedUser;

            InitializeComponent();

            codeEntry.Text = _code;

            _barcodeApi = new BarcodeApiService();
            _shoppingItemService = new ShoppingItemApiService();
        }

        private async void addItemBtn_Clicked(object sender, EventArgs e)
        {
            await _barcodeApi.AddItemAsync(nameEntry.Text, _code);

            Item item = new Item()
            {
                IsChecked = false,
                Name = nameEntry.Text,
                Quantity = Convert.ToInt32(quantityEntry.Text),
                Owner = _loggedUser
            };

            _itemList.Add(item);
            await _shoppingItemService.AddItem(item);

            await Navigation.PopAsync();
        }
    }
}