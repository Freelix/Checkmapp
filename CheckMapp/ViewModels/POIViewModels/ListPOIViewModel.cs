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

        private bool _loading = false;

        public bool Loading
        {
            get { return _loading; }
            set
            {
                _loading = value;
                RaisePropertyChanged("Loading");
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

        /// <summary>
        /// Savoir si on supprime les objets reliés au POI ou on les met a null
        /// </summary>
        public bool DeletePOIObject
        {
            get;
            set;
        }

        #endregion


        #region DBMethods

        public void DeletePOI(PointOfInterest poi)
        {
            if (DeletePOIObject)
            {
                // For some reasons, Picture table doesn't refresh properly
                // We have to remove each element in the array manually
                DataServicePicture dsPicture = new DataServicePicture();
                foreach (Picture pic in dsPicture.LoadPicturesByPoiId(poi.Id))
                {
                    Trip.Pictures.Remove(pic);
                    dsPicture.DeletePicture(pic);
                }

                DataServiceNote dsNotes = new DataServiceNote();
                foreach (Note note in dsNotes.LoadNotesByPoiId(poi.Id))
                {
                    Trip.Notes.Remove(note);
                    dsNotes.DeleteNote(note);
                }
            }
            else
            {
                // For some reasons, Picture table doesn't refresh properly
                // We have to remove each element in the array manually
                DataServicePicture dsPicture = new DataServicePicture();
                foreach (Picture pic in dsPicture.LoadPicturesByPoiId(poi.Id))
                    pic.PointOfInterest = null;

                DataServiceNote dsNotes = new DataServiceNote();
                foreach (Note note in dsNotes.LoadNotesByPoiId(poi.Id))
                    note.PointOfInterest = null;
            }

            DataServicePoi dsPoi = new DataServicePoi();
            Trip.PointsOfInterests.Remove(poi);
            PointOfInterestList.Remove(poi);

            dsPoi.DeletePoi(poi);
        }

        public void DeletePOIs(List<object> poiList)
        {
            foreach (PointOfInterest poi in poiList)
            {
                DeletePOI(poi);
            }
        }


        #endregion
    }
}