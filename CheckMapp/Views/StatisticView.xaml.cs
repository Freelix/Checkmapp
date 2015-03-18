using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.ViewModels;
using CheckMapp.Model.Tables;
using Windows.Devices.Geolocation;
using CheckMapp.Resources;
using Microsoft.Phone.Maps.Controls;

namespace CheckMapp.Views
{
    public partial class StatisticView : PhoneApplicationPage
    {
        Trip currentTrip;
        public StatisticView()
        {
            
            InitializeComponent();
            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            currentTrip = (Trip)PhoneApplicationService.Current.State["Trip"];
            if (currentTrip.IsActif)
                EndStack.Visibility = Visibility.Collapsed;
            else
                EndStack.Visibility = Visibility.Visible;    
            this.DataContext = new StatisticViewModel(currentTrip);
            loadLocation();

           
        }

        private async void loadLocation()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50; // maybe 500 if error in this part...
            geolocator.MovementThreshold = 5;
            geolocator.ReportInterval = 500;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10));

                await Utils.Utility.AddLocation(statsMap, null, null, geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude, false, AppResources.YourPosition);
                var bounds = new LocationRectangle(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude, geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);
                statsMap.SetView(bounds);
            }
            catch (Exception)
            {
                // the app does not have the right capability or the location master switch is off 
                MessageBox.Show(AppResources.LocationError);
            }
        }
    }
}