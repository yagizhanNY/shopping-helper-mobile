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
        Task<ObservableCollection<Item>> GetAllAsync(string loggedUser);
        Task<Item> AddItem(Item item);
        Task DeleteItemAsync(Item item);
    }
}
