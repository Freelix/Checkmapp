﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.ViewModels.POIViewModels;
using CheckMapp.Resources;
using CheckMapp.Model.Tables;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using CheckMapp.Utils;

namespace CheckMapp.Views.POIViews
{
    public partial class AddPOIView : PhoneApplicationPage
    {
        public AddPOIView()
        {
            InitializeComponent();
            Trip trip = (Trip)PhoneApplicationService.Current.State["Trip"];
            this.DataContext = new AddPOIViewModel(trip);
        }

        /// <summary>
        /// On assigne les titres des boutons au démarrage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Save;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Cancel;
            }
        }

        /// <summary>
        /// Sauvegarde du POI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconSave_Click(object sender, EventArgs e)
        {
            this.Focus();
            // wait till the next UI thread tick so that the binding gets updated
            Dispatcher.BeginInvoke(() =>
            {
                var vm = DataContext as AddPOIViewModel;
                if (vm != null)
                {
                    vm.AddPOICommand.Execute(null);
                }

                if (vm.IsFormValid)
                    (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
            });
            
        }

        /// <summary>
        /// Annuler l'ajout du POI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconCancel_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
        }

        private async void Map_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
           await Utils.Utility.AddLocation(this.myMap, this.PoiTextBox, e, 0.0, 0.0);
           MapLayer layer = this.myMap.Layers.FirstOrDefault();
           (this.DataContext as AddPOIViewModel).Latitude = layer[0].GeoCoordinate.Latitude;
           (this.DataContext as AddPOIViewModel).Longitude = layer[0].GeoCoordinate.Longitude;
        }

    }
}