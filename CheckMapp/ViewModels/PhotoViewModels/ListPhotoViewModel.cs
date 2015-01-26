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
        public static List<Picture> GetPhotos()
        {
            List<Picture> imageList = new List<Picture>();
            Random _rnd = new Random(42 * 42);
            DateTime start = new DateTime(2010, 1, 1);

            for (int i = 1; i <= 30; i++)
            {
                BitmapImage image = new BitmapImage();
                image.CreateOptions = BitmapCreateOptions.None;
                image.UriSource = new Uri(String.Format("/Images/vacance.jpg", i), UriKind.Relative);
                WriteableBitmap wbmp = new WriteableBitmap(image);
                MemoryStream ms = new MemoryStream();
                wbmp.SaveJpeg(ms, wbmp.PixelWidth, wbmp.PixelHeight, 0, 100);

                Picture imageData = new Picture()
                {
                    PictureData = ms.ToArray(),
                    Description = i.ToString(),
                    Date = start.AddDays(_rnd.Next(0, 450))
                };

                imageList.Add(imageData);
            }

            return imageList;
        }

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