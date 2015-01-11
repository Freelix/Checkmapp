using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;

namespace CheckMapp.Pages
{
    public partial class MapPage : PhoneApplicationPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        private void Map_ZoomLevelChanged(object sender, Microsoft.Phone.Maps.Controls.MapZoomLevelChangedEventArgs e)
        {
            if (((Map)sender).ZoomLevel > 5)
            {
                ((Map)sender).ZoomLevel = 5;
            }
        }
    }
}