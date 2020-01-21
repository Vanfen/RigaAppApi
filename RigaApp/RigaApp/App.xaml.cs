using RigaApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RigaApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new LoginPage());
            //MainPage = new NavigationPage(new ExcursionObjects());
            //MainPage = new MapPage();
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
