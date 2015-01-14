using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CheckMapp.Views.ArchivesViews
{
    public partial class TimelineView : UserControl
    {
        public TimelineView()
        {
            InitializeComponent();

            timelineControl.UserControlElementTap += timelineControl_UserControlElementTap;
        }

        void timelineControl_UserControlElementTap(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/TripViews/TripView.xaml", UriKind.Relative));
        }
    }
}
