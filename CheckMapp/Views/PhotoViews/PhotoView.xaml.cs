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
using CheckMapp.Model.DataService;
using System.Windows.Data;
using CheckMapp.Converter;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using CheckMapp.Controls;

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
            Picture current = PhoneApplicationService.Current.State["Picture"] as Picture;
            this.DataContext = new PhotoViewModel(0,current.Trip);
            MyPivot.Items.Clear();
            int index = 0;
            foreach (Picture picture in (this.DataContext as PhotoViewModel).PictureList)
            {
                PivotItem item = new PivotItem();
                item.Margin = new Thickness(0, -135, 0, 0);
                ByteToImageConverter con = new ByteToImageConverter();
                PinchAndZoomImage img = new PinchAndZoomImage();
                img.Picture = picture;
                img.Tap += img_Tap;
                item.Content = img;
                MyPivot.Items.Add(item);
                if (picture == current)
                    MyPivot.SelectedIndex = index;
                else
                    index++;

            }

            //Clear memory
            PhoneApplicationService.Current.State["Picture"] = null;
            base.OnNavigatedTo(e);
        }


        void img_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplicationBar.IsVisible = !ApplicationBar.IsVisible;
            (sender as PinchAndZoomImage).IsTextVisible = ApplicationBar.IsVisible;
        }

        #region Buttons

        private void IconDelete_Click(object sender, EventArgs e)
        {
            // Call the appropriate function in ViewModel
            var vm = DataContext as PhotoViewModel;
            if (vm != null)
                vm.DeletePictureCommand.Execute(null);

            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
        }

        private void IconEdit_Click(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["Picture"] = (this.DataContext as PhotoViewModel).PictureList[MyPivot.SelectedIndex];
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