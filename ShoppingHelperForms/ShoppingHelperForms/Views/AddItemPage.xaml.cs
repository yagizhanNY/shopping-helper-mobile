using ShoppingHelperForms.Entities;
using ShoppingHelperForms.Model;
using ShoppingHelperForms.Services;
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
    public partial class AddItemPage : ContentPage
    {
        IBarcodeApi _barcodeApiService;
        ObservableCollection<Item> _items;
        public AddItemPage(ObservableCollection<Item> items)
        {
            InitializeComponent();
            _barcodeApiService = new BarcodeApiService();
            _items = items;
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                BarcodeItem item = Task.Run(async () =>
               {
                   return await _barcodeApiService.GetItemByCodeAsync(result.Text);
               }).Result;

                if(item != null)
                {
                    Navigation.PushAsync(new AddQuantityPage(item, _items));
                    Navigation.RemovePage(this);
                }
                else
                {
                    Navigation.PushAsync(new AddNewItemPage(result.Text, _items));
                    Navigation.RemovePage(this);
                }
            });
        }
    }
}