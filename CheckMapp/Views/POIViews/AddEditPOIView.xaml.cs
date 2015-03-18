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
            var vm = DataContext as AddEditPOIViewModel;
            vm.CancelPOICommand.Execute(null);

            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
        }

    }
}