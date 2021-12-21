using ShoppingHelperForms.Entities;
using ShoppingHelperForms.Model;
using ShoppingHelperForms.Services.Abstract;
using ShoppingHelperForms.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingHelperForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly string securityCode = "8546Yny8546";
        private readonly IUserService _userApiService;
        public LoginPage()
        {
            InitializeComponent();
            _userApiService = new UserApiService();
        }

        private async void loginBtn_Clicked(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
            activityIndicator.IsVisible = true;

            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            User user = new User()
            {
                Username = username,
                Password = await Task.Run(() => EncryptePassword(password))
            };

            User userFromApi = await Task.Run(async () =>
            {
                return await _userApiService.GetUserAsync(user);
            });

            if (userFromApi != null)
            {
                string passwordFromApi = await Task.Run(() => DeEncryptePassword(userFromApi.Password));

                if(passwordFromApi == password)
                {
                    await Navigation.PushAsync(new MainPage(username));
                    Navigation.RemovePage(this);
                    activityIndicator.IsRunning = false;
                    activityIndicator.IsVisible = false;
                }
                else
                {
                    await DisplayAlert("Authentication Error.", "Username or password is incorrect, please try again.", "OK");
                    activityIndicator.IsRunning = false;
                    activityIndicator.IsVisible = false;
                }
            }
            else
            {
                await DisplayAlert("Authentication Error.", "User not registered, please register.", "OK");
                activityIndicator.IsRunning = false;
                activityIndicator.IsVisible = false;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private string EncryptePassword(string password)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(securityCode));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key= keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        private string DeEncryptePassword(string password)
        {
            byte[] data = Convert.FromBase64String(password);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(securityCode));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Encoding.UTF8.GetString(results);
                }
            }
        }
    }
}