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
        ObservableCollection<Item> _itemList;
        IBarcodeApi _barcodeApi;
        IShoppingItemService _shoppingItemService;

        public AddNewItemPage(string code, ObservableCollection<Item> itemList)
        {
            _code = code;
            _itemList = itemList;

            InitializeComponent();

            codeEntry.Text = _code;

            _barcodeApi = new BarcodeApiService();
            _shoppingItemService = new ShoppingItemApiService();
        }

        private async void addItemBtn_Clicked(object sender, EventArgs e)
        {
            _ = await _barcodeApi.AddItemAsync(nameEntry.Text, _code);

            Item item = new Item()
            {
                IsChecked = false,
                Name = nameEntry.Text,
                Quantity = Convert.ToInt32(quantityEntry.Text)
            };

            _itemList.Add(item);
            _ = await _shoppingItemService.AddItem(item);

            _ = await Navigation.PopAsync();
        }
    }
}