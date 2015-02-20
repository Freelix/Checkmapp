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
using CheckMapp.ViewModels;
using System.Collections.ObjectModel;
using System.Collections;
using CheckMapp.Model.Tables;

namespace CheckMapp.Pages
{
    public partial class MapView : PhoneApplicationPage
    {
        public MapView()
        {
            InitializeComponent();
            
            this.DataContext = new MapViewModel();
            ObservableCollection<DependencyObject> children = MapExtensions.GetChildren(MyMap);
            var obj = children.FirstOrDefault(x => x.GetType() == typeof(MapItemsControl)) as MapItemsControl;
            if (obj.ItemsSource != null)
            {
                (obj.ItemsSource as IList).Clear();
                obj.ItemsSource = null;
            }
            obj.ItemsSource = (this.DataContext as MapViewModel).TripPoints;
        }

        private void Pushpin_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Trip clickTrip = (sender as Pushpin).DataContext as Trip;
            PhoneApplicationService.Current.State["Trip"] = clickTrip;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/TripViews/TripView.xaml", UriKind.Relative));
        }


    }
}