using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.Utils;
using Microsoft.Phone.Maps.Controls;
using CheckMapp.Resources;
using System.Threading;

namespace CheckMapp.Controls
{
    public partial class MapSelectControl : UserControl
    {
        public MapSelectControl()
        {
            InitializeComponent();

            if (!Utility.checkNetworkConnection())
            {
                //MessageBox.Show(AppResources.InternetConnection, AppResources.NotConnected, MessageBoxButton.OK);
                this.PoiTextBox.IsEnabled = false;
                this.btn_place.Visibility = Visibility.Collapsed;
                this.BorderInternet.Visibility = Visibility.Visible;
            }
            else
            {
                this.PoiTextBox.IsEnabled = true;
                this.btn_place.Visibility = Visibility.Visible;
                this.BorderInternet.Visibility = Visibility.Collapsed;
            }

            btn_place.IsEnabled = !String.IsNullOrEmpty(PoiTextBox.Text);
        }

        public static readonly DependencyProperty LongitudeProperty =
     DependencyProperty.Register("Longitude", typeof(double), typeof(MapSelectControl), null);

        public double Longitude
        {
            get { return (double)GetValue(LongitudeProperty); }
            set
            {
                SetValue(LongitudeProperty, value);
            }
        }


        public static readonly DependencyProperty LatitudeProperty =
     DependencyProperty.Register("Latitude", typeof(double), typeof(MapSelectControl), null);

        public double Latitude
        {
            get { return (double)GetValue(LatitudeProperty); }
            set
            {
                SetValue(LatitudeProperty, value);
            }
        }

        public static readonly DependencyProperty LocationProperty =
DependencyProperty.Register("PoiLocation", typeof(string), typeof(MapSelectControl), null);

        public string PoiLocation
        {
            get { return (string)GetValue(LocationProperty); }
            set
            {
                SetValue(LocationProperty, value);
            }
        }


        private void PoiTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn_place.IsEnabled = !String.IsNullOrEmpty(PoiTextBox.Text);
        }

        private void btn_place_Click(object sender, RoutedEventArgs e)
        {
            AfficherCarte(this.PoiTextBox, this.myMap);
        }

        private async void Map_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await Utility.AddLocation(this.myMap, this.PoiTextBox, e, 0.0, 0.0, true);
            if (this.myMap.Layers != null && this.myMap.Layers.Count > 0)
            {
                MapLayer layer = this.myMap.Layers.FirstOrDefault();
                Latitude = layer[0].GeoCoordinate.Latitude;
                Longitude = layer[0].GeoCoordinate.Longitude;
            }
        }

        private async void AfficherCarte(PhoneTextBox myTextBox, Microsoft.Phone.Maps.Controls.Map myMap)
        {
            try
            {
                var CoordinateList = await MapHelper.getCoordinateAsync(myTextBox.Text);

                // CoordinateList[0] = latitude, CoordinateList[1] = longitude
                await Utility.AddLocation(myMap, myTextBox, null, CoordinateList[0], CoordinateList[1], true);

                Latitude = CoordinateList[0];
                Longitude = CoordinateList[1];
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(AppResources.InvalideSearch, myTextBox.Text), AppResources.Warning, MessageBoxButton.OK);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Utility.AddLocation(this.myMap, this.PoiTextBox, null, Latitude, Longitude);
            Thread.Sleep(500);
        }
    }
}
