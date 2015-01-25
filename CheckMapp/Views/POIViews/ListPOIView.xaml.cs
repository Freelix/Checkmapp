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

namespace CheckMapp.Views.POIViews
{
    public partial class ListPOIView : PhoneApplicationPage
    {
        public ListPOIView()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            this.DataContext = new ListPOIViewModel();
            ListboxPOI.DataContext = (this.DataContext as ListPOIViewModel).PointOfInterestList;
            ListboxPOI.SelectionChanged += ListboxPOI_SelectionChanged;
        }

        void ListboxPOI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Montre le POI dans la carte
        }

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                switch (menuItem.Name)
                {
                    case "POIPictures":
                        break;
                    case "POINotes":
                        break;
                    case "DeletePOI":
                        MessageBox.Show("Are you sure you want to delete this trip?");
                        break;
                }
            }
        }

        private void IconAdd_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/POIViews/AddPOIView.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.AddPOI;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
                loadData();
        }
    }
}