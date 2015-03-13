using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.ViewModels.TripViewModels;
using CheckMapp.Resources;
using CheckMapp.ViewModels;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using CheckMapp.Utils;
using CheckMapp.Model.Tables;
using System.Device.Location;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Maps.Controls;
using System.Threading.Tasks;
using Microsoft.Phone.UserData;

namespace CheckMapp.Views.TripViews
{
    public partial class AddEditTripView : PhoneApplicationPage
    {
        public AddEditTripView()
        {
            InitializeComponent();

            Mode mode = (Mode)PhoneApplicationService.Current.State["Mode"];
            Trip currentTrip = (Trip)PhoneApplicationService.Current.State["Trip"];
            this.DataContext = new AddEditTripViewModel(currentTrip, mode);

            //Assigne le titre de la page
            var vm = this.DataContext as AddEditTripViewModel;
            if (vm.Mode == Mode.add)
                TitleTextblock.Text = AppResources.AddTrip.ToLower();
            else
            {
                TitleTextblock.Text = AppResources.EditTrip.ToLower();
            }
        }

        /// <summary>
        /// On assigne les titres des boutons au démarrage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Save;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Cancel;
            }
        }

        /// <summary>
        /// Sauvegarder le voyage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconSave_Click(object sender, EventArgs e)
        {
            this.Focus();
            Dispatcher.BeginInvoke(() =>
            {
                var vm = DataContext as AddEditTripViewModel;
                if (vm != null)
                {
                    if (vm.Trip.MainPictureData == null)
                    {
                        //Si l'usager ne met pas d'image on en met une par défaut
                        BitmapImage logo = new BitmapImage();
                        logo.UriSource = new Uri(@"/Assets/Logo.png", UriKind.Relative);
                        logo.CreateOptions = BitmapCreateOptions.BackgroundCreation;
                        logo.ImageOpened += logo_ImageOpened;
                    }
                    else
                    {
                        vm.AddEditTripCommand.Execute(null);
                        if (vm.IsFormValid)
                        {
                            // En appelant directement la page principale on rafraichit celle-ci pour mettre a jour la liste des voyages
                            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
                        }
                    }
                }
            });
        }

        void logo_ImageOpened(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as AddEditTripViewModel;
            vm.Trip.MainPictureData = Utils.Utility.ConvertToBytes(sender as BitmapImage);

            vm.AddEditTripCommand.Execute(null);

            if (vm.IsFormValid)
            {
                // En appelant directement la page principale on rafraichit celle-ci pour mettre a jour la liste des voyages
                (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
            }
        }

        /// <summary>
        /// Annuler le voyage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconCancel_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
        }

        private void HubTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhotoChooserTask photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += photoChooserTask_Completed;
            photoChooserTask.ShowCamera = true;
            photoChooserTask.PixelHeight = 500;
            photoChooserTask.PixelWidth = 500;
            photoChooserTask.Show();
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                (this.DataContext as AddEditTripViewModel).MainImage = Utils.Utility.ReadFully(e.ChosenPhoto);
                hubTile.Source = Utility.ByteArrayToImage((this.DataContext as AddEditTripViewModel).MainImage,false);
            }

            //La valeur s'enleve pour une raison inconnu encore, alors on doit la réassigner
            PhoneApplicationService.Current.State["Trip"] = (this.DataContext as AddEditTripViewModel).Trip;
        }

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            AddressChooserTask phoneNumberChooserTask = new AddressChooserTask();
            phoneNumberChooserTask.Completed += phoneNumberChooserTask_Completed;
            phoneNumberChooserTask.Show();
        }

        void phoneNumberChooserTask_Completed(object sender, AddressResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                (this.DataContext as AddEditTripViewModel).FriendList.Add(e.DisplayName);
                FriendLLS.ItemsSource = null;
                FriendLLS.ItemsSource = (this.DataContext as AddEditTripViewModel).FriendList;
            }
        }

        private void DeleteFriend_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && ((sender as MenuItem).DataContext is string))
            {
                string selected = ((sender as MenuItem).DataContext as string);
                (this.DataContext as AddEditTripViewModel).FriendList.Remove(selected);
                FriendLLS.ItemsSource = null;
                FriendLLS.ItemsSource = (this.DataContext as AddEditTripViewModel).FriendList;
            }
        }

        private void ContextMenuNote_Opened(object sender, RoutedEventArgs e)
        {
            var menu = (ContextMenu)sender;
            var owner = (FrameworkElement)menu.Owner;
            if (owner.DataContext != menu.DataContext)
                menu.DataContext = owner.DataContext;
        }

    }
}