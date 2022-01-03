using ShoppingHelperForms.Services.Concrete;
using ShoppingHelperForms.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingHelperForms
{
    public partial class App : Application
    {
        private static LocalDatabaseService database;

        public static LocalDatabaseService Database
        {
            get 
            { 
                if(database == null)
                {
                    database = new LocalDatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.db"));
                }
                return database; 
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
