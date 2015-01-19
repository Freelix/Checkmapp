using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace CheckMapp.ViewModels.PhotoViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AddEditPhotoViewModel : ViewModelBase
    {
        private ICommand _addPhotoCommand;
        /// <summary>
        /// Initializes a new instance of the AddEditPhotoViewModel class.
        /// </summary>
        public AddEditPhotoViewModel(Mode mode)
        {
            this.Mode = mode;
        }

        public Mode Mode
        {
            get;
            set;
        }

        public ICommand AddPhotoCommand
        {
            get
            {
                if (_addPhotoCommand == null)
                {
                    _addPhotoCommand = new RelayCommand(() => AddPhoto());
                }
                return _addPhotoCommand;
            }

        }

        public void AddPhoto()
        {
            
        }

        /// <summary>
        /// Nom du voyage
        /// </summary>
        public string TripName
        {
            get { return "Africa 2014"; }
        }

    }
}