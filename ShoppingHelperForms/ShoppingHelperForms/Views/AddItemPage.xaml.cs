using ShoppingHelperForms.Entities;
using ShoppingHelperForms.Services;
using ShoppingHelperForms.Services.Concrete;
using System;
using System.Collections.Generic;
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
        public AddItemPage()
        {
            InitializeComponent();
            _barcodeApiService = new BarcodeApiService();
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool status = await _barcodeApiService.AddItemAsync("come from code", result.Text);
                Console.WriteLine(status);

                if (status)
                {
                    BarcodeItem item = await _barcodeApiService.GetItemByCodeAsync(result.Text);
                    barcodeResultLbl.Text = item.Name;
                }

                /*BarcodeItem item = await _barcodeApiService.GetItemByCodeAsync(result.Text);
                barcodeResultLbl.Text = item.Name;*/
            });
        }
    }
}