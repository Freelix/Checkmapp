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
using CheckMapp.Resources;
using CheckMapp.ViewModels;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using CheckMapp.Utils;
using CheckMapp.Model.Tables;
using System.Device.Location;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Maps.Controls;

namespace CheckMapp.Views.TripViews
{
    public partial class AddEditTripView : PhoneApplicationPage
    {
        public AddEditTripView()
        {
            InitializeComponent();

            if (!Utils.Utility.checkNetworkConnection())
            {
                MessageBoxResult mbr = MessageBox.Show("Connect your phone to a wifi connection or your mobile connection to create a new trip", "Warning", MessageBoxButton.OK);
            }

            Mode mode = (Mode)PhoneApplicationService.Current.State["Mode"];
            Trip currentTrip = (Trip)PhoneApplicationService.Current.State["Trip"];
            this.DataContext = new AddEditTripViewModel(currentTrip, mode);

            //Assigne le titre de la page
            var vm = this.DataContext as AddEditTripViewModel;
            if (vm.Mode == Mode.add)
                TitleTextblock.Text = AppResources.AddTrip.ToLower();
            else
                TitleTextblock.Text = AppResources.EditTrip.ToLower();

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
           

            base.OnNavigatedTo(e);
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
        /// Sauvegarder le voyage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconSave_Click(object sender, EventArgs e)
        {
            this.Focus();
            Dispatcher.BeginInvoke(() =>
            {
                var vm = DataContext as AddEditTripViewModel;
                if (vm != null)
                {
                    vm.AddEditTripCommand.Execute(null);
                }

                if (vm.IsFormValid)
                {
                    // En appelant directement la page principale on rafraichit celle-ci pour mettre a jour la liste des voyages
                    (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
                }
            });
        }

        /// <summary>
        /// Annuler le voyage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconCancel_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
        }

        private void HubTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            PhotoChooserTask photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += photoChooserTask_Completed;
            photoChooserTask.ShowCamera = true;
            photoChooserTask.PixelHeight = 500;
            photoChooserTask.PixelWidth = 500;
            photoChooserTask.Show();
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                (this.DataContext as AddEditTripViewModel).MainImage = Utils.Utility.ReadFully(e.ChosenPhoto);
                hubTile.Source = Utility.ByteArrayToImage((this.DataContext as AddEditTripViewModel).MainImage);
            }
            
        }

        private async void DepMap_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        { 
            ReverseGeocodeQuery query;
            List<MapLocation> mapLocations;
            string pushpinContent;
            MapLocation mapLocation;

            query = new ReverseGeocodeQuery();
            query.GeoCoordinate = this.DepMap.ConvertViewportPointToGeoCoordinate(e.GetPosition(this.DepMap));

            mapLocations = (List<MapLocation>)await query.GetMapLocationsAsync();
            mapLocation = mapLocations.FirstOrDefault();

            if (mapLocation != null)
            {  
                MapLayer pinLayout = new MapLayer();
                Pushpin MyPushpin = new Pushpin();
                MapOverlay pinOverlay = new MapOverlay();
                if(this.DepMap.Layers.Count > 0)
                {
                    this.DepMap.Layers.RemoveAt(this.DepMap.Layers.Count - 1);
                }
                
                this.DepMap.Layers.Add(pinLayout);

                MyPushpin.GeoCoordinate = mapLocation.GeoCoordinate;

                pinOverlay.Content = MyPushpin;
                pinOverlay.GeoCoordinate = mapLocation.GeoCoordinate;
                pinLayout.Add(pinOverlay);

                pushpinContent = mapLocation.Information.Name;
                pushpinContent = string.IsNullOrEmpty(pushpinContent) ? mapLocation.Information.Description : null;
                pushpinContent = string.IsNullOrEmpty(pushpinContent) ? string.Format("{0} {1} {2} ", mapLocation.Information.Address.Street, mapLocation.Information.Address.City, mapLocation.Information.Address.Country) : null;

                MyPushpin.Content = pushpinContent.Trim();
                MyPushpin.Visibility = Visibility.Visible;

                this.DepartureTextBox.Text = MyPushpin.Content.ToString();
      
             }
        }

        private async void DestMap_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ReverseGeocodeQuery query;
            List<MapLocation> mapLocations;
            string pushpinContent;
            MapLocation mapLocation;

            query = new ReverseGeocodeQuery();
            query.GeoCoordinate = this.DestMap.ConvertViewportPointToGeoCoordinate(e.GetPosition(this.DestMap));

            mapLocations = (List<MapLocation>)await query.GetMapLocationsAsync();
            mapLocation = mapLocations.FirstOrDefault();

            if (mapLocation != null)
            {

                MapLayer pinLayout = new MapLayer();
                Pushpin MyPushpin = new Pushpin();
                MapOverlay pinOverlay = new MapOverlay();
                if (this.DestMap.Layers.Count > 0)
                {
                    this.DestMap.Layers.RemoveAt(this.DestMap.Layers.Count - 1);
                }

                this.DestMap.Layers.Add(pinLayout);

                MyPushpin.GeoCoordinate = mapLocation.GeoCoordinate;

                pinOverlay.Content = MyPushpin;
                pinOverlay.GeoCoordinate = mapLocation.GeoCoordinate;
                pinLayout.Add(pinOverlay);

                pushpinContent = mapLocation.Information.Name;
                pushpinContent = string.IsNullOrEmpty(pushpinContent) ? mapLocation.Information.Description : null;
                pushpinContent = string.IsNullOrEmpty(pushpinContent) ? string.Format("{0} {1} {2} ", mapLocation.Information.Address.Street, mapLocation.Information.Address.City, mapLocation.Information.Address.Country) : null;

                MyPushpin.Content = pushpinContent.Trim();
                MyPushpin.Visibility = Visibility.Visible;

                this.DestinationTextBox.Text = MyPushpin.Content.ToString();
            }
        }
    }
}