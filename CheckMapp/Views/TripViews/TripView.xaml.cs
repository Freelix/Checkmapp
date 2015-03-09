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
using Microsoft.Phone.Tasks;
using CheckMapp.ViewModels;
using CheckMapp.Utils;
using System.Windows.Media.Imaging;
using System.IO;
using CheckMapp.Model.Tables;

namespace CheckMapp.Views.TripViews
{
    public partial class TripView : PhoneApplicationPage
    {
        public TripView()
        {
            InitializeComponent();
        }

        private void RoundButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/POIViews/POIView.xaml", UriKind.Relative));
        }

        private void IconButtonAddMedia_Click(object sender, EventArgs e)
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
                PhoneApplicationService.Current.State["Mode"] = Mode.add;
                PhoneApplicationService.Current.State["ChosenPhoto"] = Utility.ReadFully(e.ChosenPhoto);
                PhoneApplicationService.Current.State["Trip"] = (this.DataContext as TripViewModel).Trip;
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/PhotoViews/AddEditPhotoView.xaml", UriKind.Relative));
            }
            else
                PhoneApplicationService.Current.State["ChosenPhoto"] = null;
        }

        private void IconButtonAddNotes_Click(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["Mode"] = Mode.add;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/NoteViews/AddEditNoteView.xaml", UriKind.Relative));
        }

        private void IconButtonAddPOI_Click(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["Mode"] = Mode.add;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/POIViews/AddEditPOIView.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null && ApplicationBar.MenuItems != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.AddPicture;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.AddNote;
                (ApplicationBar.Buttons[2] as ApplicationBarIconButton).Text = AppResources.AddPOI;

                (ApplicationBar.MenuItems[1] as ApplicationBarMenuItem).Text = AppResources.Delete;
                (ApplicationBar.MenuItems[0] as ApplicationBarMenuItem).Text = AppResources.Edit;
                (ApplicationBar.MenuItems[2] as ApplicationBarMenuItem).Text = AppResources.FinishTrip;

                (ApplicationBar.MenuItems[1] as ApplicationBarMenuItem).IsEnabled = (this.DataContext as TripViewModel).Trip.IsActif;
                (ApplicationBar.MenuItems[0] as ApplicationBarMenuItem).IsEnabled = (this.DataContext as TripViewModel).Trip.IsActif;
            }
        }

        private void btnNote_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/NoteViews/ListNoteView.xaml", UriKind.Relative));
        }

        private void btnPOI_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/POIViews/ListPOIView.xaml", UriKind.Relative));
        }

        private void btnStats_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/StatisticView.xaml", UriKind.Relative));
        }

        private void btnPhoto_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/PhotoViews/ListPhotoView.xaml", UriKind.Relative));
        }

        private void EditTrip_Click(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["Mode"] = Mode.edit;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/TripViews/AddEditTripView.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Trip currentTrip = (Trip)PhoneApplicationService.Current.State["Trip"];
            this.DataContext = new TripViewModel(currentTrip);
            base.OnNavigatedTo(e);
        }

        private void FinisTrip_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(AppResources.ConfirmFinishTrip, "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                this.Focus();

                // wait till the next UI thread tick so that the binding gets updated
                Dispatcher.BeginInvoke(() =>
                {
                    var vm = DataContext as TripViewModel;
                    if (vm != null)
                    {
                        vm.FinishTripCommand.Execute(null);
                        PhoneApplicationService.Current.State["Trip"] = null;
                    }

                    // En appelant directement la page principale on rafraichit celle-ci pour mettre a jour le panorama
                    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                });
            }
        }

        private void DeleteTrip_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(AppResources.ConfirmationDeleteTrip, "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var vm = DataContext as TripViewModel;
                if (vm != null)
                {
                    vm.DeleteTripCommand.Execute(vm.Trip);
                    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
            }
        }
    }
}