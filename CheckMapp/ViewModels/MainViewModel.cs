using CheckMapp.Model.DataService;
using CheckMapp.Model.Tables;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace CheckMapp.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ICommand _ShowUserControlTimelineCommand;
        private ICommand _ShowUserControlTripCommand;

        private ViewModelBase _currentPageViewModel;

        private bool _isList;
        private bool _isTimeline;

        public List<Trip> TripList {get;set;}
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            LoadArchivesTripFromDatabase();
            PageViewModels = new List<ViewModelBase>();
            // Add available pages
            PageViewModels.Add(new CheckMapp.ViewModels.ArchivesViewModels.ArchivesViewModel(TripList.FindAll(x=>!x.IsActif)));
            PageViewModels.Add(new CheckMapp.ViewModels.ArchivesViewModels.TimelineViewModel(TripList.FindAll(x=>!x.IsActif)));
            // Set starting page
            ShowUserControlTrip();
        }

        public void LoadArchivesTripFromDatabase()
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            TripList =  dsTrip.LoadTrip();
        }

        /// <summary>
        /// Si c'est affiché en tableau
        /// </summary>
        public bool IsList
        {
            get { return _isList; }
            set
            {
                _isList = value;
                RaisePropertyChanged("IsList");
            }
        }

        /// <summary>
        /// Si c'est affiché en ligne de temps
        /// </summary>
        public bool IsTimeline
        {
            get { return _isTimeline; }
            set
            {
                _isTimeline = value;
                RaisePropertyChanged("IsTimeline");
            }
        }

        public ICommand ShowUserControlTripCommand
        {
            get
            {
                if (_ShowUserControlTripCommand == null)
                {
                    _ShowUserControlTripCommand = new RelayCommand(() => ShowUserControlTrip());
                }
                return _ShowUserControlTripCommand;
            }
        }

        public ICommand ShowUserControlTimelineCommand
        {
            get
            {
                if (_ShowUserControlTimelineCommand == null)
                {
                    _ShowUserControlTimelineCommand = new RelayCommand(() => ShowUserControlTimeline());
                }
                return _ShowUserControlTimelineCommand;
            }
        }

        /// <summary>
        /// Afficher en tableau
        /// </summary>
        public void ShowUserControlTrip()
        {
            CurrentPageViewModel = PageViewModels[0] as CheckMapp.ViewModels.ArchivesViewModels.ArchivesViewModel;
            IsTimeline = false;
            IsList = true;
        }

        /// <summary>
        /// Afficher en ligne de temps
        /// </summary>
        public void ShowUserControlTimeline()
        {
            CurrentPageViewModel = PageViewModels[1] as CheckMapp.ViewModels.ArchivesViewModels.TimelineViewModel;
            IsTimeline = true;
            IsList = false;
        }


        public static List<ViewModelBase> PageViewModels
        {
            get;
            set;
        }

        public ViewModelBase CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    RaisePropertyChanged("CurrentPageViewModel");
                }
            }
        }




    }
}