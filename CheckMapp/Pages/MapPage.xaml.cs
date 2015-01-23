using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Toolkit;

namespace CheckMapp.Pages
{
    public partial class MapPage : PhoneApplicationPage
    {
        public MapPage()
        {
            InitializeComponent();

            MapLayer layer1 = new MapLayer();
            Pushpin pushpin1 = new Pushpin();
            pushpin1.GeoCoordinate = new System.Device.Location.GeoCoordinate(22.34, 88.30); ;
            pushpin1.Content = "Content";
            MapOverlay overlay1 = new MapOverlay();
            overlay1.Content = pushpin1;
            overlay1.GeoCoordinate = new System.Device.Location.GeoCoordinate(22.34, 88.30); ;
            layer1.Add(overlay1);
            MyMap.Layers.Add(layer1);
        }

        private void Map_ZoomLevelChanged(object sender, Microsoft.Phone.Maps.Controls.MapZoomLevelChangedEventArgs e)
        {
            
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}