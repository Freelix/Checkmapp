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


        private ICommand _addTripCommand;
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
        public ICommand AddTripCommand
        {
            get
            {
                if (_addTripCommand == null)
                {
                    _addTripCommand = new RelayCommand(() => AddTrip());
                }
                return _addTripCommand;
            }

        }

        public void AddTrip()
        {
           
        }
    }
}