using CheckMapp.Model;
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
        private bool _showTrip;
        private ICommand _showCurrentTripCommand;

        /// <summary>
        /// Collection de voyage archives
        /// </summary>
        public ObservableCollection<Trip> ArchiveTripList { get; private set; }

        public ArchivesViewModel()
        {
            this.ArchiveTripList = new ObservableCollection<Trip>();

            Trip trip = new Trip();
            trip.Name = "Voyage 1";
            trip.BeginDate = DateTime.Now.AddDays(-10);
            trip.EndDate = DateTime.Now.AddDays(-2);

            Trip trip2 = new Trip();
            trip2.Name = "Voyage 2";
            trip2.BeginDate = DateTime.Now.AddDays(-20);
            trip2.EndDate = DateTime.Now.AddDays(-12);

            Trip trip3 = new Trip();
            trip3.Name = "Voyage 3";
            trip3.BeginDate = DateTime.Now.AddYears(-1);
            trip3.EndDate = DateTime.Now.AddYears(-1).AddDays(10);

            //Courant
            Trip trip4 = new Trip();
            trip4.Name = "Voyage Courant";
            trip4.BeginDate = DateTime.Now.AddDays(-1);
            trip4.EndDate = null;

            //Courant
            Trip trip5 = new Trip();
            trip5.Name = "Voyage 6";
            trip5.BeginDate = DateTime.Now.AddYears(-10);
            trip5.EndDate = null;

            ArchiveTripList.Add(trip);
            ArchiveTripList.Add(trip2);
            ArchiveTripList.Add(trip2);
            ArchiveTripList.Add(trip5);

            ShowTrip = true;
        }

        /// <summary>
        /// Cache/Affiche le voyage courant
        /// </summary>
        public ICommand ShowCurrentTripCommand
        {
            get
            {
                if (_showCurrentTripCommand == null)
                {
                    _showCurrentTripCommand = new RelayCommand(() => ShowCurrentTrip());
                }
                return _showCurrentTripCommand;
            }

        }

        public void ShowCurrentTrip()
        {
            ShowTrip = !ShowTrip;
        }

        /// <summary>
        /// Si le voyage est caché
        /// </summary>
        public bool ShowTrip
        {
            get { return _showTrip; }
            set
            {
                _showTrip = value;
                RaisePropertyChanged("ShowTrip");
            }
        }

    }
}
