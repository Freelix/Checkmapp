using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.Model.Tables;
using CheckMapp.ViewModel;
using CheckMapp.ViewModels.ArchivesViewModels;

namespace CheckMapp.Views.ArchivesViews
{
    public partial class TimelineView : UserControl
    {
        public TimelineView()
        {
            InitializeComponent();
            this.DataContext = MainViewModel.PageViewModels[1];
            timelineControl.Trips = (this.DataContext as TimelineViewModel).ArchiveTripList;
        }

        void timelineControl_UserControlElementTap(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["Trip"] = sender as Trip;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/TripViews/TripView.xaml", UriKind.Relative));
        }
    }
}
