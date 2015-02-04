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
using CheckMapp.Resources;
using CheckMapp.Model.Tables;

namespace CheckMapp.Views.POIViews
{
    public partial class ListPOIView : PhoneApplicationPage
    {
        public ListPOIView()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            this.DataContext = new ListPOIViewModel();
            POILLS.ItemsSource = (this.DataContext as ListPOIViewModel).PointOfInterestList;
        }

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && ((sender as MenuItem).DataContext is PointOfInterest))
            {
                PointOfInterest poiSelected = (sender as MenuItem).DataContext as PointOfInterest;
                switch (menuItem.Name)
                {
                    case "POIPictures":
                        break;
                    case "POINotes":
                        break;
                    case "DeletePOI":
                        if (MessageBox.Show(AppResources.ConfirmDeletePOI, "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            var vm = DataContext as ListPOIViewModel;
                            if (vm != null)
                                vm.DeletePOICommand.Execute(poiSelected);

                            vm.PointOfInterestList.Remove(poiSelected);
                            POILLS.ItemsSource = vm.PointOfInterestList;

                            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = (POILLS.ItemsSource.Count > 0);
                        }
                        break;
                }
            }
        }

        private void IconAdd_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/POIViews/AddPOIView.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            var appbar = this.Resources["AppBarList"] as ApplicationBar;
            if (appbar.Buttons != null)
            {
                (appbar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Select;
                (appbar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.AddPicture;
            }

            var appbarSelect = this.Resources["AppBarListSelect"] as ApplicationBar;
            if (appbarSelect.Buttons != null)
            {
                (appbarSelect.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Delete;
            }
            ApplicationBar = this.Resources["AppBarList"] as ApplicationBar;
            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = (POILLS.ItemsSource.Count > 0);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
                loadData();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back && POILLS.IsSelectionEnabled)
            {
                POILLS.IsSelectionEnabled = false;
                e.Cancel = true;
            }
        }

        private void POILLS_IsSelectionEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (POILLS.IsSelectionEnabled)
                ApplicationBar = this.Resources["AppBarListSelect"] as ApplicationBar;
            else
                ApplicationBar = this.Resources["AppBarList"] as ApplicationBar;

            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = !POILLS.IsSelectionEnabled;
        }

        private void POILLS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (POILLS.IsSelectionEnabled)
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = (POILLS.SelectedItems.Count > 0);
        }

        private void IconMultiSelect_Click(object sender, EventArgs e)
        {
            POILLS.IsSelectionEnabled = !POILLS.IsSelectionEnabled;
        }

        private void IconDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(AppResources.ConfirmDeletePOIs, "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var vm = DataContext as ListPOIViewModel;
                List<PointOfInterest> poiList = new List<PointOfInterest>();
                for (int i = 0; i < POILLS.SelectedItems.Count; i++)
                {
                    poiList.Add(POILLS.SelectedItems[i] as PointOfInterest);
                    vm.PointOfInterestList.Remove(POILLS.SelectedItems[i] as PointOfInterest);
                }

                if (vm != null)
                    vm.DeletePOIsCommand.Execute(poiList);

                POILLS.ItemsSource = vm.PointOfInterestList;

                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = (POILLS.ItemsSource.Count > 0);
            }
        }

        private void POILLS_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            //LongListMultiSelectorItem item = (sender as LongListMultiSelectorItem);
            //TextBlock textBlock = item.FindName("RowNumber") as TextBlock;
            //textBlock.Text = 
        }
    }
}