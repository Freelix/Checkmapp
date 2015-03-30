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
        private Trip _trip;

        /// <summary>
        /// Initializes a new instance of the TripViewModel class.
        /// </summary>
        public TripViewModel(Trip trip)
        {
            this.Trip = trip;
        }

        public Trip Trip
        {
            get { return _trip; }
            set
            {
                _trip = value;
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
                    DataServicePoi dsPoi = new DataServicePoi();
                    return String.Format(AppResources.POITripTitle, dsPoi.LoadPointOfInterestsFromTrip(Trip).Count);
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


        #endregion

        #region DBMethods

        public void DeleteTrip()
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            dsTrip.DeleteTrip(Trip);
        }


        #endregion

    }
}