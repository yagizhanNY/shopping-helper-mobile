using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShoppingHelperForms.Model;
using ShoppingHelperForms.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHelperForms.Services.Concrete
{
    public class ShoppingItemApiService : IShoppingItemService
    {
        private readonly string _apiUrl = "https://shopping-helper-rest-api.herokuapp.com/";

        public async Task<Item> AddItem(Item item)
        {
            string url = _apiUrl + "shopping";
            return await SendPostRequest(item, url);
        }

        private static async Task<Item> SendPostRequest(Item item, string url)
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
                    Item response = JsonConvert.DeserializeObject<Item>(data);

                    return response;
                }
                catch
                {
                    return null;
                }

            }
        }

        public async Task DeleteItemAsync(Item item)
        {
            string url = _apiUrl + $"shopping/{item.Name}/{item.Owner}";
            await SendDeleteRequest(item, url);
        }

        public async Task<ObservableCollection<Item>> GetAllAsync(string loggedUser)
        {
            string url = _apiUrl + $"shopping/{loggedUser}";

            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                string data = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ObservableCollection<Item>>(data);
            }
        }

        private static async Task SendDeleteRequest(Item item, string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = await client.DeleteAsync(url);
                string data = await result.Content.ReadAsStringAsync();
            }
        }
    }
}
