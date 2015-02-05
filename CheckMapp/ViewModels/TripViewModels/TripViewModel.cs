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
        public TripViewModel(int id)
        {
           // LoadTripFromDatabase(id);
            Trip trip = new Trip();
            trip.Name = "Belgique - Suisse 2012";
            trip.BeginDate = DateTime.Now.AddDays(-20);
            trip.EndDate = DateTime.Now.AddDays(-12);
            CurrentTrip = trip;
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

        public string FormatDate
        {
            get
            {
                return CurrentTrip.BeginDate.ToShortDateString() + " - " + CurrentTrip.EndDate.Value.ToShortDateString();
            }
        }

        /// <summary>
        /// Le titre des notes dans le voyage
        /// </summary>
        public string NoteTitle
        {
            get { return String.Format(AppResources.NoteTripTitle, 2); }
        }

        /// <summary>
        /// Le titre des photos dans le voyage
        /// </summary>
        public string PhotoTitle
        {
            get { return String.Format(AppResources.PhotoTripTitle, 6); }
        }

        /// <summary>
        /// Le titre des points d'intérets dans le voyage
        /// </summary>
        public string POITitle
        {
            get { return String.Format(AppResources.POITripTitle, 40); }
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

        public void LoadTripFromDatabase(int id)
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            _currentTrip = dsTrip.getTripById(id);
        }

        public void DeleteTrip()
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            dsTrip.DeleteTrip(CurrentTrip);
        }

        #endregion

    }
}