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
            {
                TitleTextblock.Text = AppResources.EditTrip.ToLower();
                AddLocation_onEdit(currentTrip);
            }
                

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
                // En appelant directement la page principale on rafraichit celle-ci pour mettre a jour la liste des voyages
                (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
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
            await AddLocation_onHold(this.DepMap, this.DepartureTextBox, e);
        }

        private async void DestMap_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await AddLocation_onHold(this.DestMap, this.DestinationTextBox, e);
        }

        private void btn_dep_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as AddEditTripViewModel;
            AfficherCarte(vm, this.DepartureTextBox, this.DepMap);
        }

        private void btn_dest_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as AddEditTripViewModel;
            AfficherCarte(vm, this.DestinationTextBox, this.DestMap);
        }

        private async System.Threading.Tasks.Task AddLocation_onHold(Microsoft.Phone.Maps.Controls.Map myMap, TextBox myTextBox, System.Windows.Input.GestureEventArgs e)
        {
            ReverseGeocodeQuery query;
            List<MapLocation> mapLocations;
            string pushpinContent;
            MapLocation mapLocation;

            query = new ReverseGeocodeQuery();
            query.GeoCoordinate = myMap.ConvertViewportPointToGeoCoordinate(e.GetPosition(myMap));

            mapLocations = (List<MapLocation>)await query.GetMapLocationsAsync();
            mapLocation = mapLocations.FirstOrDefault();

            if (mapLocation != null)
            {
                MapLayer pinLayout = new MapLayer();
                Pushpin MyPushpin = new Pushpin();
                MapOverlay pinOverlay = new MapOverlay();
                if (myMap.Layers.Count > 0)
                {
                    myMap.Layers.RemoveAt(myMap.Layers.Count - 1);
                }

                myMap.Layers.Add(pinLayout);

                MyPushpin.GeoCoordinate = mapLocation.GeoCoordinate;

                pinOverlay.Content = MyPushpin;
                pinOverlay.GeoCoordinate = mapLocation.GeoCoordinate;
                pinLayout.Add(pinOverlay);

                pushpinContent = mapLocation.Information.Name;
                pushpinContent = string.IsNullOrEmpty(pushpinContent) ? mapLocation.Information.Description : null;
                pushpinContent = string.IsNullOrEmpty(pushpinContent) ? string.Format("{0} {1} {2} ", mapLocation.Information.Address.Street, mapLocation.Information.Address.City, mapLocation.Information.Address.Country) : null;

                MyPushpin.Content = pushpinContent.Trim();
                MyPushpin.Visibility = Visibility.Visible;

                myTextBox.Text = MyPushpin.Content.ToString();

            }
        }

        public async void AddLocation_onEdit(Trip currentTrip)
        {
            await AddLocation_onEdit(this.DepMap, this.DepartureTextBox, currentTrip.DepartureLatitude, currentTrip.DepartureLongitude);
            await AddLocation_onEdit(this.DestMap, this.DestinationTextBox, currentTrip.DestinationLatitude, currentTrip.DestinationLongitude);
        }

        private async System.Threading.Tasks.Task AddLocation_onEdit(Microsoft.Phone.Maps.Controls.Map myMap, TextBox myTextBox, double latitude, double longitude)
        {
            List<MapLocation> mapLocations;
            MapLocation mapLocation;
            string pushpinContent;
            ReverseGeocodeQuery query = new ReverseGeocodeQuery();
            query.GeoCoordinate = new GeoCoordinate(latitude, longitude);
           
            mapLocations =(List<MapLocation>)await query.GetMapLocationsAsync();
            mapLocation = mapLocations.FirstOrDefault();

            if (mapLocation != null)
            {
                MapLayer pinLayout = new MapLayer();
                Pushpin MyPushpin = new Pushpin();
                MapOverlay pinOverlay = new MapOverlay();
                if (myMap.Layers.Count > 0)
                {
                    myMap.Layers.RemoveAt(myMap.Layers.Count - 1);
                }

                myMap.Layers.Add(pinLayout);

                MyPushpin.GeoCoordinate = mapLocation.GeoCoordinate;

                pinOverlay.Content = MyPushpin;
                pinOverlay.GeoCoordinate = mapLocation.GeoCoordinate;
                pinLayout.Add(pinOverlay);

                pushpinContent = mapLocation.Information.Name;
                pushpinContent = string.IsNullOrEmpty(pushpinContent) ? mapLocation.Information.Description : null;
                pushpinContent = string.IsNullOrEmpty(pushpinContent) ? string.Format("{0} {1} {2} ", mapLocation.Information.Address.Street, mapLocation.Information.Address.City, mapLocation.Information.Address.Country) : null;

                MyPushpin.Content = pushpinContent.Trim();
                MyPushpin.Visibility = Visibility.Visible;

                myTextBox.Text = MyPushpin.Content.ToString();
                    
            }
        }

        private async void AfficherCarte(AddEditTripViewModel vm, TextBox myTextBox, Microsoft.Phone.Maps.Controls.Map myMap)
        {
            try
            {
                var CoordinateList = await vm.getCoordinateAsync(myTextBox.Text);
                await AddLocation_onEdit(myMap, myTextBox, CoordinateList[0], CoordinateList[1]);
            }
            catch(Exception e)
            {
                Console.Out.WriteLine(e);
            }
        }

    }
}