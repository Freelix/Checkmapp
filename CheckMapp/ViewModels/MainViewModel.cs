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
        private List<ViewModelBase> _pageViewModels;

        private bool _isList;

        private bool _isTimeline;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            // Add available pages
            PageViewModels.Add(new CheckMapp.ViewModels.ArchivesViewModels.ArchivesViewModel());
            PageViewModels.Add(new CheckMapp.ViewModels.ArchivesViewModels.TimelineViewModel());
            // Set starting page
            ShowUserControlTrip();
        }


        public bool IsList
        {
            get { return _isList; }
            set
            {
                _isList = value;
                RaisePropertyChanged("IsList");
            }
        }


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

        public void ShowUserControlTrip()
        {
            CurrentPageViewModel = PageViewModels[0] as CheckMapp.ViewModels.ArchivesViewModels.ArchivesViewModel;
            IsTimeline = false;
            IsList = true;
        }

        public void ShowUserControlTimeline()
        {
            CurrentPageViewModel = PageViewModels[1] as CheckMapp.ViewModels.ArchivesViewModels.TimelineViewModel;
            IsTimeline = true;
            IsList = false;
        }


        public List<ViewModelBase> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<ViewModelBase>();

                return _pageViewModels;
            }
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