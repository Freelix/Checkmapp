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
        List<double> MyCoordinate = new List<double>();

        /// <summary>
        /// Initializes a new instance of the AddTripViewModel class.
        /// </summary>
        public AddEditTripViewModel(Mode mode)
        {
            this.Mode = mode;
        }

        public void showInfo(Trip TripToModify)
        {
            TripId = TripToModify.Id;
            TripName = TripToModify.Name;
            TripBeginDate = TripToModify.BeginDate;
            TripEndDate = TripToModify.EndDate;
            //TripDeparture = TripToModify.Departure;
            //TripDestination = TripToModify.Destination;
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

        public Mode Mode
        {
            get;
            set;
        }

        private int _id;
        public int TripId
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged("TripId");
                }
            }
        }

        private string _TripName;
        public string TripName
        {
            get { return _TripName; }
            set
            {
                if (_TripName != value)
                {
                    _TripName = value;
                    NotifyPropertyChanged("TripName");
                }
            }
        }

        private DateTime _TripBeginDate = DateTime.Now;
        public DateTime TripBeginDate
        {
            get { return _TripBeginDate; }
            set
            {
                if (!_TripBeginDate.Equals(value))
                {
                    _TripBeginDate = value;
                    NotifyPropertyChanged("TripBeginDate");
                   
                }
            }
        }

        private DateTime? _TripEndDate;
        public DateTime? TripEndDate
        {
            get { return _TripEndDate; }
            set
            {
                if (!_TripEndDate.Equals(value))
                {
                    _TripEndDate = value;
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


        private TripLocalisation _TripDeparture = new TripLocalisation();
        public TripLocalisation TripDeparture
        {
            get { return _TripDeparture; }
            set
            {
                if (_TripDeparture != value)
                {
                    _TripDeparture = value;
                }
            }
        }

        private TripLocalisation _TripDestination = new TripLocalisation();
        public TripLocalisation TripDestination
        {
            get { return _TripDestination; }
            set
            {
                if (_TripDestination != value)
                {
                    _TripDestination = value;
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
            // Adding a note
            if (Mode == Mode.add)
            {
                if (!string.IsNullOrWhiteSpace(_TripName) &&
                    !string.IsNullOrWhiteSpace(_departure) && !string.IsNullOrWhiteSpace(_destination))
                {
                       
                   await SetCoordinate();

                   Trip newTrip = new Trip
                    {
                        BeginDate = _TripBeginDate,
                        EndDate = _TripEndDate,
                      //  Departure = _TripDeparture,
                       // Destination = _TripDestination,
                        Name = _TripName
                    };

                    AddTripInDB(newTrip);

                    (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
                }
                else
                {
                    // Show an appropriate message
                }
            }
            else if (Mode == Mode.edit)
            {
                // Edit a note
                if (!string.IsNullOrWhiteSpace(_TripName))
                {
                    UpdateExistingTrip();

                    (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
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

            Trip updatedTrip = new Trip();
            updatedTrip.Id = _id;
            updatedTrip.Name = _TripName;
            updatedTrip.BeginDate = _TripBeginDate;
            updatedTrip.EndDate = _TripEndDate;
            //updatedTrip.Departure = _TripDeparture;
            //updatedTrip.Destination = _TripDestination;
            dsTrip.UpdateTrip(updatedTrip);
        }

        #endregion

        #region MapHelperMethod

        //Set latitude and longitude from query to _TripDeparture and _TripDestination
        private async Task SetCoordinate()
        {

            await SetCoordinateAsync(_departure, _TripDeparture);
            _TripDeparture.SetPosition(TripLocalisation.Position.Departure);

            await SetCoordinateAsync(_destination, _TripDestination);
            _TripDestination.SetPosition(TripLocalisation.Position.Destination);
        }

        private async Task SetCoordinateAsync(string searchString, TripLocalisation tripLocalisation)
        {
            string _Key = @"ApLNskx1-wRcHef5fqjiu4wQADHER1tzQjJvNFkGc93ezOx3YK8HO6rMlScx74Lt";
            var _Helper = new MapHelper(_Key);

            var _Location = await _Helper.FindLocationByQueryAsync(searchString).ConfigureAwait(false);

            var _Address = _Location.First().address;
            var _Coordinate = _Location.First().point;

            //Insertion de la latitude et de la longitude
            tripLocalisation.Latitude =_Coordinate.coordinates[0];
            tripLocalisation.Longitude = _Coordinate.coordinates[1];
        }
        #endregion

    }
}



/*using CheckMapp.Model.DataService;
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
        public AddEditTripViewModel(Mode mode)
        {
            this.Mode = mode;
        }

        public void showInfo(Trip TripToModify)
        {
            TripId = TripToModify.Id;
            TripName = TripToModify.Name;
            TripBeginDate = TripToModify.BeginDate;
            TripEndDate = TripToModify.EndDate;
            TripDeparture = TripToModify.Departure;
            TripDestination = TripToModify.Destination;
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


        
        public TripLocalisation TripDeparture
        {
            get { return Trip.Departure; }
            set
            {
                if (Trip.Departure != value)
                {
                    Trip.Departure = value;
                }
            }
        }

        public TripLocalisation TripDestination
        {
            get { return Trip.Destination; }
            set
            {
                if (Trip.Destination != value)
                {
                    Trip.Destination = value;
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
            // Adding a note
            if (Mode == Mode.add)
            {
                if (!string.IsNullOrWhiteSpace(Trip.Name) &&
                    !string.IsNullOrWhiteSpace(_departure) && !string.IsNullOrWhiteSpace(_destination))
                {
                   await SetCoordinate();
                   AddTripInDB(Trip);
                    (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
                }
                else
                {
                    // Show an appropriate message
                }
            }
            else if (Mode == Mode.edit)
            {
                // Edit a note
                if (!string.IsNullOrWhiteSpace(Trip.Name) &&
                    !string.IsNullOrWhiteSpace(_departure) && !string.IsNullOrWhiteSpace(_destination))
                {
                    UpdateExistingTrip();

                    (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
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

            await SetCoordinateAsync(_departure, Trip.Departure);
            Trip.Departure.SetPosition(TripLocalisation.Position.Departure);

            await SetCoordinateAsync(_destination, Trip.Destination);
            Trip.Destination.SetPosition(TripLocalisation.Position.Destination);
        }

        private async Task SetCoordinateAsync(string searchString, TripLocalisation tripLocalisation)
        {
            string _Key = @"ApLNskx1-wRcHef5fqjiu4wQADHER1tzQjJvNFkGc93ezOx3YK8HO6rMlScx74Lt";
            var _Helper = new MapHelper(_Key);

            var _Location = await _Helper.FindLocationByQueryAsync(searchString).ConfigureAwait(false);

            var _Address = _Location.First().address;
            var _Coordinate = _Location.First().point;

            tripLocalisation.Latitude =_Coordinate.coordinates[0];
            tripLocalisation.Longitude = _Coordinate.coordinates[1];
        }
        #endregion

    }
}*/