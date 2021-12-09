using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ShoppingHelperForms.Entities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShoppingHelperTest
{
    [TestClass]
    public class ApiTest
    {
        [TestMethod]
        public async Task GetBarcodeFromCodeAsync()
        {
            string url = "https://shopping-helper-api.herokuapp.com/get/8690624303582";

            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                string data = await result.Content.ReadAsStringAsync();
                Assert.AreEqual("Cips", JsonConvert.DeserializeObject<BarcodeItem>(data).Name);
            }
        }
    }
}
