using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.ViewModels.POIViewModels;

namespace CheckMapp.Views.POIViews
{
    public partial class POIView : PhoneApplicationPage
    {
        public POIView()
        {
            InitializeComponent();
            this.DataContext = new POIViewModel();
        }
    }
}