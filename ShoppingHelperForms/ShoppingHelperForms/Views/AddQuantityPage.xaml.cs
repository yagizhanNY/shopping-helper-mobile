using ShoppingHelperForms.Entities;
using ShoppingHelperForms.Model;
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

        public AddQuantityPage(BarcodeItem item, ObservableCollection<Item> items)
        {
            _items = items;
            _barcodeItem = item;
            InitializeComponent();
            codeEntry.Text = _barcodeItem.Code;
        }

        private void addItemBtn_Clicked(object sender, EventArgs e)
        {
            Item item = new Item()
            {
                IsChecked = false,
                Name = _barcodeItem.Name,
                Quantity = Convert.ToInt32(quantityEntry.Text)
            };

            _items.Add(item);

            Navigation.PopAsync();
        }
    }
}