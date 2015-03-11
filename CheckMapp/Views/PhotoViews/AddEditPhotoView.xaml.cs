﻿using System;
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

            Trip trip = (Trip)PhoneApplicationService.Current.State["Trip"];
            Mode mode = (Mode)PhoneApplicationService.Current.State["Mode"];
            Picture myPicture = (Picture)PhoneApplicationService.Current.State["Picture"];

            this.DataContext = new AddEditPhotoViewModel(trip, myPicture, mode, PhoneApplicationService.Current.State["ChosenPhoto"] as byte[]);

            //On vide la mémoire plus possible
            PhoneApplicationService.Current.State["ChosenPhoto"] = null;

            //Assigne le titre de la page
            var vm = this.DataContext as AddEditPhotoViewModel;

            if (vm.Mode == Mode.add)
                TitleTextblock.Text = AppResources.AddPicture.ToLower();
            else if (vm.Mode == Mode.edit)
                TitleTextblock.Text = AppResources.EditPicture.ToLower();

            if (vm.PoiList.Count == 0)
                chkNoPOI.IsEnabled = false;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
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

            // wait till the next UI thread tick so that the binding gets updated
            Dispatcher.BeginInvoke(() =>
            {
                var vm = DataContext as AddEditPhotoViewModel;
                if (vm != null)
                {
                    vm.AddEditPhotoCommand.Execute(null);
                }

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

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                (this.DataContext as AddEditPhotoViewModel).ImageSource = Utils.Utility.ReadFully(e.ChosenPhoto);
                hubTile.Source = Utility.ByteArrayToImage((this.DataContext as AddEditPhotoViewModel).ImageSource);
            }

            PhoneApplicationService.Current.State["Trip"] = (this.DataContext as AddEditPhotoViewModel).Trip;
        }

        #region CheckBox functions

        private void chkNoPOI_Checked(object sender, RoutedEventArgs e)
        {
            chkHide_Storyboard.Begin();
            poiListPicker.Visibility = Visibility.Collapsed;
        }

        private void chkNoPOI_UnChecked(object sender, RoutedEventArgs e)
        {
            chkShow_Storyboard.Begin();
            poiListPicker.Visibility = Visibility.Visible;
            poiListPicker.SelectedIndex = 0;
        }

        #endregion
    }
}