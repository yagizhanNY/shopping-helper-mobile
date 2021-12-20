using ShoppingHelperForms.Entities;
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
    public partial class RegisterPage : ContentPage
    {
        string securityCode = "8546Yny8546";
        IUserService _userApiService;
        public RegisterPage()
        {
            InitializeComponent();
            _userApiService = new UserApiService();
        }

        private void registerBtn_Clicked(object sender, EventArgs e)
        {
            if(passwordEntry.Text == confirmPasswordEntry.Text)
            {
                User user = new User()
                {
                    Username = usernameEntry.Text
                };

                byte[] data = Encoding.UTF8.GetBytes(passwordEntry.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(securityCode));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        user.Password = Convert.ToBase64String(results, 0, results.Length);
                    }
                }

                _userApiService.AddUserAsync(user);

                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Passwords doesn't match!", "Your passwords doesn't match. Please check the passwords.", "OK");
            }
        }
    }
}