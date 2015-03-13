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
    public class ListPhotoViewModel : ViewModelBase
    {
        public ListPhotoViewModel(Trip trip)
        {
            this.Trip = trip;
            Loading = true;
        }

        public ListPhotoViewModel(Trip trip, int poiId)
        {
            this.Trip = trip;
            this.PoiLoaded = poiId;
            Loading = true;
        }

        #region Properties

        /// <summary>
        /// Le voyage choisi
        /// </summary>
        public Trip Trip
        {
            get;
            set;
        }

        /// <summary>
        /// Si c'est des photos à partir d'un poi
        /// </summary>
        public int? PoiLoaded
        {
            get;
            set;
        }


        private ICommand _deletePictureCommand;
        public ICommand DeletePictureCommand
        {
            get
            {
                if (_deletePictureCommand == null)
                {
                    _deletePictureCommand = new RelayCommand<Picture>((picture) => DeletePicture(picture));
                }
                return _deletePictureCommand;
            }

        }

        private ICommand _deletePicturesCommand;
        public ICommand DeletePicturesCommand
        {
            get
            {
                if (_deletePicturesCommand == null)
                {
                    _deletePicturesCommand = new RelayCommand<List<object>>((pictureList) => DeletePictures(pictureList));
                }
                return _deletePicturesCommand;
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




        #endregion

        //EXEMPLES

        public List<KeyedList<string, Picture>> GroupedPhotos
        {
            get
            {
                List<Picture> pictureList = null;
                if (PoiLoaded == null)
                    pictureList = Trip.Pictures.ToList();
                else
                    pictureList = Trip.Pictures.Where(x => (x.PointOfInterest != null) && (x.PointOfInterest.Id == PoiLoaded)).ToList();

                var groupedPhotos =
                    from photo in pictureList
                    orderby photo.Date
                    group photo by photo.Date.ToString("m") into photosByDay
                    select new KeyedList<string, Picture>(photosByDay);

                return new List<KeyedList<string, Picture>>(groupedPhotos);
            }
        }

        #region DBMethods

        public void DeletePictures(List<object> pictureList)
        {
            DataServicePicture dsPicture = new DataServicePicture();
            foreach (Picture picture in pictureList)
            {
                Trip.Pictures.Remove(picture);
                dsPicture.DeletePicture(picture);
            }
        }

        public void DeletePicture(Picture picture)
        {
            DataServicePicture dsPicture = new DataServicePicture();
            Trip.Pictures.Remove(picture);
            dsPicture.DeletePicture(picture);
        }


        #endregion

    }
}