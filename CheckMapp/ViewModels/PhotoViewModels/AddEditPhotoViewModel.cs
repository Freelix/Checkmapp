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
using FluentValidation;
using CheckMapp.Utils.Validations;

namespace CheckMapp.ViewModels.PhotoViewModels
{
    public class AddEditPhotoViewModel : ViewModelBase
    {
        private ICommand _addEditPhotoCommand;
        private Picture _picture;

        // Used for validate the form
        private IValidator<Picture> _validator;

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

            if (Trip.PointsOfInterests != null)
            {
                PoiList = new List<PointOfInterest>(this.Trip.PointsOfInterests);
            }

            if (photoArray != null)
                this.ImageSource = photoArray;

            InitialiseValidator();
        }

        private void InitialiseValidator()
        {
            _validator = new ValidatorFactory().GetValidator<Picture>();
        }

        #region Properties

        private bool _isFormValid;

        public bool IsFormValid
        {
            get { return _isFormValid; }
            set
            {
                _isFormValid = value;
            }
        }

        private bool _noneCheck;

        public bool NoneCheck
        {
            get { return _noneCheck; }
            set { _noneCheck = value; }
        }
       

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
                RaisePropertyChanged("PictureId");
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
                RaisePropertyChanged("POISelected");
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
                RaisePropertyChanged("Description");
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
                RaisePropertyChanged("ImageSource");
            }
        }

        public Trip Trip
        {
            get;
            set;
        }

        /// <summary>
        /// La date de ma photo
        /// </summary>
        public DateTime PictureDate
        {
            get { return Picture.Date; }
            set
            {
                Picture.Date = value;
                RaisePropertyChanged("PictureDate");
            }
        }

        /// <summary>
        /// Nom du voyage
        /// </summary>
        public string TripName
        {
            get { return Trip.Name; }
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
            // If the form is not valid, a notification will appear
            if (ValidationErrorsHandler.IsValid(_validator, Picture))
            {
                _isFormValid = true;

                if (NoneCheck)
                    Picture.PointOfInterest = null;

                if (Mode == Mode.add)
                    AddPictureInDB(Picture);
                else if (Mode == Mode.edit)
                    UpdateExistingPicture();
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