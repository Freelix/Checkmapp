using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.ViewModel;

namespace CheckMapp.Views
{
    public partial class TripView : PhoneApplicationPage
    {
        public TripView()
        {
            InitializeComponent();
            this.DataContext = new TripViewModel();
        }
    }
}