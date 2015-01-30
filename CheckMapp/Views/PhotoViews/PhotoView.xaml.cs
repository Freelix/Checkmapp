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
using CheckMapp.Model.Tables;
using CheckMapp.ViewModels;
using Utility = CheckMapp.Utils.Utility;
using CheckMapp.Resources;

namespace CheckMapp.Views.PhotoViews
{
    public partial class PhotoView : PhoneApplicationPage
    {
        public PhotoView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Picture myPicture = (Picture)PhoneApplicationService.Current.State["Picture"];
            this.DataContext = new PhotoViewModel(myPicture);
            base.OnNavigatedTo(e);
        }

        private void OnFlick(object sender, FlickGestureEventArgs e)
        {
            var vm = DataContext as PhotoViewModel;
            if (vm != null)
            {
                // User flicked towards left
                if (e.HorizontalVelocity < 0)
                {
                    // Load the next image 
                    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/PhotoViews/PhotoView.xaml", UriKind.Relative));
                }

                // User flicked towards right
                if (e.HorizontalVelocity > 0)
                {
                    // Load the previous image
                    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/PhotoViews/PhotoView.xaml", UriKind.Relative));
                }
            }
        }

        #region Buttons

        private void IconDelete_Click(object sender, EventArgs e)
        {
            // Call the appropriate function in ViewModel
            var vm = DataContext as PhotoViewModel;
            if (vm != null)
            {
                vm.DeletePictureCommand.Execute(null);
            }

            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
        }

        private void IconEdit_Click(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["Mode"] = Mode.edit;
            NavigationService.Navigate(new Uri("/Views/PhotoViews/AddEditPhotoView.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Edit;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Delete;
            }
        }

        #endregion
    }
}