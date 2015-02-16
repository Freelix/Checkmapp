using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.Resources;
using CheckMapp.ViewModel;
using System.Windows.Data;
using System.Windows.Markup;
using System.Threading;
using CheckMapp.Model.Tables;
using CheckMapp.ViewModels.TripViewModels;
using CheckMapp.Views;

namespace CheckMapp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            while (NavigationService.RemoveBackEntry() != null) ;

            this.DataContext = new MainViewModel();
            MainPanorama.SelectionChanged += MainPanorama_SelectionChanged;
            Trip current = (this.DataContext as MainViewModel).TripActif;
            PhoneApplicationService.Current.State["Trip"] = current;
            if ((this.DataContext as MainViewModel).IsTripActif)
                CurrentView.DataContext = new CurrentViewModel(current);

            DashboardView.LoadComponents((this.DataContext as MainViewModel).IsTripActif);
        }

        /// <summary>
        /// Quand le panorama change, la barre d'application aussi, on doit l,updater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainPanorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainPanorama.SelectedItem == CurrentTripItem)
            {
                ApplicationBar = (ApplicationBar)Resources["currentTripApplicationBar"];
                ApplicationBar.IsVisible = true;
                if (ApplicationBar.Buttons[0] != null)
                {
                    (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Edit;
                }
            }
            else
            {
                if (ApplicationBar != null)
                    ApplicationBar.IsVisible = false;
            }
        }

        /// <summary>
        /// Edition du voyage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconButtonEdit_Click(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["Trip"] = (CurrentView.DataContext as CurrentViewModel).CurrentTrip;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/TripViews/TripView.xaml", UriKind.Relative));
        }

    }
}