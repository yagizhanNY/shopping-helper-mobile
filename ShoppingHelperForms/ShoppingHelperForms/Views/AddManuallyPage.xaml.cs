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
    public partial class AddManuallyPage : ContentPage
    {
        ObservableCollection<Item> _itemList;
        string _loggedUser;
        private IShoppingItemService _shoppingItemService;
        public AddManuallyPage(ObservableCollection<Item> itemList, string loggedUser)
        {
            InitializeComponent();
            _itemList = itemList;
            _loggedUser = loggedUser;
            _shoppingItemService = new ShoppingItemApiService();
        }

        private void addItemBtn_Clicked(object sender, EventArgs e)
        {
            Item item = new Item()
            {
                IsChecked= false,
                Name = nameEntry.Text,
                Quantity = Convert.ToInt32(quantityEntry.Text),
                Owner = _loggedUser
            };

            _itemList.Add(item);
            _shoppingItemService.AddItem(item);

            Navigation.RemovePage(this);
        }
    }
}