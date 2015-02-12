using CheckMapp.Model.DataService;
using CheckMapp.Model.Tables;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CheckMapp.ViewModels.ArchivesViewModels
{
    public class ArchivesViewModel : ViewModelBase
    {
        /// <summary>
        /// Collection de voyage archives
        /// </summary>
        public ObservableCollection<Trip> ArchiveTripList { get; private set; }

        public ArchivesViewModel(List<Trip> trip)
        {
            ArchiveTripList = new ObservableCollection<Trip>(trip);
        }

        private ICommand _deleteTripCommand;
        public ICommand DeleteTripCommand
        {
            get
            {
                if (_deleteTripCommand == null)
                {
                    _deleteTripCommand = new RelayCommand<Trip>((trip) => DeleteTrip(trip));
                }
                return _deleteTripCommand;
            }

        }

        private void DeleteTrip(Trip trip)
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            dsTrip.DeleteTrip(trip);
        }

    }
}
