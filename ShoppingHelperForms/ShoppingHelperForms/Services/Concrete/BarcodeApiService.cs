using Newtonsoft.Json;
using ShoppingHelperForms.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHelperForms.Services.Concrete
{
    public class BarcodeApiService : IBarcodeApi
    {
        private readonly string _apiUrl = "https://shopping-helper-api.herokuapp.com/";

        public async Task<bool> AddItemAsync(string name, string code)
        {
            string url = _apiUrl + $"barcode/add?name={name}&code={code}";
            
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(url);
                string data = await result.Content.ReadAsStringAsync();
                try
                {
                    List<BarcodeItem> response = JsonConvert.DeserializeObject<List<BarcodeItem>>(data);

                    return response != null;
                }
                catch
                {
                    return false;
                }
                
            }
        }

        public async Task<List<BarcodeItem>> GetAllAsync()
        {
            string url = _apiUrl + "barcode/getall";

            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                string data = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<BarcodeItem>>(data);
            }
        }

        public async Task<BarcodeItem> GetItemByCodeAsync(string code)
        {
            string url = _apiUrl + "barcode/get/" + code;

            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                string data = await result.Content.ReadAsStringAsync();

                try
                {
                    BarcodeItem item = JsonConvert.DeserializeObject<List<BarcodeItem>>(data)[0];
                    return item;
                }
                catch
                {
                    return null;
                }
                
            }
        }

        
    }
}
