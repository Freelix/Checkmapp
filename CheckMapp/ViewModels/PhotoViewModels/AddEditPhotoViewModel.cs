﻿using GalaSoft.MvvmLight;
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

namespace CheckMapp.ViewModels.PhotoViewModels
{
    public class AddEditPhotoViewModel : PhoneApplicationPage, INotifyPropertyChanged
    {
        private ICommand _addEditPhotoCommand;

        public AddEditPhotoViewModel(Mode mode, byte[] picture)
        {
            this.Mode = mode;
            if (picture != null)
                this.ImageSource = picture;

            LoadAllPOIFromDatabase();
        }

        #region Properties

        public Mode Mode
        {
            get;
            set;
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

        private byte[] _imageSource;
        public byte[] ImageSource
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
                    // Create the picture
                    Picture picture = new Picture
                    {
                        Description = _description,
                        Date = DateTime.Now,
                        PictureData = ImageSource,
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
            PoiList = dsPoi.LoadListBoxPointOfInterests();
        }

        #endregion
    }
}