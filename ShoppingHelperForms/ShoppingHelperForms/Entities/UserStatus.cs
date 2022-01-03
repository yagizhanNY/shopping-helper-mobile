using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingHelperForms.Entities
{
    public class UserStatus
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string Username { get; set; }
        public bool LoggedIn { get; set; }
    }
}
