using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShoppingHelperForms.Entities;
using ShoppingHelperForms.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHelperForms.Services.Concrete
{
    public class UserApiService : IUserService
    {
        private readonly string _apiUrl = "https://shopping-helper-api.herokuapp.com/";
        public async Task<User> AddUserAsync(User user)
        {
            string url = _apiUrl + "users/add";
            return await SendPostRequest(user, url);
        }

        public async Task<User> GetUserAsync(User user)
        {
            string url = _apiUrl + $"users/get?username={user.Username}&password={user.Password}";

            using(HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(url);
                string data = await result.Content.ReadAsStringAsync();

                try
                {
                    JsonSerializerSettings serializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    return JsonConvert.DeserializeObject<List<User>>(data, serializerSettings)[0];
                }
                catch
                {
                    return null;
                }
                
                
            }
        }

        private static async Task<User> SendPostRequest(User user, string url)
        {
            using (HttpClient client = new HttpClient())
            {
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(user, serializerSettings), Encoding.UTF8, "application/json");
                HttpResponseMessage result = await client.PostAsync(url, httpContent);
                string data = await result.Content.ReadAsStringAsync();
                try
                {
                    User response = JsonConvert.DeserializeObject<User>(data);

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
