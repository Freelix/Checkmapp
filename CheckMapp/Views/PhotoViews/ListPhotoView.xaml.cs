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

namespace CheckMapp.Views.PhotoViews
{
    public partial class ListPhotoView : PhoneApplicationPage
    {
        public ListPhotoView()
        {
            InitializeComponent();
            this.DataContext = new ListPhotoViewModel();
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
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/PhotoViews/PhotoView.xaml", UriKind.Relative));
        }
    }
}