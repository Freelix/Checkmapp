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
using CheckMapp.Utils;
using CheckMapp.ViewModels;

namespace CheckMapp.Views.POIViews
{
    public partial class AddEditPOIView : PhoneApplicationPage
    {
        public AddEditPOIView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Trip trip = (Trip)PhoneApplicationService.Current.State["Trip"];
            Mode mode = (Mode)PhoneApplicationService.Current.State["Mode"];
            PointOfInterest currentPoi = (PointOfInterest)PhoneApplicationService.Current.State["Poi"];
            this.DataContext = new AddEditPOIViewModel(trip, mode, currentPoi);

            //Assigne le titre de la page
            AddEditPOIViewModel vm = DataContext as AddEditPOIViewModel;
            if (vm.Mode == Mode.add)
                TitleTextBox.Text = AppResources.AddPOI.ToLower();
            else if (vm.Mode == Mode.edit)
                TitleTextBox.Text = AppResources.EditPoi.ToLower();

            if (mode == Mode.edit)
                Utils.Utility.AddLocation(this.myMap, this.PoiTextBox, null, currentPoi.Latitude, currentPoi.Longitude);

            btn_place.IsEnabled = !String.IsNullOrEmpty(PoiTextBox.Text);
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
                var vm = DataContext as AddEditPOIViewModel;
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
           await Utils.Utility.AddLocation(this.myMap, this.PoiTextBox, e, 0.0, 0.0, true);
           MapLayer layer = this.myMap.Layers.FirstOrDefault();
           (this.DataContext as AddEditPOIViewModel).Latitude = layer[0].GeoCoordinate.Latitude;
           (this.DataContext as AddEditPOIViewModel).Longitude = layer[0].GeoCoordinate.Longitude;
        }

        private void btn_place_Click(object sender, RoutedEventArgs e)
        {
            AfficherCarte(this.PoiTextBox, this.myMap);
        }

        private async void AfficherCarte(PhoneTextBox myTextBox, Microsoft.Phone.Maps.Controls.Map myMap)
        {
            try
            {
                var CoordinateList = await MapHelper.getCoordinateAsync(myTextBox.Text);

                // CoordinateList[0] = latitude, CoordinateList[1] = longitude
                await Utils.Utility.AddLocation(myMap, myTextBox, null, CoordinateList[0], CoordinateList[1], true);

                (this.DataContext as AddEditPOIViewModel).PointOfInterest.Latitude = CoordinateList[0];
                (this.DataContext as AddEditPOIViewModel).PointOfInterest.Longitude = CoordinateList[1];
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(AppResources.InvalideSearch, myTextBox.Text), AppResources.Warning, MessageBoxButton.OK);
            }
        }

        private void PoiTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn_place.IsEnabled = !String.IsNullOrEmpty((sender as TextBox).Text);
        }

    }
}