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
            PointOfInterestList = new ObservableCollection<PointOfInterest>(Trip.PointsOfInterests);
        }

        #region Properties

        /// <summary>
        /// Le voyage courant
        /// </summary>
        public Trip Trip
        {
            get;
            set;
        }

        private ObservableCollection<PointOfInterest> _pointOfInterestList;
        /// <summary>
        /// La liste des points d'intérêts
        /// </summary>
        public ObservableCollection<PointOfInterest> PointOfInterestList
        {
            get { return _pointOfInterestList; }
            set
            {
                _pointOfInterestList = value;
                RaisePropertyChanged("PointOfInterestList");
            }
        }

        private ICommand _deletePOIsCommand;
        public ICommand DeletePOIsCommand
        {
            get
            {
                if (_deletePOIsCommand == null)
                {
                    _deletePOIsCommand = new RelayCommand<List<object>>((poiList) => DeletePOIs(poiList));
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
            PointOfInterestList.Remove(poi);
            // For some reasons, Picture table doesn't refresh properly
            // We have to remove each element in the array manually
            DataServicePicture dsPicture = new DataServicePicture();
            foreach (Picture pic in dsPicture.LoadPicturesByPoiId(poi.Id))
                Trip.Pictures.Remove(pic);

            dsPoi.DeletePoi(poi);
        }

        public void DeletePOIs(List<object> poiList)
        {
            DataServicePoi dsPoi = new DataServicePoi();
            foreach (PointOfInterest poi in poiList)
            {
                PointOfInterestList.Remove(poi);
                Trip.PointsOfInterests.Remove(poi);
                dsPoi.DeletePoi(poi);
            }
        }
     

        #endregion
    }
}