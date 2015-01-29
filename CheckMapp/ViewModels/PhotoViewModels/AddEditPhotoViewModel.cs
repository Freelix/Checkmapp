using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using System;
using CheckMapp.Model.Tables;
using CheckMapp.Model.DataService;
using Utility = CheckMapp.Utils.Utility;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace CheckMapp.ViewModels.PhotoViewModels
{
    public class AddEditPhotoViewModel : PhoneApplicationPage, INotifyPropertyChanged
    {
        private ICommand _addEditPhotoCommand;

        public AddEditPhotoViewModel(Mode mode)
        {
            this.Mode = mode;
            LoadAllPOIFromDatabase();
        }

        #region Properties

        public Mode Mode
        {
            get;
            set;
        }

        private int _pictureId;
        public int PictureId
        {
            get { return _pictureId; }
            set
            {
                _pictureId = value;
                NotifyPropertyChanged("PictureId");
            }
        }

        private string _poiId;
        public string POIID
        {
            get { return _poiId; }
            set
            {
                _poiId = value;
                NotifyPropertyChanged("POIID");
            }
        }

        private List<PointOfInterest> _poiList;
        public List<PointOfInterest> PoiList
        {
            get { return _poiList; }
            set
            {
                _poiList = value;
                NotifyPropertyChanged("PoiList");
            }
        }

        private PointOfInterest _poiSelected;
        public PointOfInterest POISelected
        {
            get { return _poiSelected; }
            set
            {
                _poiSelected = value;
                NotifyPropertyChanged("POISelected");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                NotifyPropertyChanged("ImageSource");
            }
        }

        /// <summary>
        /// Nom du voyage
        /// </summary>
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

        public ICommand AddEditPhotoCommand
        {
            get
            {
                if (_addEditPhotoCommand == null)
                {
                    _addEditPhotoCommand = new RelayCommand(() => AddEditPhoto());
                }
                return _addEditPhotoCommand;
            }

        }

        #region DBMethods

        public void AddEditPhoto()
        {
            if (Mode == Mode.add)
            {
                if (!string.IsNullOrWhiteSpace(_description) /*&& !string.IsNullOrWhiteSpace(_imageSource)*/)
                {
                    // Retrieve image path from source
                    string imagePath = "/Images/vacance.jpg";

                    MemoryStream ms = Utility.ImageToByteArray(imagePath);

                    // Create the picture
                    Picture picture = new Picture
                    {
                        Description = _description,
                        Date = DateTime.Now,
                        PictureData = ms.ToArray(),
                        PointOfInterest = RetrievePOI()
                    };

                    AddPictureInDB(picture);

                    (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
                }
                else
                {
                    // Show an appropriate message
                }
            }
            else if (Mode == Mode.edit)
            {
                // Edit a picture
                if (!string.IsNullOrWhiteSpace(_description) /*&& !string.IsNullOrWhiteSpace(_imageSource)*/)
                {
                    UpdateExistingPicture();

                    (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
                }
                else
                {
                    // Show an appropriate message
                }
            }
        }

        private void AddPictureInDB(Picture picture)
        {
            DataServicePicture dsPicture = new DataServicePicture();
            dsPicture.addPicture(picture);
        }

        private PointOfInterest RetrievePOI()
        {
            PointOfInterest p = new PointOfInterest();

            // If the point of interest is set
            if (!string.IsNullOrWhiteSpace(_poiId))
            {
                int id = Utility.StringToNumber(_poiId);

                if (id > -1)
                    p = getPOIById(id);
            }

            return p;
        }

        private PointOfInterest getPOIById(int id)
        {
            DataServicePoi dsPoi = new DataServicePoi();
            return dsPoi.getPOIById(id);
        }

        private void LoadAllPOIFromDatabase()
        {
            DataServicePoi dsPoi = new DataServicePoi();
            _poiList = dsPoi.LoadListBoxPointOfInterests();
            _poiId = dsPoi.getDefaultPOI().Id.ToString();
        }

        public void ShowInfo(Picture pictureToModify)
        {
            if (pictureToModify != null)
            {
                _description = pictureToModify.Description;
                _pictureId = pictureToModify.Id;

                // Set the picture to the view
                //_imageSource = Utility.ByteArrayToImage(pictureToModify.PictureData);

                if (pictureToModify.PointOfInterest != null)
                {
                    _poiId = pictureToModify.PointOfInterest.Id.ToString();
                    _poiSelected = getPOIById(pictureToModify.PointOfInterest.Id);
                }
            }
        }

        private void UpdateExistingPicture()
        {
            DataServicePicture dsPicture = new DataServicePicture();

            Picture updatedPicture = new Picture();

            string imagePath = "/Images/vacance.jpg";
            MemoryStream ms = Utility.ImageToByteArray(imagePath);

            updatedPicture.Id = _pictureId;
            updatedPicture.PictureData = ms.ToArray();
            updatedPicture.PointOfInterest = RetrievePOI();
            updatedPicture.Description = _description;

            dsPicture.UpdatePicture(updatedPicture);
        }

        #endregion
    }
}