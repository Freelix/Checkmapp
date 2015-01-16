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

namespace CheckMapp.Views.PhotoViews
{
    public partial class PhotoView : PhoneApplicationPage
    {
        public PhotoView()
        {
            InitializeComponent();
            this.DataContext = new PhotoViewModel();
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
    }
}