using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace CheckMapp.ViewModels.NoteViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AddNoteViewModel : ViewModelBase
    {
        private ICommand _addNoteCommand;
        /// <summary>
        /// Initializes a new instance of the AddTripViewModel class.
        /// </summary>
        public AddNoteViewModel()
        {

        }

        /// <summary>
        /// Cache/Affiche le voyage courant
        /// </summary>
        public ICommand AddNoteCommand
        {
            get
            {
                if (_addNoteCommand == null)
                {
                    _addNoteCommand = new RelayCommand(() => AddNote());
                }
                return _addNoteCommand;
            }

        }

        public string TripName
        {
            get { return "Africa 2014"; }
        }

        public void AddNote()
        {

        }
    }
}