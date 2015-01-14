using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.ViewModels.TripViewModels;

namespace CheckMapp.Views.TripViews
{
    public partial class TripView : PhoneApplicationPage
    {
        public TripView()
        {
            InitializeComponent();
            this.DataContext = new TripViewModel();
        }

        private void RoundButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/POIViews/POIView.xaml", UriKind.Relative));
        }
    }
}