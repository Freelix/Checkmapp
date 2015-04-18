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
using CheckMapp.Model.Tables;
using CheckMapp.Resources;

namespace CheckMapp.Views.TripViews
{
    public partial class SelectEndDateView : PhoneApplicationPage
    {
        public SelectEndDateView()
        {
            InitializeComponent();
            this.DataContext = new SelectEndDateViewModel((int)PhoneApplicationService.Current.State["Trip"]);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var vm = DataContext as SelectEndDateViewModel;
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Back)
            {
                // wait till the next UI thread tick so that the binding gets updated
                Dispatcher.BeginInvoke(() =>
                {
                    if (vm != null)
                    {
                        vm.FinishTripCommand.Execute(null);
                        PhoneApplicationService.Current.State["Trip"] = 0;
                    }
                    if (vm.IsFormValid)
                    {
                        (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                    }
                });

                
            }
            else if(e.NavigationMode == NavigationMode.Back && !vm.IsFormValid)
            {
                vm.CancelSelectDateCommand.Execute(null);
            }

            PhoneApplicationService.Current.State["Trip"] = vm.Trip.Id;
            base.OnNavigatedFrom(e);
        }

        private void IconSave_Click(object sender, EventArgs e)
        {
            this.Focus();

            // wait till the next UI thread tick so that the binding gets updated
            Dispatcher.BeginInvoke(() =>
            {
                var vm = DataContext as SelectEndDateViewModel;
                if (vm != null)
                {
                    vm.FinishTripCommand.Execute(null);
                    PhoneApplicationService.Current.State["Trip"] = 0;
                }
                if (vm.IsFormValid)
                {
                    // En appelant directement la page principale on rafraichit celle-ci pour mettre a jour le panorama
                    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
            });

           
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Save;
            }
        }
    }
}