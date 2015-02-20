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
    public class ListPOIViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the POIViewModel class.
        /// </summary>
        public ListPOIViewModel(Trip trip)
        {
            this.Trip = trip;
            PointOfInterestList = new ObservableCollection<PointOfInterest>(this.Trip.PointsOfInterests);
        }

        #region Properties

        public Trip Trip
        {
            get;
            set;
        }

        private ObservableCollection<PointOfInterest> _pointOfInterestList;
        public ObservableCollection<PointOfInterest> PointOfInterestList
        {
            get { return _pointOfInterestList; }
            set
            {
                _pointOfInterestList = value;
                RaisePropertyChanged("PointOfInterestList");
            }
        }

        public string TripName
        {
            get { return Trip.Name; }
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


        #region DBMethods

        public void DeletePOI(PointOfInterest poi)
        {
            DataServicePoi dsPoi = new DataServicePoi();
            Trip.PointsOfInterests.Remove(poi);
            dsPoi.DeletePoi(poi);
        }

        public void DeletePOIs(List<PointOfInterest> poiList)
        {
            DataServicePoi dsPoi = new DataServicePoi();
            foreach (PointOfInterest poi in poiList)
            {
                Trip.PointsOfInterests.Remove(poi);
                dsPoi.DeletePoi(poi);
            }
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