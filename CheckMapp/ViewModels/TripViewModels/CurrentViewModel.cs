using CheckMapp.Model;
using CheckMapp.Resources;
using GalaSoft.MvvmLight;
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
    public class CurrentViewModel : ViewModelBase
    {
        private ICommand _showCurrentTripCommand;
        private Trip _currentTrip;

        /// <summary>
        /// Initializes a new instance of the CurrentViewModel class.
        /// </summary>
        public CurrentViewModel()
        {
            Trip trip = new Trip();
            trip.Name = "Australie en famille 2014";
            trip.BeginDate = DateTime.Now.AddDays(-20);
            trip.EndDate = DateTime.Now.AddDays(-12);
            CurrentTrip = trip;
        }

        /// <summary>
        /// Le voyage en cours..
        /// </summary>
        public Trip CurrentTrip
        {
            get { return _currentTrip; }
            set { _currentTrip = value; }
        }

        public int ElapsedDays
        {
            get
            {
                TimeSpan elapsed = DateTime.Now.Subtract(CurrentTrip.BeginDate);
                return (int)elapsed.TotalDays;
            }
        }

        /// <summary>
        /// Nombre de notes
        /// </summary>
        public string CountNotes
        {
            get { return String.Format(AppResources.CountNotes,43); }
        }

        /// <summary>
        /// Nombres de points d'intérêts
        /// </summary>
        public string CountPOI
        {
            get { return String.Format(AppResources.CountPOI, 3); }
        }

        /// <summary>
        /// Nombre de photos
        /// </summary>
        public string CountPhotos
        {
            get { return String.Format(AppResources.CountPhotos, 40); }
        }


        
    }
}