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
using Windows.Devices.Geolocation;
using CheckMapp.Resources;
using Microsoft.Phone.Maps.Controls;
using System.Collections.ObjectModel;
using Microsoft.Phone.Maps.Toolkit;
using System.Collections;

namespace CheckMapp.Views
{
    public partial class StatisticView : PhoneApplicationPage
    {
        Trip currentTrip;
        public StatisticView()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            currentTrip = (Trip)PhoneApplicationService.Current.State["Trip"];
            if (currentTrip.IsActif)
                EndStack.Visibility = Visibility.Collapsed;
            else
                EndStack.Visibility = Visibility.Visible;    
            this.DataContext = new StatisticViewModel(currentTrip);


            //Bind les POI a la map
            ObservableCollection<DependencyObject> children = MapExtensions.GetChildren(statsMap);
            var obj = children.FirstOrDefault(x => x.GetType() == typeof(MapItemsControl)) as MapItemsControl;
            if (obj.ItemsSource != null)
            {
                (obj.ItemsSource as IList).Clear();
                obj.ItemsSource = null;
            }
            obj.ItemsSource = ((this.DataContext as StatisticViewModel).PointOfInterestList);
        }

        private void statsMap_Loaded(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.Sleep(500);

            var poiList = (this.DataContext as StatisticViewModel).PointOfInterestList;
            if (poiList.Count > 0)
            {
                var bounds = new LocationRectangle(
                    poiList.Max((p) => p.Latitude),
                    poiList.Min((p) => p.Longitude),
                    poiList.Min((p) => p.Latitude),
                    poiList.Max((p) => p.Longitude));
                statsMap.SetView(bounds);
            }
        }
    }
}