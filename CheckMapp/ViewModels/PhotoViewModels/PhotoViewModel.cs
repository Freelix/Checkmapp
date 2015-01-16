using GalaSoft.MvvmLight;

namespace CheckMapp.ViewModels.PhotoViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PhotoViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the PhotoViewModel class.
        /// </summary>
        public PhotoViewModel()
        {

        }

        public string PhotoMessage
        {
            get { return "C'Est ca la photo `;a éétét pris la"; }
        }
    }
}