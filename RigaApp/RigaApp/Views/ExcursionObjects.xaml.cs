using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RigaApp.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExcursionObjects : ContentPage
    {

        public ExcursionObjects()
        {
            InitializeComponent();
            _ = Page_LoadedAsync();
        }

        private async void Open_Maps (object sender, EventArgs e)
        {
            //Page_LoadedAsync();
            await Navigation.PushAsync(new MapPage());
        }

        private async Task Page_LoadedAsync()
        {
            //Debug.WriteLine("Button Pressed");
            //await ((ObjectsViewModel)ObjectList.ItemsSource).Load_Places();

            ObjectsViewModel Places = new ObjectsViewModel();

            //var Places = ObjectsViewModel.Load_Places();
            ObservableCollection<Place> objectList = await Places.Load_Places();
            ObjectList.ItemsSource = objectList;
            //return PlaceList = new ObservableCollection<Place>(Places);
        }

    }
}