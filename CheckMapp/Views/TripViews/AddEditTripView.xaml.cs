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
using System.Threading.Tasks;

namespace CheckMapp.Views.TripViews
{
    public partial class AddEditTripView : PhoneApplicationPage
    {
        public AddEditTripView()
        {
            InitializeComponent();

            if (!Utils.Utility.checkNetworkConnection())
            {
                MessageBox.Show(AppResources.InternetConnection, AppResources.NotConnected, MessageBoxButton.OK);
                this.DepartureTextBox.IsEnabled = false;
                this.DestinationTextBox.IsEnabled = false;
                this.btn_dep.Visibility = Visibility.Collapsed;
                this.btn_dest.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.DepartureTextBox.IsEnabled = true;
                this.DestinationTextBox.IsEnabled = true;
                this.btn_dep.Visibility = Visibility.Visible;
                this.btn_dest.Visibility = Visibility.Visible;
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
            await AddLocation_onHold(this.DepMap, this.DepartureTextBox, e);
            MapLayer layer = this.DepMap.Layers.FirstOrDefault();
            (this.DataContext as AddEditTripViewModel).Trip.DepartureLatitude = layer[0].GeoCoordinate.Latitude;
            (this.DataContext as AddEditTripViewModel).Trip.DepartureLongitude = layer[0].GeoCoordinate.Longitude;
        }

        private async void DestMap_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await AddLocation_onHold(this.DestMap, this.DestinationTextBox, e);
            MapLayer layer = this.DestMap.Layers.FirstOrDefault();
            (this.DataContext as AddEditTripViewModel).Trip.DestinationLatitude = layer[0].GeoCoordinate.Latitude;
            (this.DataContext as AddEditTripViewModel).Trip.DestinationLongitude = layer[0].GeoCoordinate.Longitude;
          
        }

        private void btn_dep_Click(object sender, RoutedEventArgs e)
        {
            AfficherCarte(this.DepartureTextBox, this.DepMap);
        }

        private void btn_dest_Click(object sender, RoutedEventArgs e)
        {
            AfficherCarte(this.DestinationTextBox, this.DestMap);
        }
        
        private async System.Threading.Tasks.Task AddLocation_onHold(Microsoft.Phone.Maps.Controls.Map myMap, PhoneTextBox myTextBox, System.Windows.Input.GestureEventArgs e)
        {
            await Utils.Utility.AddLocation(myMap, myTextBox, e, 0.0, 0.0);
        }

        private async void AddLocation_onEdit(Trip currentTrip)
        {
            await AddLocation_onEdit(this.DepMap, this.DepartureTextBox, currentTrip.DepartureLatitude, currentTrip.DepartureLongitude);
            await AddLocation_onEdit(this.DestMap, this.DestinationTextBox, currentTrip.DestinationLatitude, currentTrip.DestinationLongitude);
        }

        private async System.Threading.Tasks.Task AddLocation_onEdit(Microsoft.Phone.Maps.Controls.Map myMap, PhoneTextBox myTextBox, double latitude, double longitude)
        {
            await Utils.Utility.AddLocation(myMap, myTextBox, null, latitude, longitude);
        }
          
        private async void AfficherCarte(PhoneTextBox myTextBox, Microsoft.Phone.Maps.Controls.Map myMap)
        {
            try
            {
                var CoordinateList = await (this.DataContext as AddEditTripViewModel).getCoordinateAsync(myTextBox.Text);
               
                // CoordinateList[0] = latitude, CoordinateList[1] = longitude
                await AddLocation_onEdit(myMap, myTextBox, CoordinateList[0], CoordinateList[1]);
                if (myMap == this.DepMap)
                { 
                    (this.DataContext as AddEditTripViewModel).Trip.DepartureLatitude = CoordinateList[0];
                    (this.DataContext as AddEditTripViewModel).Trip.DepartureLongitude = CoordinateList[1];
                }
                else 
                {
                    (this.DataContext as AddEditTripViewModel).Trip.DestinationLatitude = CoordinateList[0];
                    (this.DataContext as AddEditTripViewModel).Trip.DestinationLongitude = CoordinateList[1];
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(string.Format(AppResources.InvalideSearch, myTextBox.Text), AppResources.Warning, MessageBoxButton.OK);
            }
        }



    }
}