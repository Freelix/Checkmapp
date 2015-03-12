using CheckMapp.KeyGroup;
using CheckMapp.Model.Tables;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using System.Linq;
using CheckMapp.Model.DataService;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace CheckMapp.ViewModels.PhotoViewModels
{
    public class PhotoViewModel : ViewModelBase
    {
        private int _selectedPictureIndex;
        public PhotoViewModel(Picture picture)
        {
            SelectedPicture = picture;
            this.Trip = picture.Trip;
        }

        #region Properties


        public int SelectedPictureIndex
        {
            get { return _selectedPictureIndex; }
            set
            {
                _selectedPictureIndex = value;
            }
        }
        public Trip Trip
        {
            get;
            set;
        }

        public List<KeyedList<string, Picture>> GroupedPhotos
        {
            get
            {
                var groupedPhotos =
                    from photo in Trip.Pictures.ToList()
                    orderby photo.Date
                    group photo by photo.Date.ToString("m") into photosByDay
                    select new KeyedList<string, Picture>(photosByDay);

                return new List<KeyedList<string, Picture>>(groupedPhotos);
            }
        }

        public Picture _picture = null;

        public Picture SelectedPicture
        {
            get
            {
                return _picture;
            }
            set
            {
                _picture = value;
                RaisePropertyChanged("SelectedPicture");
            }
        }

       
        #endregion

        #region Buttons

        private ICommand _deletePictureCommand;
        public ICommand DeletePictureCommand
        {
            get
            {
                if (_deletePictureCommand == null)
                {
                    _deletePictureCommand = new RelayCommand(() => DeletePicture());
                }
                return _deletePictureCommand;
            }

        }

        #endregion

        #region DBMethods

        /// <summary>
        /// Suppression d'une photo
        /// </summary>
        public void DeletePicture()
        {
            DataServicePicture dsPicture = new DataServicePicture();
            this.Trip.Pictures.Remove(this.SelectedPicture);
            dsPicture.DeletePicture(this.SelectedPicture);
        }


        #endregion
    }
}