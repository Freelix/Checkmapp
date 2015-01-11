using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.ViewModel;
using CheckMapp.Resources;

namespace CheckMapp.Views
{
    public partial class ArchivesView : UserControl
    {
        public ArchivesView()
        {
            InitializeComponent();
            this.DataContext = new ArchivesViewModel();
        }

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                switch (menuItem.Name)
                {
                    case "Share":
                        break;
                    case "Delete":
                        MessageBox.Show("Are you sure you want to delete this trip?");
                        break;
                    case "Rename":
                        break;
                }
            }
        }


        private void listArchiveTrips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/TripView.xaml", UriKind.Relative));
        }

    }
}
