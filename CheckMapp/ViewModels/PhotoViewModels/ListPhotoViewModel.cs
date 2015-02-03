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
        public ListPhotoViewModel()
        {
            LoadAllPicturesFromDatabase();
        }

        #region Properties

        public string TripName
        {
            get { return "Africa 2014"; }
        }

        private List<Picture> _pictureList;
        public List<Picture> PictureList
        {
            get { return _pictureList; }
            set
            {
                _pictureList = value;
                NotifyPropertyChanged("PictureList");
            }
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
                var groupedPhotos =
                    from photo in PictureList
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

        public List<Picture> LoadAllPicturesFromDatabase()
        {
            DataServicePicture dsPicture = new DataServicePicture();
            _pictureList = dsPicture.LoadPictures();
            return dsPicture.LoadPictures();
        }

        #endregion

    }
}