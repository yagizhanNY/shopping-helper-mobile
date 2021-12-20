using ShoppingHelperForms.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHelperForms.Services.Abstract
{
    public interface IUserService
    {
        Task<User> GetUserAsync(User user);
        Task<User> AddUserAsync(User user);
    }
}
