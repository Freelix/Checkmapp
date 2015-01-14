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

namespace CheckMapp.Views.TripViews
{
    public partial class AddTripView : PhoneApplicationPage
    {
        public AddTripView()
        {
            InitializeComponent();
            this.DataContext = new AddTripViewModel();
        }
    }
}