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

namespace CheckMapp.Views.TripViews
{
    public partial class TripView : PhoneApplicationPage
    {
        public TripView()
        {
            InitializeComponent();
            this.DataContext = new TripViewModel(1);
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
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/PhotoViews/AddEditPhotoView.xaml", UriKind.Relative));
            }
        }

        private void IconButtonAddNotes_Click(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["Mode"] = Mode.add;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/NoteViews/AddEditNoteView.xaml", UriKind.Relative));
        }

        private void IconButtonAddPOI_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/POIViews/AddPOIView.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null && ApplicationBar.MenuItems != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.AddPicture;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.AddNote;
                (ApplicationBar.Buttons[2] as ApplicationBarIconButton).Text = AppResources.AddPOI;

                (ApplicationBar.MenuItems[0] as ApplicationBarMenuItem).Text = AppResources.Delete;
                (ApplicationBar.MenuItems[1] as ApplicationBarMenuItem).Text = AppResources.Edit;
                (ApplicationBar.MenuItems[2] as ApplicationBarMenuItem).Text = AppResources.FinishTrip;
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

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
           // int id = (int)PhoneApplicationService.Current.State["id"];
            //this.DataContext = new TripViewModel(id);
            base.OnNavigatedTo(e);
        }
    }
}