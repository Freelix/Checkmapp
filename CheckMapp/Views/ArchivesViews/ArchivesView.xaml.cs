using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.ViewModels.ArchivesViewModels;
using CheckMapp.Resources;
using CheckMapp.Model.Tables;
using CheckMapp.ViewModel;

namespace CheckMapp.Views.ArchivesViews
{
    public partial class ArchivesView : UserControl
    {
        public ArchivesView()
        {
            InitializeComponent();
            MainViewModel myModel = (((PhoneApplicationFrame)Application.Current.RootVisual).Content as MainPage).DataContext as MainViewModel;
            this.DataContext = myModel.PageViewModels[0];
        }

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                Trip tripSelected = ((sender as MenuItem).DataContext as Trip);
                switch (menuItem.Name)
                {
                    case "Delete":
                        if (MessageBox.Show(AppResources.ConfirmationDeleteTrip, "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            var vm = DataContext as ArchivesViewModel;
                            if (vm != null)
                            {
                                MainViewModel myModel = (((PhoneApplicationFrame)Application.Current.RootVisual).Content as MainPage).DataContext as MainViewModel;
                                vm.DeleteTripCommand.Execute(tripSelected);
                                vm.ArchiveTripList.Remove(tripSelected);
                                listArchiveTrips.ItemsSource = vm.ArchiveTripList;
                                myModel.TripList.Remove(tripSelected);
                            }
                        }
                        break;
                }
            }
        }

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhoneApplicationService.Current.State["Trip"] = ((sender as FrameworkElement).DataContext as Trip).Id;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/TripViews/TripView.xaml", UriKind.Relative));

           
          
        }

    }
}
