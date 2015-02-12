using CheckMapp.Model;
using CheckMapp.Resources;
using GalaSoft.MvvmLight;
using System;
using CheckMapp.Model.Tables;
using CheckMapp.Model.DataService;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.ComponentModel;

namespace CheckMapp.ViewModels.TripViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TripViewModel : ViewModelBase
    {
        private Trip _currentTrip;

        /// <summary>
        /// Initializes a new instance of the TripViewModel class.
        /// </summary>
        public TripViewModel(Trip trip)
        {
            this.CurrentTrip = trip;
        }

        public Trip CurrentTrip
        {
            get { return _currentTrip; }
            set
            {
                _currentTrip = value;
                NotifyPropertyChanged("CurrentTrip");
            }
        }

        /// <summary>
        /// Le titre des notes dans le voyage
        /// </summary>
        public string NoteTitle
        {
            get
            {
                if (CurrentTrip.Notes != null)
                {
                    return String.Format(AppResources.NoteTripTitle, CurrentTrip.Notes.Count);
                }
                else
                {
                    return String.Format(AppResources.NoteTripTitle, 0);
                }
            }
        }

        /// <summary>
        /// Le titre des photos dans le voyage
        /// </summary>
        public string PhotoTitle
        {
            get
            {
                if (CurrentTrip.Pictures != null)
                {
                    return String.Format(AppResources.PhotoTripTitle, CurrentTrip.Pictures.Count);
                }
                else
                {
                    return String.Format(AppResources.PhotoTripTitle, 0);
                }
            }
        }

        /// <summary>
        /// Le titre des points d'intérets dans le voyage
        /// </summary>
        public string POITitle
        {
            get
            {
                if (CurrentTrip.PointsOfInterests != null)
                {
                    return String.Format(AppResources.POITripTitle, CurrentTrip.PointsOfInterests.Count);
                }
                else
                {
                    return String.Format(AppResources.POITripTitle, 0);
                }
            }
        }

        #region Buttons

        private ICommand _deleteTripCommand;
        public ICommand DeleteTripCommand
        {
            get
            {
                if (_deleteTripCommand == null)
                {
                    _deleteTripCommand = new RelayCommand(() => DeleteTrip());
                }
                return _deleteTripCommand;
            }

        }

        private ICommand _finishTripCommand;
        public ICommand FinishTripCommand
        {
            get
            {
                if (_finishTripCommand == null)
                {
                    _finishTripCommand = new RelayCommand(() => FinishTrip());
                }
                return _finishTripCommand;
            }

        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region DBMethods

        public void DeleteTrip()
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            dsTrip.DeleteTrip(CurrentTrip);
        }

        public void FinishTrip()
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            CurrentTrip.EndDate = DateTime.Now;
            dsTrip.UpdateTrip(CurrentTrip);
        }

        #endregion

    }
}