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
using System.IO.IsolatedStorage;
using Microsoft.Phone.Tasks;

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


        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(AppResources.ConfirmationImport, AppResources.Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Utils.Utility.ImportBD();

                MessageBox.Show(AppResources.RestartApp, AppResources.Warning, MessageBoxButton.OK);
                IsolatedStorageSettings.ApplicationSettings["ReplaceDB"] = true;
                IsolatedStorageSettings.ApplicationSettings.Save();
                Application.Current.Terminate();
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            Utils.Utility.ExportDB();
        }


        private void BtnRateApp_Click(object sender, EventArgs e)
        {
            this.Focus();
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
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