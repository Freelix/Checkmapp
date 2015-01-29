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
            int id = (int)PhoneApplicationService.Current.State["id"];
            this.DataContext = new PhotoViewModel(id);
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
            int id = (int)IDPictureTextBlock.Tag;

            if (id != 0)
            {
                PhoneApplicationService.Current.State["Mode"] = Mode.edit;
                PhoneApplicationService.Current.State["id"] = id;
                NavigationService.Navigate(new Uri("/Views/PhotoViews/AddEditPhotoView.xaml", UriKind.Relative));
            }
        }

        #endregion
    }
}