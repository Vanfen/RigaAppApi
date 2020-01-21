using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RigaApp
{
    class ObjectsViewModel
    {
        private const string WebApiServiceBaseAddress = "https://rigaappapi20191216120747.azurewebsites.net/";
        private const string WebApiServiceObjectAddress = "api/objects";

        private readonly HttpClient _httpClient;

        private List<Place> TempPlaces { get; set; }

        public ObservableCollection<Place> PlaceList { get; set; }


        public ObjectsViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(WebApiServiceBaseAddress) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
             
        }

        public async Task<ObservableCollection<Place>> Load_Places()
        {
            //HttpResponseMessage response = await _httpClient.GetAsync(WebApiServiceObjectAddress); 
            var TempPlaces = await _httpClient.GetStringAsync(WebApiServiceObjectAddress);
            var Places = JsonConvert.DeserializeObject<List<Place>>(TempPlaces);
            return PlaceList = new ObservableCollection<Place>(Places);

        }
    }
}
