using GalaSoft.MvvmLight;

namespace CheckMapp.ViewModels.PhotoViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ListPhotoViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the ListPhotoViewModel class.
        /// </summary>
        public ListPhotoViewModel()
        {
        }

        public string TripName
        {
            get { return "Africa 2014"; }
        }
    }
}