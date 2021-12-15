using ShoppingHelperForms.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHelperForms.Services.Abstract
{
    public interface IShoppingItemService
    {
        Task<ObservableCollection<Item>> GetAllAsync();
        Task<Item> AddItem(Item item);
        Task<Item> DeleteItemAsync(Item item);
    }
}
