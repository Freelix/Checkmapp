using CheckMapp.Model.Tables;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CheckMapp.ViewModels.ArchivesViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TimelineViewModel : ViewModelBase
    {

        /// <summary>
        /// Collection de voyage archives
        /// </summary>
        public ObservableCollection<Trip> ArchiveTripList { get; private set; }
        /// <summary>
        /// Initializes a new instance of the TimelineViewModel class.
        /// </summary>
        public TimelineViewModel(List<Trip> trip)
        {
            ArchiveTripList = new ObservableCollection<Trip>(trip);
        }
    }
}