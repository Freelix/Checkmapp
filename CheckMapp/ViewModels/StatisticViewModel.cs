using GalaSoft.MvvmLight;

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
        /// <summary>
        /// Initializes a new instance of the StatisticViewModel class.
        /// </summary>
        public StatisticViewModel()
        {
        }

        public string TripName
        {
            get { return "Africa 2014"; }
        }
    }
}