﻿using CheckMapp.Model.DataService;
using CheckMapp.Model.Tables;
using CheckMapp.Utils.EditableObject;
using CheckMapp.Utils.Validations;
using FluentValidation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace CheckMapp.ViewModels.TripViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SelectEndDateViewModel : ViewModelBase
    {
        private IValidator<Trip> _validator;
        private Trip _trip;
        /// <summary>
        /// Initializes a new instance of the SelectEndDateViewModel class.
        /// </summary>
        public SelectEndDateViewModel(int trip)
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            this.Trip = dsTrip.getTripById(trip);
            Date = DateTime.Now;
            InitialiseValidator();
        }

        #region properties
        public Trip Trip
        {
            get { return _trip; }
            set
            {
                _trip = value;
            }
        }

        /// <summary>
        /// Mon objet editable, nécessaire pour annuler les changements
        /// </summary>

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

        private ICommand _cancelSelectDateCommand;
        public ICommand CancelSelectDateCommand
        {
            get
            {
                if (_cancelSelectDateCommand == null)
                {
                    _cancelSelectDateCommand = new RelayCommand(() => CancelSelectDate());
                }
                return _cancelSelectDateCommand;
            }
        }

        private bool _isFormValid;
        public bool IsFormValid
        {
            get { return _isFormValid; }
            set
            {
                _isFormValid = value;
            }
        }

        /// <summary>
        /// Date de fin
        /// </summary>
        public DateTime? Date
        {
            get
            {
                return Trip.EndDate.GetValueOrDefault();
            }
            set
            {
                if (Trip.EndDate != value)
                {
                    if(value.HasValue)
                        Trip.EndDate = value.Value.Date;
                    else
                        Trip.EndDate = null;
                    RaisePropertyChanged("Date");
                }
            }
        } 
        #endregion

        #region methods
        private void InitialiseValidator()
        {
            _validator = new ValidatorFactory().GetValidator<Trip>();
        }

        public void CancelSelectDate()
        {
            Date = null;
        }

        public void FinishTrip()
        {
            if (ValidationErrorsHandler.IsValid(_validator, Trip))
            {
                _isFormValid = true;
                DataServiceTrip dsTrip = new DataServiceTrip();
                dsTrip.UpdateTrip(Trip);

            }
        } 
        #endregion
    }
}