using ShoppingHelperForms.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHelperForms.Services.Concrete
{
    public class LocalDatabaseService
    {
        private readonly SQLiteAsyncConnection _connection;

        public LocalDatabaseService(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<UserStatus>();
        }

        public async Task<UserStatus> GetLoggedUser()
        {
            return await _connection.Table<UserStatus>().FirstOrDefaultAsync(u => u.LoggedIn == true);
        }

        public async Task ChangeUserStatus(UserStatus userStatus)
        {
            UserStatus user = await _connection.Table<UserStatus>().FirstOrDefaultAsync(u => u.Username == userStatus.Username);

            if (user == null)
            {
                await _connection.InsertAsync(userStatus);
            }
            else
            {
                userStatus.Id = user.Id;
                await _connection.UpdateAsync(userStatus);
            }
        }
    }
}
