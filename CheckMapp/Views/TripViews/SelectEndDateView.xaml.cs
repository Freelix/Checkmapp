﻿using System;
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

namespace CheckMapp.Views.TripViews
{
    public partial class SelectEndDateView : PhoneApplicationPage
    {
        public SelectEndDateView()
        {
            InitializeComponent();
            this.DataContext = new SelectEndDateViewModel((Trip)PhoneApplicationService.Current.State["Trip"]);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode != NavigationMode.New)
            {
                // wait till the next UI thread tick so that the binding gets updated
                Dispatcher.BeginInvoke(() =>
                {
                    var vm = DataContext as SelectEndDateViewModel;
                    if (vm != null)
                    {
                        vm.FinishTripCommand.Execute(null);
                        PhoneApplicationService.Current.State["Trip"] = null;
                    }
                    if (vm.IsFormValid)
                    {
                        (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                    }
                });

                
            }
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
                    PhoneApplicationService.Current.State["Trip"] = null;
                }
                if (vm.IsFormValid)
                {
                    // En appelant directement la page principale on rafraichit celle-ci pour mettre a jour le panorama
                    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
            });

           
        }
    }
}