using ShoppingHelperForms.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHelperForms.Services
{
    public interface IBarcodeApi
    {
        Task<List<BarcodeItem>> GetAllAsync();
        Task<BarcodeItem> GetItemByCodeAsync(string code);
        Task AddItemAsync(string name, string code);
    }
}
