using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.ViewModels;
using CheckMapp.Model.Tables;

namespace CheckMapp.Views
{
    public partial class StatisticView : PhoneApplicationPage
    {
        Trip currentTrip;
        public StatisticView()
        {
            currentTrip = (Trip)PhoneApplicationService.Current.State["Trip"];
            InitializeComponent();
            this.DataContext = new StatisticViewModel(currentTrip);
        }
    }
}