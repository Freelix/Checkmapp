using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CheckMapp.Model.DataService;
using CheckMapp.Model.Tables;

namespace CheckMapp.ViewModels.POIViewModels
{
    public class ListPOIViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the POIViewModel class.
        /// </summary>
        public ListPOIViewModel()
        {
            /*POI poi1 = new POI();
            poi1.date = DateTime.Now.AddDays(-2);
            poi1.nom = "Tour effeil";
            poi1.ville = "Paris";

            POI poi2 = new POI();
            poi2.date = DateTime.Now;
            poi2.nom = "Louvre";
            poi2.ville = "Paris";

            POIList.Add(poi1);
            POIList.Add(poi2);*/

            LoadAllPoiFromDatabase();
        }

        #region Properties

        private ObservableCollection<PointOfInterest> _pointOfInterestList;
        public ObservableCollection<PointOfInterest> PointOfInterestList
        {
            get { return _pointOfInterestList; }
            set
            {
                _pointOfInterestList = value;
                NotifyPropertyChanged("PointOfInterestList");
            }
        }

        public string TripName
        {
            get { return "Africa 2014"; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region DBMethods

        public void LoadAllPoiFromDatabase()
        {
            DataServicePoi dsPoi = new DataServicePoi();
            var allPoiInDB = dsPoi.LoadPointOfInterests();

            PointOfInterestList = new ObservableCollection<PointOfInterest>(allPoiInDB);
        }

        #endregion
    }
}