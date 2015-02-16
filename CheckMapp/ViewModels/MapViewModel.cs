using CheckMapp.Model.DataService;
using CheckMapp.Model.Tables;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;

namespace CheckMapp.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MapViewModel : ViewModelBase
    {
        private ObservableCollection<Trip> tripPoints;

        /// <summary>
        /// Initializes a new instance of the MapViewModel class.
        /// </summary>
        public MapViewModel()
        {
            tripPoints = new ObservableCollection<Trip>();
            LoadAllCoordinateFromDatabase();
        }

        public ObservableCollection<Trip> TripPoints
        {
            get { return tripPoints; }
            set 
            { 
                tripPoints = value;
                RaisePropertyChanged("TripPoints");
            }
        }

        public void LoadAllCoordinateFromDatabase()
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            List<Trip> allTripInDB = dsTrip.LoadTrip();
            TripPoints = new ObservableCollection<Trip>(allTripInDB);
        }
    }
}