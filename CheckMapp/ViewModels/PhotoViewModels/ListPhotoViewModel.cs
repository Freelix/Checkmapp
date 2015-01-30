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

namespace CheckMapp.ViewModels.PhotoViewModels
{
    public class ListPhotoViewModel : INotifyPropertyChanged
    {
        public ListPhotoViewModel()
        {
            
        }

        #region Properties

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

        //EXEMPLES

        public List<KeyedList<string, Picture>> GroupedPhotos
        {
            get
            {
                //var photos = GetPhotos();
                List<Picture> photos = LoadAllPicturesFromDatabase();

                var groupedPhotos =
                    from photo in photos
                    orderby photo.Date
                    group photo by photo.Date.ToString("m") into photosByDay
                    select new KeyedList<string, Picture>(photosByDay);

                return new List<KeyedList<string,Picture>>(groupedPhotos);
            }
        }

        #region DBMethods

        public List<Picture> LoadAllPicturesFromDatabase()
        {
            DataServicePicture dsPicture = new DataServicePicture();
            //_pictureList = dsPicture.LoadPictures();
            return dsPicture.LoadPictures();
        }

        #endregion

    }
}