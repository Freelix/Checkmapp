using CheckMapp.Model.Tables;
using CheckMapp.Resources;
using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;
using CheckMapp.Model.DataService;
using System.Collections.ObjectModel;

namespace CheckMapp.ViewModels.TripViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CurrentViewModel : ViewModelBase
    {
        private Trip _trip;

        /// <summary>
        /// Initializes a new instance of the CurrentViewModel class.
        /// </summary>
        public CurrentViewModel(int trip)
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            Trip = dsTrip.getTripById(trip);
        }

        /// <summary>
        /// Le voyage en cours..
        /// </summary>
        public Trip Trip
        {
            get { return _trip; }
            set { _trip = value; }
        }

        /// <summary>
        /// Le nombre de jours écoulés
        /// </summary>
        public int ElapsedDays
        {
            get
            {
                if (Trip != null)
                {
                    TimeSpan elapsed = DateTime.Now.Subtract(Trip.BeginDate);
                    if (elapsed.TotalDays < 0)
                        return 0;
                    return (int)elapsed.TotalDays;
                }
                else
                    return 0;
            }
        }

        /// <summary>
        /// Nombre de notes
        /// </summary>
        public string CountNotes
        {
            get
            {
                if (Trip != null)
                    return String.Format(AppResources.CountNotes, Trip.Notes.Count);
                else
                    return "0";
            }
        }

        /// <summary>
        /// Nombres de points d'intérêts
        /// </summary>
        public string CountPOI
        {
            get 
            {
                if (Trip != null)
                {
                    DataServicePoi dsPoi = new DataServicePoi();
                    return String.Format(AppResources.CountPOI, Trip.PointsOfInterests.Count);
                }
                else
                    return "0";
            }
        }

        /// <summary>
        /// Nombre de photos
        /// </summary>
        public string CountPhotos
        {
            get
            {
                if (Trip != null)
                    return String.Format(AppResources.CountPhotos, Trip.Pictures.Count);
                else
                    return "0";
            }
        }


        
    }
}