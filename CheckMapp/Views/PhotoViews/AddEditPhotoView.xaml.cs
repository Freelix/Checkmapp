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
using CheckMapp.ViewModels;
using CheckMapp.Resources;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using CheckMapp.Model.Tables;
using System.IO;
using CheckMapp.Utils;

namespace CheckMapp.Views.PhotoViews
{
    public partial class AddEditPhotoView : PhoneApplicationPage
    {
        public AddEditPhotoView()
        {
            InitializeComponent();

            // On vide la mémoire le plus possible
            PhoneApplicationService.Current.State["TombstoneMode"] = false;

            LoadPage();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            
            Uri destination = e.Uri;

            if (!destination.OriginalString.Contains("DatePickerPage.xaml"))
            {
                if(hubTile.Source!=null)
                    hubTile.Source.ClearValue(BitmapImage.UriSourceProperty);
            }

            // Save the note instance to retrieve when a tombstone occured
            AddEditPhotoViewModel vm = DataContext as AddEditPhotoViewModel;

            //On annule les changeemnts si l'usager fait BACK
            if (e.NavigationMode == NavigationMode.Back && !vm.IsFormValid)
                vm.CancelPhotoCommand.Execute(null);

            PhoneApplicationService.Current.State["Picture"] = vm.Picture.Id;

            if (vm.POISelected != null)
                PhoneApplicationService.Current.State["POISelected"] = vm.POISelected;
            else if (vm.PoiList.Count > 0)
                PhoneApplicationService.Current.State["POISelected"] = vm.PoiList[0];
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Utility.IsTombstoned())
            {
                PhoneApplicationService.Current.State["TombstoneMode"] = true;
                LoadPage();
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Save;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Cancel;
            }
        }

        private void IconSave_Click(object sender, EventArgs e)
        {
            this.Focus();

            // wait till the next UI thread tick so that the binding gets updated
            Dispatcher.BeginInvoke(() =>
            {
                var vm = DataContext as AddEditPhotoViewModel;
                vm.AddEditPhotoCommand.Execute(null);
                if (vm.IsFormValid)
                    (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
            });

        }

        private void IconCancel_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
        }

        private void HubTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhotoChooserTask photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += photoChooserTask_Completed;
            photoChooserTask.ShowCamera = true;
            photoChooserTask.Show();
        }

        private void LoadPage()
        {
            int trip = (int)PhoneApplicationService.Current.State["Trip"];
            Mode mode = (Mode)PhoneApplicationService.Current.State["Mode"];
            int myPicture = (int)PhoneApplicationService.Current.State["Picture"];

            this.DataContext = new AddEditPhotoViewModel(trip, myPicture, mode);

            //Assigne le titre de la page
            var vm = this.DataContext as AddEditPhotoViewModel;

            if (vm.Mode == Mode.add)
                TitleTextblock.Text = AppResources.AddPicture.ToLower();
            else if (vm.Mode == Mode.edit)
                TitleTextblock.Text = AppResources.EditPicture.ToLower();
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                (this.DataContext as AddEditPhotoViewModel).ImageSource = Utils.Utility.ReadFully(e.ChosenPhoto);
                hubTile.Source = Utility.ByteArrayToImage((this.DataContext as AddEditPhotoViewModel).ImageSource, false);
            }

            PhoneApplicationService.Current.State["Trip"] = (this.DataContext as AddEditPhotoViewModel).Picture.Trip.Id;
        }
    }
}