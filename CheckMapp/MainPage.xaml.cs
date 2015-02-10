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

namespace CheckMapp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
            MainPanorama.SelectionChanged += MainPanorama_SelectionChanged;
        }

        /// <summary>
        /// Quand le panorama change, la barre d'application aussi, on doit l,updater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainPanorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (MainPanorama.SelectedIndex)
            {
                case 1:
                    ApplicationBar = (ApplicationBar)Resources["currentTripApplicationBar"];
                    ApplicationBar.IsVisible = true;
                    if (ApplicationBar.Buttons[0] != null)
                    {
                        (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Edit;
                    }
                    break;
                default:
                    if(ApplicationBar!=null)
                        ApplicationBar.IsVisible = false;
                    break;
            }
        }

        /// <summary>
        /// Edition du voyage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconButtonEdit_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/TripViews/TripView.xaml", UriKind.Relative));
        }

    }
}