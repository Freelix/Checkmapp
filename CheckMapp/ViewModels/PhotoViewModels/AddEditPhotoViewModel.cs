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
        private Picture _picture;

        public AddEditPhotoViewModel(Trip trip, Picture picture, Mode mode, byte[] photoArray)
        {
            this.Trip = trip;
            this.Mode = mode;

            if (this.Mode == Mode.add)
            {
                Picture = new Picture();
                Picture.Date = DateTime.Now;
            }
            else
                Picture = picture;

            if(Trip.PointsOfInterests!=null)
                PoiList = new List<PointOfInterest>(this.Trip.PointsOfInterests);

            if (photoArray != null)
                this.ImageSource = photoArray;
        }

        #region Properties

        /// <summary>
        /// Ma photo
        /// </summary>
        public Picture Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
            }
        }

        public Mode Mode
        {
            get;
            set;
        }


        private List<PointOfInterest> _poiList;
        /// <summary>
        /// Ma liste de points d'intérêt
        /// </summary>
        public List<PointOfInterest> PoiList
        {
            get { return _poiList; }
            set
            {
                _poiList = value;
            }
        }

        /// <summary>
        /// Le id de mon image
        /// </summary>
        public int PictureId
        {
            get { return Picture.Id; }
            set
            {
                Picture.Id = value;
                NotifyPropertyChanged("PictureId");
            }
        }

        /// <summary>
        /// Le point d'intérêt sélectionné
        /// </summary>
        public PointOfInterest POISelected
        {
            get { return Picture.PointOfInterest; }
            set
            {
                Picture.PointOfInterest = value;
                NotifyPropertyChanged("POISelected");
            }
        }

        /// <summary>
        /// La description de ma photo
        /// </summary>
        public string Description
        {
            get { return Picture.Description; }
            set
            {
                Picture.Description = value;
                NotifyPropertyChanged("Description");
            }
        }

        /// <summary>
        /// L'image en tableau de byte
        /// </summary>
        public byte[] ImageSource
        {
            get { return Picture.PictureData; }
            set
            {
                Picture.PictureData = value;
                NotifyPropertyChanged("ImageSource");
            }
        }

        public Trip Trip
        {
            get;
            set;
        }

        /// <summary>
        /// Nom du voyage
        /// </summary>
        public string TripName
        {
            get { return Trip.Name; }
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
        /// <summary>
        /// Ajout ou modification d'une photo
        /// </summary>
        public void AddEditPhoto()
        {
            if (!string.IsNullOrWhiteSpace(Description) && ImageSource != null)
            {
                if (Mode == Mode.add)
                    AddPictureInDB(Picture);
                else if (Mode == Mode.edit)
                    UpdateExistingPicture();
            }
            else
            {
                // Show an appropriate message
            }
        }

        private void AddPictureInDB(Picture picture)
        {
            DataServicePicture dsPicture = new DataServicePicture();
            picture.Trip = Trip;
            Trip.Pictures.Add(picture);
            dsPicture.addPicture(picture);
        }

        private void UpdateExistingPicture()
        {
            DataServicePicture dsPicture = new DataServicePicture();
            dsPicture.UpdatePicture(Picture);
        }

        #endregion
    }
}