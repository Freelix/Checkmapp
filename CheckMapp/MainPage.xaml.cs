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

namespace CheckMapp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
            // Affecter l'exemple de données au contexte de données du contrôle ListBox
            MainPanorama.SelectionChanged += MainPanorama_SelectionChanged;
            
            ApplicationBar = (ApplicationBar)Resources["dashboardApplicationBar"];
            ApplicationBar.IsVisible = true;

        }


        void MainPanorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ApplicationBar != null)
            {
                string pivotResource = String.Empty;
                
                switch (MainPanorama.SelectedIndex)
                {
                    case 0:
                        ApplicationBar.IsVisible = true;
                        ApplicationBar = (ApplicationBar)Resources["dashboardApplicationBar"];
                        break;
                    case 1:
                        ApplicationBar.IsVisible = true;
                        ApplicationBar = (ApplicationBar)Resources["currentTripApplicationBar"];
                        break;
                    case 2:
                        ApplicationBar.IsVisible = false;
                        break;
                    default:
                        ApplicationBar.IsVisible = false;
                        break;
                }
            }
        }


        // Charger les données pour les éléments ViewModel
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Console.WriteLine("Change");
            
        }

        private void IconButtonEdit_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/TripViews/TripView.xaml", UriKind.Relative));
        }


    }
}