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

        public ArchivesViewModel()
        {
            LoadAllTripFromDatabase();
        }

        public void LoadAllTripFromDatabase()
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            ArchiveTripList = new ObservableCollection<Trip>(dsTrip.LoadArchiveTrip());
        }

    }
}
