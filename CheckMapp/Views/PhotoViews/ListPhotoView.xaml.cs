using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.ViewModels.PhotoViewModels;
using CheckMapp.Resources;
using CheckMapp.ViewModels;
using CheckMapp.Model.Tables;

namespace CheckMapp.Views.PhotoViews
{
    public partial class ListPhotoView : PhoneApplicationPage
    {
        public ListPhotoView()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            this.DataContext = new ListPhotoViewModel();
            PhotoHubLLS.ItemsSource = (this.DataContext as ListPhotoViewModel).GroupedPhotos;
            PhotoHubLLS.SelectionChanged += PhotoHubLLS_SelectionChanged;
            PhotoHubLLS.SelectedItem = null;
        }

        private void IconAdd_Click(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["Mode"] = Mode.add;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/PhotoViews/AddEditPhotoView.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.AddPicture;
            }
        }

        private void PhotoHubLLS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PhotoHubLLS.SelectedItem != null)
            {
                int pictureId = (PhotoHubLLS.SelectedItem as Picture).Id;
                PhoneApplicationService.Current.State["id"] = pictureId;
                NavigationService.Navigate(new Uri("/Views/PhotoViews/PhotoView.xaml", UriKind.Relative));
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
                loadData();
        }

        /// <summary>
        /// Click sur les options du menu contextuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                switch (menuItem.Name)
                {
                    case "EditPhoto":
                        PhoneApplicationService.Current.State["Mode"] = Mode.edit;
                        (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/PhotoViews/AddEditPhotoView.xaml", UriKind.Relative));
                        break;
                    case "DeletePhoto":
                        MessageBox.Show("Are you sure you want to delete this picture?");
                        break;
                }
            }
        }
    }
}