using Newtonsoft.Json;
using RigaApp.Models;
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
    class DirectionsViewModel
    {
        private string DirectionsApiServiceBaseAddress = "https://maps.googleapis.com/maps/api/directions/json?"; //origin=lat,lon&destination=lat,lon
        private string DirectionsKey = "&key=AIzaSyCO2Y2wHIVIhzQa7kC166lXJLqeBB6CtVY&mode=walking";

        private readonly HttpClient _httpClient;

        public ObservableCollection<Directions> DirectionList { get; set; }


        public DirectionsViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(DirectionsApiServiceBaseAddress) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
        }

        public async Task<Directions> Find_Directions(string latOrig, string lonOrig, string latDest, string lonDest)
        {
            DirectionsApiServiceBaseAddress += "origin="+latOrig+","+lonOrig; // Append with Origin
            DirectionsApiServiceBaseAddress += "&destination=" + latDest + "," + lonDest; // Append with Destination
            HttpResponseMessage response = await _httpClient.GetAsync(DirectionsApiServiceBaseAddress+DirectionsKey); 
            var TempDirections = await _httpClient.GetStringAsync(DirectionsApiServiceBaseAddress);

            Directions DirectionsList = JsonConvert.DeserializeObject<Directions>(TempDirections);
            //return DirectionList = new ObservableCollection<Directions>(DirectionsList);
            return DirectionsList;

        }
    }
}
