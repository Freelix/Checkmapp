﻿using CheckMapp.Model;
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
            this.Trip = trip;
        }

        public Trip Trip
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
                if (Trip.Notes != null)
                {
                    return String.Format(AppResources.NoteTripTitle, Trip.Notes.Count);
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
                if (Trip.Pictures != null)
                {
                    return String.Format(AppResources.PhotoTripTitle, Trip.Pictures.Count);
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
                if (Trip.PointsOfInterests != null)
                {
                    return String.Format(AppResources.POITripTitle, Trip.PointsOfInterests.Count);
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
            dsTrip.DeleteTrip(Trip);
        }

        public void FinishTrip()
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            Trip.EndDate = DateTime.Now;
            dsTrip.UpdateTrip(Trip);
        }

        #endregion

    }
}