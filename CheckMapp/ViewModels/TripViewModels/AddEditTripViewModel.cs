﻿using CheckMapp.Model.DataService;
using CheckMapp.Model.Tables;
using CheckMapp.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls;
using System;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CheckMapp.Model.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Device.Location;
using Windows.Devices.Geolocation;
using FluentValidation;
using CheckMapp.Utils.Validations;

namespace CheckMapp.ViewModels.TripViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AddEditTripViewModel : ViewModelBase
    {
        private Trip _trip;

        // Used for validate the form
        private IValidator<Trip> _validator;

        /// <summary>
        /// Initializes a new instance of the AddTripViewModel class.
        /// </summary>
        public AddEditTripViewModel(Trip trip, Mode mode)
        {
            this.Mode = mode;

            if (this.Mode == Mode.add)
            {
                Trip = new Trip();
                Trip.BeginDate = DateTime.Now;
            }
            else
                Trip = trip;

            InitialiseValidator();
        }

        private void InitialiseValidator()
        {
            _validator = new ValidatorFactory().GetValidator<Trip>();
        }

        private ICommand _addEditTripCommand;
        /// <summary>
        /// Commande pour ajouter un voyage
        /// </summary>
        public ICommand AddEditTripCommand
        {
            get
            {
                if (_addEditTripCommand == null)
                {
                    _addEditTripCommand = new RelayCommand(() => AddEditTrip());
                }
                return _addEditTripCommand;
            }

        }

        #region Properties

        private bool _isFormValid;

        public bool IsFormValid
        {
            get { return _isFormValid; }
            set
            {
                _isFormValid = value;
            }
        }

        public Trip Trip
        {
            get { return _trip; }
            set
            {
                _trip = value;
            }
        }

        public Mode Mode
        {
            get;
            set;
        }

        public int TripId
        {
            get { return Trip.Id; }
            set
            {
                if (Trip.Id != value)
                {
                    Trip.Id = value;
                    RaisePropertyChanged("TripId");
                }
            }
        }

       
        public string TripName
        {
            get { return Trip.Name; }
            set
            {
                if (Trip.Name != value)
                {
                    Trip.Name = value;
                    RaisePropertyChanged("TripName");
                }
            }
        }

        public DateTime TripBeginDate
        {
            get { return Trip.BeginDate; }
            set
            {
                if (!Trip.BeginDate.Equals(value))
                {
                    Trip.BeginDate = value;
                    RaisePropertyChanged("TripBeginDate");
                   
                }
            }
        }

        public DateTime? TripEndDate
        {
            get { return Trip.EndDate; }
            set
            {
                if (!Trip.EndDate.Equals(value))
                {
                    Trip.EndDate = value;
                    RaisePropertyChanged("TripEndDate");
                }
            }
        }

        private string _departure;
        public string Departure
        {
            get { return _departure; }
            set
            {
                if (_departure != value)
                {
                    _departure = value;
                    RaisePropertyChanged("Departure");
                }
            }
        }


        private string _destination;
        public string Destination
        {
            get { return _destination; }
            set
            {
                if (_destination != value)
                {
                    _destination = value;
                    RaisePropertyChanged("Destination");
                }
            }
        }

        private double _departureLongitude;

   
        public double DepartureLongitude
        {
            get { return _departureLongitude; }
            set
            {
                if (_departureLongitude != value)
                {
                    _departureLongitude = value;
                    RaisePropertyChanged("DepartureLongitude");
                }
            }
        }

        private double _departureLatitude;

    
        public double DepartureLatitude
        {
            get { return _departureLatitude; }
            set
            {
                if (_departureLatitude != value)
                {
                    _departureLatitude = value;
                    RaisePropertyChanged("DepartureLatitude");
                }
            }
        }

        private double _destinationLongitude;

      
        public double DestinationLongitude
        {
            get { return _destinationLongitude; }
            set
            {
                if (_destinationLongitude != value)
                {
                    _destinationLongitude = value;
                    RaisePropertyChanged("DestinationLongitude");
                }
            }
        }

        private double _destinationLatitude;


        public double DestinationLatitude
        {
            get { return _destinationLatitude; }
            set
            {
                if (_destinationLatitude != value)
                {
                    _destinationLatitude = value;
                    RaisePropertyChanged("DestinationLatitude");
                }
            }
        }

        public byte[] MainImage
        {
            get { return Trip.MainPictureData; }
            set
            {
                Trip.MainPictureData = value;
                RaisePropertyChanged("MainImage");
            }
        }

        #endregion

        #region DBMethods

        public void AddEditTrip()
        {
            // If the form is not valid, a notification will appear
            if (ValidationErrorsHandler.IsValid(_validator, Trip))
            {
                _isFormValid = true;

                // Adding a Trip
                if (Mode == Mode.add)
                {   
                   AddTripInDB(Trip);
                }
                else if (Mode == Mode.edit)
                {
                    UpdateExistingTrip();
                }
            }
        }

        public void AddTripInDB(Trip trip)
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            dsTrip.addTrip(trip);
        }

        public void UpdateExistingTrip()
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            dsTrip.UpdateTrip(Trip);
        }

        #endregion

    }
}