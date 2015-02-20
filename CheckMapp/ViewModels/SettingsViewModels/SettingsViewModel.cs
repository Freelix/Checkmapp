using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using CheckMapp.Utils.Settings;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CheckMapp.Utils.Languages;
using Utility = CheckMapp.Utils.Utility;

namespace CheckMapp.ViewModels.SettingsViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private CMIsolatedStorageProperty<bool> WifiOnlyStorageProperty;
        private CMIsolatedStorageProperty<bool> AutoSyncStorageProperty;
        private CMIsolatedStorageProperty<string> LanguageStorageProperty;

        private List<string> _languagesCode;

        public SettingsViewModel()
        {
            _languagesCode = new List<string>();
            loadData();
        }

        private void loadData()
        {
            _appVersion = Utility.GetAppVersion();
            _languagesList = LocalizationManager.GetAllLanguages();
            _languagesCode = LocalizationManager.GetAllLanguagesCode();
            loadDataFromIsolatedStorage();
        }

        private void loadDataFromIsolatedStorage()
        {
            WifiOnlyStorageProperty = CMSettingsContainer.WifiOnly;
            _wifiOnly = WifiOnlyStorageProperty.Value;

            AutoSyncStorageProperty = CMSettingsContainer.AutoSync;
            _autoSync = AutoSyncStorageProperty.Value;

            LanguageStorageProperty = CMSettingsContainer.Language;

            int index;

            if (LanguageStorageProperty.Value != null)
                index = Enumerable.Range(0, _languagesCode.Count)
                    .Where(x => _languagesCode[x] == LanguageStorageProperty.Value).FirstOrDefault();
            else
            {
                string currentLang = LocalizationManager.GetCurrentAppLang();

                index = Enumerable.Range(0, _languagesCode.Count)
                    .Where(x => _languagesCode[x] == currentLang).FirstOrDefault();

                LanguageStorageProperty.Value = currentLang;
            }

            if (index > -1)
                    LanguageIndex = index;          
        }

        #region Properties

        private string _appVersion;

        public string AppVersion
        {
            get { return _appVersion; }
            set
            {
                _appVersion = value;
                RaisePropertyChanged("AppVersion");
            }
        }

        private bool _wifiOnly;

        public bool WifiOnly
        {
            get { return _wifiOnly; }
            set
            {
                _wifiOnly = value;
                RaisePropertyChanged("WifiOnly");
            }
        }

        private bool _autoSync;

        public bool AutoSync
        {
            get { return _autoSync; }
            set
            {
                _autoSync = value;
                RaisePropertyChanged("AutoSync");
            }
        }

        private int _languageIndex;

        public int LanguageIndex
        {
            get { return _languageIndex; }
            set
            {
                _languageIndex = value;
                RaisePropertyChanged("LanguageIndex");
            }
        }

        private List<string> _languagesList;

        public List<string> LanguagesList
        {
            get { return _languagesList; }
            set
            {
                _languagesList = value;
                RaisePropertyChanged("LanguagesList");
            }
        }

        #endregion

        #region Buttons Command

        private ICommand _editSettingsCommand;
        public ICommand EditSettingsCommand
        {
            get
            {
                if (_editSettingsCommand == null)
                {
                    _editSettingsCommand = new RelayCommand(() => EditSettings());
                }
                return _editSettingsCommand;
            }

        }

        private ICommand _clearHistoryCommand;
        public ICommand ClearHistoryCommand
        {
            get
            {
                if (_clearHistoryCommand == null)
                {
                    _clearHistoryCommand = new RelayCommand(() => ClearHistory());
                }
                return _clearHistoryCommand;
            }

        }

        private ICommand _checkUpdatesCommand;
        public ICommand CheckUpdatesCommand
        {
            get
            {
                if (_checkUpdatesCommand == null)
                {
                    _checkUpdatesCommand = new RelayCommand(() => CheckUpdates());
                }
                return _checkUpdatesCommand;
            }
        }

        private ICommand _recommendAppCommand;
        public ICommand RecommendAppCommand
        {
            get
            {
                if (_recommendAppCommand == null)
                {
                    _recommendAppCommand = new RelayCommand(() => RecommendApp());
                }
                return _recommendAppCommand;
            }

        }

        #endregion


        #region Storage Methods

        private void EditSettings()
        {
            WifiOnlyStorageProperty.Value = _wifiOnly;
            AutoSyncStorageProperty.Value = _autoSync;
            UpdateLanguage();
        }

        /// <summary>
        /// Update the Language if it was not the same
        /// </summary>
        private void UpdateLanguage()
        {
            string newLang = _languagesCode[_languageIndex];

            if (!LanguageStorageProperty.Value.Equals(newLang))
                LocalizationManager.ChangeAppLanguage(newLang);
        }

        #endregion

        #region Buttons Methods

        private void ClearHistory()
        {

        }

        private void CheckUpdates()
        {

        }

        private void RecommendApp()
        {

        }

        #endregion
    }
}
