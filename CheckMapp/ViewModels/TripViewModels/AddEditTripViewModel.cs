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

        private double _departureLongitude;

   
        public double DepartureLongitude
        {
            get { return _departureLongitude; }
            set
            {
                if (_departureLongitude != value)
                {
                    _departureLongitude = value;
                    NotifyPropertyChanged("DepartureLongitude");
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
                    NotifyPropertyChanged("DepartureLatitude");
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
                    NotifyPropertyChanged("DestinationLongitude");
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
                    NotifyPropertyChanged("DestinationLatitude");
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

        public async void AddEditTrip()
        {
            // Adding a Trip
            if (Mode == Mode.add)
            {
                if (!string.IsNullOrWhiteSpace(Trip.Name) &&
                    !string.IsNullOrWhiteSpace(_departure) && !string.IsNullOrWhiteSpace(_destination))
                {
                   await SetCoordinate();
                   AddTripInDB(Trip);
                }
                else
                {
                    // Show an appropriate message
                }
            }
            else if (Mode == Mode.edit)
            {
                // Edit a Trip
                if (!string.IsNullOrWhiteSpace(Trip.Name) &&
                    !string.IsNullOrWhiteSpace(_departure) && !string.IsNullOrWhiteSpace(_destination))
                {
                    UpdateExistingTrip();
                }
                else
                {
                    // Show an appropriate message
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

            await SetCoordinateAsync(_departure, true);

            await SetCoordinateAsync(_destination, false);
           
        }

        public async Task SetCoordinateAsync(string searchString, bool isDeparture)
        {
            try
            {
                string _Key = @"ApLNskx1-wRcHef5fqjiu4wQADHER1tzQjJvNFkGc93ezOx3YK8HO6rMlScx74Lt";
                var _Helper = new MapHelper(_Key);

                var _Location = await _Helper.FindLocationByQueryAsync(searchString).ConfigureAwait(false);

                var _Address = _Location.First().address;
                var _Coordinate = _Location.First().point;

                //Ajout de la longitude et de la latitude
                if (isDeparture)
                {
                    Trip.DepartureLatitude = _Coordinate.coordinates[0];
                    Trip.DepartureLongitude = _Coordinate.coordinates[1];
                }
                else
                {
                    Trip.DestinationLatitude = _Coordinate.coordinates[0];
                    Trip.DestinationLongitude = _Coordinate.coordinates[1];
                }
            }
            catch (NullReferenceException e)
            {
                Console.Out.WriteLine(e);
            }           
           
        }

        public async Task<List<double>> getCoordinateAsync(string searchString)
        {
            List<double> myList = new List<double>();
            try
            {
                
                string _Key = @"ApLNskx1-wRcHef5fqjiu4wQADHER1tzQjJvNFkGc93ezOx3YK8HO6rMlScx74Lt";
                var _Helper = new MapHelper(_Key);

                var _Location = await _Helper.FindLocationByQueryAsync(searchString).ConfigureAwait(false);

                var _Address = _Location.First().address;
                var _Coordinate = _Location.First().point;


                myList.Add(_Coordinate.coordinates[0]);
                myList.Add(_Coordinate.coordinates[1]);
                return myList;
            }
            catch (MapHelper.InvalidStatusCodeException e)
            {
                Console.Out.WriteLine(e);
                throw e;
            }           
        }
        #endregion

    }
}