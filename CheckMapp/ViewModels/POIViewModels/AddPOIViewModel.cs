using GalaSoft.MvvmLight;

namespace CheckMapp.ViewModels.POIViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AddPOIViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the AddPOIViewModel class.
        /// </summary>
        public AddPOIViewModel()
        {
        }

        public string TripName
        {
            get { return "Africa 2014"; }
        }
    }
}