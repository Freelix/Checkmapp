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
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Live;
using Utility = CheckMapp.Utils.Utility;

namespace CheckMapp.Views.SettingsViews
{
    public partial class SettingsView : PhoneApplicationPage
    {
        CancellationTokenSource cts;
        public SettingsView()
        {
            InitializeComponent();
            var vm = DataContext as SettingsViewModel;
        }

        #region Buttons


        private async void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (Utility.checkNetworkConnection() == false)
            {
                MessageBox.Show(AppResources.InternetConnectionSettings, AppResources.NotConnected, MessageBoxButton.OK);
                return;
            }
            if (MessageBox.Show(AppResources.ConfirmationImport, AppResources.Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var vm = DataContext as SettingsViewModel;
                vm.ImportCommand.Execute(null);

                cts = new CancellationTokenSource();
                Progress<LiveOperationProgress> uploadProgress = new Progress<LiveOperationProgress>(
        (p) =>
        {
            vm.ProgressPercent = p.ProgressPercentage;

        });
                int import = await Utility.ImportBD(cts.Token, uploadProgress);
                vm.CancelCommand.Execute(null);
                if (import == 0)
                {
                    MessageBox.Show(AppResources.FileNotFound, AppResources.Warning, MessageBoxButton.OK);
                    return;
                }
                else
                {
                    MessageBox.Show(AppResources.RestartApp, AppResources.Warning, MessageBoxButton.OK);
                    IsolatedStorageSettings.ApplicationSettings["ReplaceDB"] = true;
                    IsolatedStorageSettings.ApplicationSettings.Save();
                    Application.Current.Terminate();
                }
            }
        }

        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (Utility.checkNetworkConnection() == false)
            {
                MessageBox.Show(AppResources.InternetConnectionSettings, AppResources.NotConnected, MessageBoxButton.OK);
                return;
            }
            var vm = DataContext as SettingsViewModel;
            vm.ExportCommand.Execute(null);
            cts = new CancellationTokenSource();
            Progress<LiveOperationProgress> uploadProgress = new Progress<LiveOperationProgress>(
        (p) =>
        {
            vm.ProgressPercent = p.ProgressPercentage;

        });
            int export = await Utility.ExportDB(cts.Token, uploadProgress);
            vm.CancelCommand.Execute(null);
        }


        private void BtnRateApp_Click(object sender, EventArgs e)
        {
            this.Focus();
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void BtnWebsite_Click(object sender, EventArgs e)
        {
            // TODO: Change the URL with our real website
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri("https://www.google.ca/");
            webBrowserTask.Show();
        }

        #endregion

        #region ToggleSwitch

        private void WifiOnlySwitch_Checked(object sender, RoutedEventArgs e)
        { var vm = DataContext as SettingsViewModel; vm.WifiOnly = true; }

        private void WifiOnlySwitch_Unchecked(object sender, RoutedEventArgs e)
        { var vm = DataContext as SettingsViewModel; vm.WifiOnly = false; }

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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
            }
            var vm = DataContext as SettingsViewModel;
            vm.CancelCommand.Execute(null);
        }
    }
}