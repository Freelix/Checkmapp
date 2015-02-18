using CheckMapp.Model.DataService;
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
                    NotifyPropertyChanged("TripId");
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
                    NotifyPropertyChanged("TripName");
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
                    NotifyPropertyChanged("TripBeginDate");
                   
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
                    NotifyPropertyChanged("TripEndDate");
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
                    NotifyPropertyChanged("Departure");
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
                    NotifyPropertyChanged("Destination");
                }
            }
        }

        public byte[] MainImage
        {
            get { return Trip.MainPictureData; }
            set
            {
                Trip.MainPictureData = value;
                NotifyPropertyChanged("MainImage");
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

        public void AddEditTrip()
        {
            // If the form is not valid, a notification will appear
            if (ValidationErrorsHandler.IsValid(_validator, Trip))
            {
                _isFormValid = true;

                // Adding a Trip
                if (Mode == Mode.add)
                {
                    //  await SetCoordinate();
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

        #region MapHelperMethod

        //Set latitude and longitude from query to _TripDeparture and _TripDestination
        private async Task SetCoordinate()
        {

          /*  await SetCoordinateAsync(_departure, Trip.Departure);
            Trip.Departure.SetPosition(TripLocalisation.Position.Departure);

            await SetCoordinateAsync(_destination, Trip.Destination);
            Trip.Destination.SetPosition(TripLocalisation.Position.Destination);*/
        }

        private async Task SetCoordinateAsync(string searchString)
        {
            string _Key = @"ApLNskx1-wRcHef5fqjiu4wQADHER1tzQjJvNFkGc93ezOx3YK8HO6rMlScx74Lt";
            var _Helper = new MapHelper(_Key);

            var _Location = await _Helper.FindLocationByQueryAsync(searchString).ConfigureAwait(false);

            var _Address = _Location.First().address;
            var _Coordinate = _Location.First().point;

            //Ajout de la longitude et de la latitude
           // tripLocalisation.Latitude =_Coordinate.coordinates[0];
           // tripLocalisation.Longitude = _Coordinate.coordinates[1];
        }
        #endregion

    }
}