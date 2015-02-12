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
    public class ListPhotoViewModel : INotifyPropertyChanged
    {
        public ListPhotoViewModel(Trip trip)
        {
            this.Trip = trip;
        }

        public ListPhotoViewModel(Trip trip, int poiId)
        {
            this.Trip = trip;
            this.PoiLoaded = poiId;
        } 

        #region Properties

        public Trip Trip
        {
            get;
            set;
        }
        public string TripName
        {
            get { return Trip.Name; }
        }

        public int? PoiLoaded
        {
            get;
            set;
        }

        private List<Picture> _pictureList;
        public List<Picture> PictureList
        {
            get { return Trip.Pictures.ToList(); }
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
                    _deletePicturesCommand = new RelayCommand<List<Picture>>((pictureList) => DeletePictures(pictureList));
                }
                return _deletePicturesCommand;
            }

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

                return new List<KeyedList<string,Picture>>(groupedPhotos);
            }
        }

        #region DBMethods

        public void DeletePictures(List<Picture> pictureList)
        {
            DataServicePicture dsPicture = new DataServicePicture();
            foreach (Picture picture in pictureList)
            {
                dsPicture.DeletePicture(picture);
            }
        }

        public void DeletePicture(Picture picture)
        {
            DataServicePicture dsPicture = new DataServicePicture();
            dsPicture.DeletePicture(picture);
        }


        public List<Picture> LoadAllPicturesByPoiId(int poiId)
        {
            DataServicePicture dsPicture = new DataServicePicture();
            _pictureList = dsPicture.LoadPicturesByPoiId(poiId);
            return _pictureList;
        }

        #endregion

    }
}