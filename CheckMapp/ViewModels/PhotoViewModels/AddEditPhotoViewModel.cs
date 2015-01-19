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
        private ICommand _addEditPhotoCommand;
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

        public ICommand AddEditPhotoCommand
        {
            get
            {
                if (_addEditPhotoCommand == null)
                {
                    _addEditPhotoCommand = new RelayCommand(() => AddEditPhoto());
                }
                return _addEditPhotoCommand;
            }

        }

        public void AddEditPhoto()
        {
            if (Mode == Mode.add)
            {

            }
            else
            {

            }
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