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
            this.Mode = mode;

            if (this.Mode == Mode.add && !Utility.IsTombstoned())
            {
                Picture = new Picture();
                Picture.Trip = trip;
                Picture.Date = DateTime.Now;
            }
            else
                Picture = picture;

            if (trip.PointsOfInterests != null)
                PoiList = new List<PointOfInterest>(trip.PointsOfInterests);

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
        /// <summary>
        /// Si la form est valide
        /// </summary>
        public bool IsFormValid
        {
            get { return _isFormValid; }
            set
            {
                _isFormValid = value;
            }
        }

        private bool _noneCheck;
        /// <summary>
        /// Si on coche aucun points d'intérêt
        /// </summary>
        public bool NoneCheck
        {
            get { return _noneCheck; }
            set { _noneCheck = value; }
        }


        /// <summary>
        /// La photo choisie
        /// </summary>
        public Picture Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
            }
        }

        /// <summary>
        /// Mode édition ou ajout
        /// </summary>
        public Mode Mode
        {
            get;
            set;
        }

        private List<PointOfInterest> _poiList;
        /// <summary>
        /// La liste de points d'intérêt
        /// </summary>
        public List<PointOfInterest> PoiList
        {
            get { return _poiList; }
            set
            {
                _poiList = value;
                RaisePropertyChanged("PoiList");
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
        /// La description de la photo
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


        private ICommand _cancelPhotoCommand;
        public ICommand CancelPhotoCommand
        {
            get
            {
                if (_cancelPhotoCommand == null)
                {
                    _cancelPhotoCommand = new RelayCommand(() => CancelPhoto());
                }
                return _cancelPhotoCommand;
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
                    AddPictureInDB();
                else if (Mode == Mode.edit)
                    UpdateExistingPicture();
            }
        }

        public void CancelPhoto()
        {
            if (Mode == ViewModels.Mode.add)
            {
                Picture.Trip = null;
            }
        }

        private void AddPictureInDB()
        {
            // Workaround explained in AddEditNoteViewModel 
            Picture.Trip.Pictures.Add(Picture);
            Picture.Trip = Utility.GetAssociatedTrip(Picture.Trip.Id);

            DataServicePicture dsPicture = new DataServicePicture();
            dsPicture.addPicture(Picture);
        }

        private void UpdateExistingPicture()
        {
            DataServicePicture dsPicture = new DataServicePicture();
            dsPicture.UpdatePicture(Picture);
        }

        #endregion
    }
}