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
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Mode mode = (Mode)PhoneApplicationService.Current.State["Mode"];
            this.DataContext = new AddEditPhotoViewModel(mode, PhoneApplicationService.Current.State["ChosenPhoto"] as byte[]);

            //Assigne le titre de la page
            var vm = this.DataContext as AddEditPhotoViewModel;
            if (vm.Mode == Mode.add)
            {
                TitleTextblock.Text = AppResources.AddPicture;
            }
            else
            {
                TitleTextblock.Text = AppResources.EditPicture;
            }

            base.OnNavigatedTo(e);
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

            if (((PointOfInterest)poiListPicker.SelectedItem) != null)
                poiTextBox.Text = ((PointOfInterest)poiListPicker.SelectedItem).Id.ToString();

            var vm = DataContext as AddEditPhotoViewModel;
            if (vm != null)
            {
                vm.AddEditPhotoCommand.Execute(null);
            }
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

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
                PhoneApplicationService.Current.State["ChosenPhoto"] = Utility.ReadFully(e.ChosenPhoto);

            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
        }
    }
}