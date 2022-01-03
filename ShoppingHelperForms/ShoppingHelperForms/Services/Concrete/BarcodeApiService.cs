using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        private readonly string _apiUrl = "https://shopping-helper-rest-api.herokuapp.com/";

        public async Task AddItemAsync(string name, string code)
        {
            string url = _apiUrl + $"barcode/add";

            BarcodeItem item = new BarcodeItem()
            {
                Code = code,
                Name = name
            };

            await SendPostRequest(item, url);
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

        private static async Task<BarcodeItem> SendPostRequest(BarcodeItem item, string url)
        {
            using (HttpClient client = new HttpClient())
            {
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(item, serializerSettings), Encoding.UTF8, "application/json");
                HttpResponseMessage result = await client.PostAsync(url, httpContent);
                string data = await result.Content.ReadAsStringAsync();
                try
                {
                    BarcodeItem response = JsonConvert.DeserializeObject<BarcodeItem>(data);

                    return response;
                }
                catch
                {
                    return null;
                }

            }
        }


    }
}
