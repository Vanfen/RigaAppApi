using Newtonsoft.Json;
using RigaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RigaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnRegistrationButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
            private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            User user = new User()
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text,
                //Email = emailEntry.Text
            };
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response = await _httpClient.PostAsync("https://rigaappapi20191216120747.azurewebsites.net/api/user/Login", content);

            var result = await response.Content.ReadAsStringAsync();

            if (result.ToString().Contains("successfully!"))
            {
                await DisplayAlert("Authentification status", "Logged in successfully!", "Ok");
                await Navigation.PushAsync(new ExcursionObjects());
            }
            else
            {
                await DisplayAlert("Authentification status", "Log in failed.", "Try Again");
                //ConnectionState.Text = result.ToString();
            }
        }
    }
}