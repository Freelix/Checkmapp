using System;
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

namespace CheckMapp.Views.POIViews
{
    public partial class AddPOIView : PhoneApplicationPage
    {
        TextBox PoiNameTextBox;
        Microsoft.Phone.Maps.Controls.Map poiMap;
        StackPanel myStack;
        public AddPOIView()
        {
            InitializeComponent();
            Trip trip = (Trip)PhoneApplicationService.Current.State["Trip"];
            this.DataContext = new AddPOIViewModel(trip);

            foreach (var element in this.ContentPanel.Children)
            {
                if (element is StackPanel) {
                    myStack = (StackPanel)element;
                    foreach (var element2 in myStack.Children)
                    {
                        if (element2 is TextBox)
                            PoiNameTextBox = (TextBox)element2;
                        if (element2 is Microsoft.Phone.Maps.Controls.Map)
                            poiMap = (Microsoft.Phone.Maps.Controls.Map)element2;
                    }
                }
            }
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
            ReverseGeocodeQuery query;
            List<MapLocation> mapLocations;
            string pushpinContent;
            MapLocation mapLocation;

            query = new ReverseGeocodeQuery();
            query.GeoCoordinate = poiMap.ConvertViewportPointToGeoCoordinate(e.GetPosition(poiMap));

            mapLocations = (List<MapLocation>)await query.GetMapLocationsAsync();
            mapLocation = mapLocations.FirstOrDefault();

            if (mapLocation != null)
            {
                MapLayer pinLayout = new MapLayer();
                Pushpin MyPushpin = new Pushpin();
                MapOverlay pinOverlay = new MapOverlay();
                if (poiMap.Layers.Count > 0)
                {
                    poiMap.Layers.RemoveAt(poiMap.Layers.Count - 1);
                }

                poiMap.Layers.Add(pinLayout);

                MyPushpin.GeoCoordinate = mapLocation.GeoCoordinate;

                pinOverlay.Content = MyPushpin;
                pinOverlay.GeoCoordinate = mapLocation.GeoCoordinate;
                pinLayout.Add(pinOverlay);

                pushpinContent = mapLocation.Information.Name;
                pushpinContent = string.IsNullOrEmpty(pushpinContent) ? mapLocation.Information.Description : null;
                pushpinContent = string.IsNullOrEmpty(pushpinContent) ? string.Format("{0} {1} {2} ", mapLocation.Information.Address.Street, mapLocation.Information.Address.City, mapLocation.Information.Address.Country) : null;

                MyPushpin.Content = pushpinContent.Trim();
                MyPushpin.Visibility = Visibility.Visible;

                this.PoiNameTextBox.Text = MyPushpin.Content.ToString();

            }
        }
    }
}