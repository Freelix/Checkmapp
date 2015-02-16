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
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();

            TiltEffect.TiltableItems.Add(typeof(PinButton));
            this.DataContext = new DashboardViewModel();
        }

        public void LoadComponents(bool hasCurrentTrip)
        {
            //Si il existe un voyage en cours
            if (hasCurrentTrip)
            {
                pinButtonAddTrip.IsEnabled = false;
                pinButtonAddTrip.Opacity = 0.4;
                textAddTrip.Opacity = 0.4;

                pinButtonCurrentTrip.IsEnabled = true;
                pinButtonCurrentTrip.Opacity = 1.0;
                textCurrentTrip.Opacity = 1.0;
            }
            else
            {
                pinButtonAddTrip.IsEnabled = true;
                pinButtonAddTrip.Opacity = 1.0;
                textAddTrip.Opacity = 1.0;

                pinButtonCurrentTrip.IsEnabled = false;
                pinButtonCurrentTrip.Opacity = 0.4;
                textCurrentTrip.Opacity = 0.4;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void pinButtonMap_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/MapView.xaml", UriKind.Relative));
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
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/SettingsViews/SettingsView.xaml", UriKind.Relative));
        }
    }
}
