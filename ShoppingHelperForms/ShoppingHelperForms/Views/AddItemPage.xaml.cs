using ShoppingHelperForms.Entities;
using ShoppingHelperForms.Interfaces;
using ShoppingHelperForms.Model;
using ShoppingHelperForms.Services;
using ShoppingHelperForms.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingHelperForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        IBarcodeApi _barcodeApiService;
        ObservableCollection<Item> _items;
        string _loggedUser;
        private bool _flashlightStatus = false;
        public AddItemPage(ObservableCollection<Item> items, string loggedUser)
        {
            InitializeComponent();
            _barcodeApiService = new BarcodeApiService();
            _items = items;
            _loggedUser = loggedUser;
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                DependencyService.Get<IAudio>().PlaySound("barcode_sound.mp3");
                BarcodeItem item = Task.Run(async () =>
               {
                   return await _barcodeApiService.GetItemByCodeAsync(result.Text);
               }).Result;

                if(item != null)
                {
                    Navigation.PushAsync(new AddQuantityPage(item, _items, _loggedUser));
                    Navigation.RemovePage(this);
                }
                else
                {
                    Navigation.PushAsync(new AddNewItemPage(result.Text, _items, _loggedUser));
                    Navigation.RemovePage(this);
                }
            });
        }

        private async void flashlightBtn_Clicked(object sender, EventArgs e)
        {
            if (!_flashlightStatus)
            {
                cameraScreen.IsTorchOn = true;
                _flashlightStatus = true;
            }
            else
            {
                cameraScreen.IsTorchOn = false;
                _flashlightStatus = false;
            }
        }
    }
}