using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.Controls;
using CheckMapp.ViewModel;
using CheckMapp.ViewModels;

namespace CheckMapp.Views
{
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();
            //Si il existe un voyage en cours
            if (PhoneApplicationService.Current.State["Trip"] != null)
            {
                pinButtonAddTrip.IsEnabled = false;
                pinButtonAddTrip.Opacity = 0.4;

                pinButtonCurrentTrip.IsEnabled = true;
                pinButtonCurrentTrip.Opacity = 1.0;
            }
            else
            { 
                pinButtonAddTrip.IsEnabled = true;
                pinButtonAddTrip.Opacity = 1.0;

                pinButtonCurrentTrip.IsEnabled = false;
                pinButtonCurrentTrip.Opacity = 0.4;
            }
            TiltEffect.TiltableItems.Add(typeof(PinButton));
            this.DataContext = new MapViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //connectors.Children.Add(pinButtonMap.LinkTo(pinButtonCurrentTrip));
            //connectors.Children.Add(pinButtonCurrentTrip.LinkTo(pinButtonAddTrip));
            //connectors.Children.Add(pinButtonAddTrip.LinkTo(pinButtonSettings));
        }

        private void pinButtonMap_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Pages/MapPage.xaml", UriKind.Relative));
        }

        private void pinButtonCurrentTrip_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/TripViews/TripView.xaml", UriKind.Relative));
        }

        private void pinButtonAddTrip_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhoneApplicationService.Current.State["Mode"] = Mode.add;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/TripViews/AddEditTripView.xaml", UriKind.Relative));
        }

        private void pinButtonSettings_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/SettingsView.xaml", UriKind.Relative));
        }
    }
}
