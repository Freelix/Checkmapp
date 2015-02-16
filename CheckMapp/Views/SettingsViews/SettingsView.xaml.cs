using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.Resources;
using CheckMapp.ViewModels.SettingsViewModels;

namespace CheckMapp.Views.SettingsViews
{
    public partial class SettingsView : PhoneApplicationPage
    {
        public SettingsView()
        {
            InitializeComponent();
            var vm = DataContext as SettingsViewModel;
        }

        #region Buttons

        private void IconSave_Click(object sender, EventArgs e)
        {
            this.Focus();
            var vm = DataContext as SettingsViewModel;

            // wait till the next UI thread tick so that the binding gets updated
            Dispatcher.BeginInvoke(() =>
            {
                if (vm != null)
                {
                    vm.EditSettingsCommand.Execute(null);
                }

                (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
            });
        }

        private void IconCancel_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Save;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Cancel;
            }
        }

        private void BtnClearHistory_Click(object sender, EventArgs e)
        {
            this.Focus();

            // wait till the next UI thread tick so that the binding gets updated
            Dispatcher.BeginInvoke(() =>
            {
                var vm = DataContext as SettingsViewModel;
                if (vm != null)
                {
                    vm.ClearHistoryCommand.Execute(null);
                }
            });
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            this.Focus();

            // wait till the next UI thread tick so that the binding gets updated
            Dispatcher.BeginInvoke(() =>
            {
                var vm = DataContext as SettingsViewModel;
                if (vm != null)
                {
                    vm.CheckUpdatesCommand.Execute(null);
                }
            });
        }

        private void BtnRecommendApp_Click(object sender, EventArgs e)
        {
            this.Focus();

            // wait till the next UI thread tick so that the binding gets updated
            Dispatcher.BeginInvoke(() =>
            {
                var vm = DataContext as SettingsViewModel;
                if (vm != null)
                {
                    vm.RecommendAppCommand.Execute(null);
                }
            });
        }

        #endregion

        #region ToggleSwitch

        private void WifiOnlySwitch_Checked(object sender, RoutedEventArgs e) 
        { var vm = DataContext as SettingsViewModel; vm.WifiOnly = true; }

        private void WifiOnlySwitch_Unchecked(object sender, RoutedEventArgs e) 
        { var vm = DataContext as SettingsViewModel; vm.WifiOnly = false; }

        private void AutoSyncSwitch_Checked(object sender, RoutedEventArgs e) 
        { var vm = DataContext as SettingsViewModel; vm.AutoSync = true; }

        private void AutoSyncSwitch_Unchecked(object sender, RoutedEventArgs e) 
        { var vm = DataContext as SettingsViewModel; vm.AutoSync = false; }

        #endregion

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            loadData();
        }

        private void loadData()
        {
            this.DataContext = new SettingsViewModel();
        }
    }
}