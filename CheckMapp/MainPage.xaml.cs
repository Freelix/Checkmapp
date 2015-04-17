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

            Trip current = null;

            // This part is used for the dormant state
            // Without it, the trip will not show the added objects !
            if (PhoneApplicationService.Current.State["Trip"] != null)
                current = (Trip)PhoneApplicationService.Current.State["Trip"];
            else
                current  = (this.DataContext as MainViewModel).TripActif;

            CheckUpdateTile(current);

            PhoneApplicationService.Current.State["Trip"] = current;
            if ((this.DataContext as MainViewModel).IsTripActif)
                CurrentView.DataContext = new CurrentViewModel(current);

            DashboardView.LoadComponents((this.DataContext as MainViewModel).IsTripActif);
        }

        /// <summary>
        /// Met a jour la tuile sur l'écran d'accueil avec les nouvelles infos du voyage en cours
        /// </summary>
        /// <param name="current"></param>
        public void CheckUpdateTile(Trip current)
        {
            IconicTileData newTileData = new IconicTileData();
            newTileData.Title = "Checkmapp";
            newTileData.WideContent1 = String.Empty;
            newTileData.WideContent2 = String.Empty;
            newTileData.IconImage = new Uri(@"Assets/Logo.png", UriKind.Relative);
            newTileData.SmallIconImage = new Uri(@"Assets/Logo.png", UriKind.Relative);
            if (current != null)
            {
                newTileData.WideContent1 = current.Name;
                int day = 0;
                TimeSpan elapsed = DateTime.Now.Subtract(current.BeginDate);
                if (elapsed.TotalDays > 0)
                    day = (int)elapsed.TotalDays;

                newTileData.WideContent2 = AppResources.Day + " " + day;
            }

            //Mise a jour (pour le texte)
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault();
            if (tile != null)
                tile.Update(newTileData);
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
            PhoneApplicationService.Current.State["Trip"] = (CurrentView.DataContext as CurrentViewModel).Trip;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/TripViews/TripView.xaml", UriKind.Relative));
        }

    }
}