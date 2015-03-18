using CheckMapp.Model.Tables;
using CheckMapp.Resources;
using GalaSoft.MvvmLight;
using System;
using System.Linq;

namespace CheckMapp.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class StatisticViewModel : ViewModelBase
    {
        private Trip _trip;

        /// <summary>
        /// Initializes a new instance of the StatisticViewModel class.
        /// </summary>
        public StatisticViewModel(Trip currentTrip)
        {
            _trip = currentTrip;
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

        public string TripName
        {
            get { return Trip.Name; }
        }

        public string TripBeginDate
        {
            get { return String.Format(AppResources.TripBeginDate, Trip.BeginDate.ToShortDateString()); }
        }

        public string TripEndDate
        {
            get
            {
                if (Trip.EndDate != null)
                {
                    return String.Format(AppResources.TripEndDate, Trip.EndDate.Value.ToShortDateString());
                }
                else
                    return null;
            }
        }

        public string TripNoteToday
        {
            get { return String.Format(AppResources.NoteToday, Trip.Notes.Where(x => x.Date.Date == DateTime.Today).Count()); }
        }

        public string TripNoteAllTime
        {
            get { return String.Format(AppResources.NoteAllTime, Trip.Notes.Count()); }
        }

        public string TripPictureToday
        {
            get { return String.Format(AppResources.PictureToday, Trip.Pictures.Where(x => x.Date.Date == DateTime.Today).Count()); }
        }

        public string TripPictureAllTime
        {
            get { return String.Format(AppResources.PictureAllTime, Trip.Pictures.Count()); }
        }

        #endregion

    }
}