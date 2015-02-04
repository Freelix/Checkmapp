using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CheckMapp.Model.DataService;
using CheckMapp.Model.Tables;
using System.Collections.Generic;
using System.Device.Location;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace CheckMapp.ViewModels.POIViewModels
{
    public class ListPOIViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the POIViewModel class.
        /// </summary>
        public ListPOIViewModel()
        {
            List<PointOfInterest> poiList = new List<PointOfInterest>();

            PointOfInterest poi = new PointOfInterest();
            poi.City = "Drummondville";
            poi.Name = "Heriot";
            poi.Latitude = 20.0;
            poi.Longitude = 12.0;

            PointOfInterest poi2 = new PointOfInterest();
            poi2.City = "Victoriaville";
            poi2.Name = "Heriot";
            poi2.Latitude = 25.0;
            poi2.Longitude = 3.0;

            PointOfInterestList = new ObservableCollection<PointOfInterest>();

            PointOfInterestList.Add(poi);
            PointOfInterestList.Add(poi2);

            //LoadAllPoiFromDatabase();
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

        private ICommand _deletePOIsCommand;
        public ICommand DeletePOIsCommand
        {
            get
            {
                if (_deletePOIsCommand == null)
                {
                    _deletePOIsCommand = new RelayCommand<List<PointOfInterest>>((poiList) => DeletePOIs(poiList));
                }
                return _deletePOIsCommand;
            }

        }

        private ICommand _deletePOICommand;
        public ICommand DeletePOICommand
        {
            get
            {
                if (_deletePOICommand == null)
                {
                    _deletePOICommand = new RelayCommand<PointOfInterest>((poi) => DeletePOI(poi));
                }
                return _deletePOICommand;
            }

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

        public void DeletePOI(PointOfInterest poi)
        {
            DataServicePoi dsPoi = new DataServicePoi();
            
        }

        public void DeletePOIs(List<PointOfInterest> poiList)
        {
            DataServicePoi dsPoi = new DataServicePoi();
        }


        public void LoadAllPoiFromDatabase()
        {
            DataServicePoi dsPoi = new DataServicePoi();
            var allPoiInDB = dsPoi.LoadPointOfInterests();

            PointOfInterestList = new ObservableCollection<PointOfInterest>(allPoiInDB);
        }

        #endregion
    }
}