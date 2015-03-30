using CheckMapp.Model.DataService;
using CheckMapp.Model.Tables;
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
        public SelectEndDateViewModel(Trip trip)
        {
            this.Trip = trip;
            Date = DateTime.Now.Date;
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
        public DateTime Date
        {
            get
            {
                return (DateTime)Trip.EndDate;
            }
            set
            {
                if (Trip.EndDate != value)
                {
                    Trip.EndDate = value.Date;
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