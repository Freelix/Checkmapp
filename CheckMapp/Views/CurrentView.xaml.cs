﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.ViewModel;
using System.Windows.Markup;
using System.Threading;

namespace CheckMapp.Views
{
    public partial class CurrentView : UserControl
    {
        public CurrentView()
        {
            InitializeComponent();
            this.DataContext = new CurrentViewModel();
        }
    }
}
