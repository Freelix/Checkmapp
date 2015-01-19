using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace CheckMapp.ViewModels.TripViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AddEditTripViewModel : ViewModelBase
    {


        private ICommand _addEditTripCommand;
        /// <summary>
        /// Initializes a new instance of the AddTripViewModel class.
        /// </summary>
        public AddEditTripViewModel(Mode mode)
        {
            this.Mode = mode;
        }

        public Mode Mode
        {
            get;
            set;
        }

        /// <summary>
        /// Commande pour ajouter un voyage
        /// </summary>
        public ICommand AddEditTripCommand
        {
            get
            {
                if (_addEditTripCommand == null)
                {
                    _addEditTripCommand = new RelayCommand(() => AddEditTrip());
                }
                return _addEditTripCommand;
            }

        }

        public void AddEditTrip()
        {
            if (Mode == Mode.add)
            {

            }
            else
            {

            }
        }
    }
}