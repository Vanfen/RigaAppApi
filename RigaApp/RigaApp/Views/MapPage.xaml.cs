using Plugin.Geolocator;
using RigaApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace RigaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {       

        public MapPage()
        {
            InitializeComponent();
            //MainMap.MapType = MapType.Satellite;
            Position position = new Position(56.948891, 24.105714);
            MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);

            //var map = new Map(mapSpan);

            MainMap.MoveToRegion(mapSpan);
            //customMap.MoveToRegion(mapSpan);

            //map.IsShowingUser = true;

            MainMap.IsShowingUser = true;
            //customMap.IsShowingUser = true;

            /*MainMap.Pins.Add(new Pin
            {
                Position = position,
                Type = PinType.Generic,
                Label = "test",
                Address = "Address",
            });*/

            _ = PopulateByPins(MainMap);
            //_ = PopulateByPins(customMap);

            var expanded = false;
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                ExpandStack(expanded);
                expanded = !expanded;
            };
            InfoBlock.GestureRecognizers.Add(tapGestureRecognizer);
            ObjectInfo.GestureRecognizers.Add(tapGestureRecognizer);

            //_ = PopulateByPins(map);
            //map.Pins.Add(new Pin());

            //Content = map;
        }

        private void ExpandStack(bool expanded)
        {
            if (expanded) {
                InfoBlock.HeightRequest = 130;
                ObjectInfo.MaxLines = 2;
            } else
            {
                InfoBlock.HeightRequest = 1000;
                ObjectInfo.MaxLines = 100;
            }
        }

        private async Task PopulateByPins(Map map)
        {
            var pins = new ObjectsViewModel();
            ObservableCollection<Place> pinsList = await pins.Load_Places();

            ObjectInfo.Text = pinsList.Count().ToString();

            foreach (Place place in pinsList)
            {
                ObjectInfo.Text += "foreach works";
                var pos = new Position(Convert.ToDouble(place.Lat), Convert.ToDouble(place.Lon));
                ObjectInfo.Text += pos;
                var pin = new Pin
                {
                    Position = pos,
                    Type = PinType.Generic,
                    Label = place.Info,
                    Address = place.Address,
                };
                ObjectInfo.Text += pin.Address;
                ObjectInfo.Text += pin.Label;
                ObjectInfo.Text += pin.Position;
                //ObjectInfo.Text += place.Address;
                map.Pins.Add(pin);
                map.Pins.Add(new Pin
                {
                    Position = pos,
                    Type = PinType.Generic,
                    Label = "test",
                    Address = "Address",
                });
                //string ImageName = place.ImageName;
                //pin.MarkerClicked += (s, args) =>
                //{
                //    Fill_Info(pos, ImageName);
                //args.HideInfoWindow = true;
                //    string pinName = ((Pin)s).Label;

                //    ObjectInfo.Text = pinName;
                //await DisplayAlert("Pin Clicked", $"{pinName} was clicked.", "Ok");
                //};//Display_Info(pin.Label);
            }
        }

        private async void Fill_Info(Position pos, string ImageName)
        {
            var geoInfo = await Plugin.Geolocator.CrossGeolocator.Current.GetPositionAsync(new TimeSpan(0, 0, 10), null, true);
            double curLat = 56.948891;
            double curLon = 24.105714;
            if (geoInfo != null)
            {
                // get the lat lng
                curLat = geoInfo.Latitude;
                curLon = geoInfo.Longitude;
                // get the location accuracy
                var locationAccuracy = (int)geoInfo.Accuracy;
            }

            //ObjectInfo.Text += curLat;
            //ObjectInfo.Text += curLon;
            //ObjectInfo.Text += "Image Name - " + ImageName;
            ObjectImage.Source = ImageName;
            

            DirectionsViewModel dir = new DirectionsViewModel();
            Directions dirList = await dir.Find_Directions(Convert.ToString(pos.Latitude), Convert.ToString(pos.Longitude), Convert.ToString(curLat), Convert.ToString(curLon));

            //Directions.Leg points = Directions.Leg dirList.Leg[0];

            
            //var tem = dirList.;
            //ObjectInfo.Text = tem;
            /*foreach (Directions dirData in dirList)
            {
                ObjectInfo.Text = dirData.ToString();
            }*/
        }
    }
}