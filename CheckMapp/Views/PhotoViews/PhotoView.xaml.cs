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
using Microsoft.Phone.Tasks;
using Windows.Storage;
using System.IO;
using Microsoft.Xna.Framework.Media.PhoneExtensions;
using System.IO.IsolatedStorage;
using CheckMapp.KeyGroup;

namespace CheckMapp.Views.PhotoViews
{
    public partial class PhotoView : PhoneApplicationPage
    {
        public PhotoView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            myImage.Bitmap.ClearValue(BitmapImage.UriSourceProperty);
            PhoneApplicationService.Current.State["Trip"] = (this.DataContext as PhotoViewModel).Trip;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Picture current = PhoneApplicationService.Current.State["Picture"] as Picture;
            this.DataContext = new PhotoViewModel(current);
            int currentIndex = (this.DataContext as PhotoViewModel).GroupedPhotos[0].IndexOf(current);
            (this.DataContext as PhotoViewModel).SelectedPictureIndex = currentIndex;

            myImage.Picture = current;

            PhoneApplicationService.Current.State["Trip"] = null;
            PhoneApplicationService.Current.State["Picture"] = null;
            base.OnNavigatedTo(e);
        }

        private void GestureListener_Flick(object sender, FlickGestureEventArgs e)
        {
            if (!myImage.IsOrigin)
                return;
            var vm = (this.DataContext as PhotoViewModel);

            // User flicked towards gauche
            if (e.HorizontalVelocity > 0)
            {
                myImage.Bitmap.ClearValue(BitmapImage.UriSourceProperty);
                // Load the next image 
                vm.SelectedPictureIndex -= 1;
                if (vm.SelectedPictureIndex < 0)
                    vm.SelectedPictureIndex = vm.GroupedPhotos[0].Count - 1;
            }

            // User flicked towards droit
            if (e.HorizontalVelocity < 0)
            {
                myImage.Bitmap.ClearValue(BitmapImage.UriSourceProperty);
                // Load the previous image
                vm.SelectedPictureIndex += 1;
                if (vm.SelectedPictureIndex >= vm.GroupedPhotos[0].Count)
                    vm.SelectedPictureIndex = 0;
            }

            myImage.Picture = vm.GroupedPhotos[0][vm.SelectedPictureIndex];
            myImage.InitializeImage();
            vm.SelectedPicture = myImage.Picture;
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
            PhoneApplicationService.Current.State["Picture"] = myImage.Picture;
            PhoneApplicationService.Current.State["Mode"] = Mode.edit;
            NavigationService.Navigate(new Uri("/Views/PhotoViews/AddEditPhotoView.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Share;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Edit;
                (ApplicationBar.Buttons[2] as ApplicationBarIconButton).Text = AppResources.Delete;
            }
        }

        #endregion

        private void IconShare_Click(object sender, EventArgs e)
        {
            //On a besoin d'un filePath donc on l,ajoute a la librarie temporairement
            //byte[] temp = (this.DataContext as PhotoViewModel).PictureList[(this.DataContext as PhotoViewModel).SelectedPicture].PictureData;

            //using (var ms = new MemoryStream(temp))
            //{
            //    ms.Seek(0, SeekOrigin.Begin);
            //    var lib = new Microsoft.Xna.Framework.Media.MediaLibrary();
            //    var picture = lib.SavePicture(string.Format("test.jpg"), ms);

            //    var task = new ShareMediaTask();

            //    task.FilePath = MediaLibraryExtensions.GetPath(picture);
            //    task.Show();

            //    //On le supprime par la suite.

            //}

        }



    }
}