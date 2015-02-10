using CheckMapp.Model.Tables;
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
        private Trip _currentTrip;

        /// <summary>
        /// Initializes a new instance of the CurrentViewModel class.
        /// </summary>
        public CurrentViewModel(Trip trip)
        {
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
                if (CurrentTrip != null)
                {
                    TimeSpan elapsed = DateTime.Now.Subtract(CurrentTrip.BeginDate);
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
                if (CurrentTrip != null)
                    return String.Format(AppResources.CountNotes, CurrentTrip.Notes.Count);
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
               /* if (CurrentTrip != null)
                    return String.Format(AppResources.CountPOI, CurrentTrip.PointsOfInterests.Count);
                else*/
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
                if (CurrentTrip != null)
                    return String.Format(AppResources.CountPhotos, CurrentTrip.Pictures.Count);
                else
                    return "0";
            }
        }


        
    }
}