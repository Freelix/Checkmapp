using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CheckMapp.Model.Tables;
using System.ComponentModel;
using System.Windows.Input;
using CheckMapp.Model.DataService;
using System.Windows.Media.Imaging;
using Utility = CheckMapp.Utils.Utility;

namespace CheckMapp.ViewModels.PhotoViewModels
{
    public class PhotoViewModel : INotifyPropertyChanged
    {
        public PhotoViewModel(int id)
        {
            LoadPictureFromDatabase(id);
        }

        #region Properties

        private Picture _picture;
        public Picture Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                NotifyPropertyChanged("Picture");
            }
        }

        private BitmapImage _photoToShow;
        public BitmapImage PhotoToShow
        {
            get { return _photoToShow; }
            set
            {
                _photoToShow = value;
                NotifyPropertyChanged("PhotoToShow");
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

        public void LoadPictureFromDatabase(int id)
        {
            if (id > 0)
            {
                DataServicePicture dsPicture = new DataServicePicture();
                _picture = dsPicture.getPictureById(id);

                // Set the picture to the view
                _photoToShow = Utility.ByteArrayToImage(_picture.PictureData);
            }
        }

        public void DeletePicture()
        {
            DataServicePicture dsPicture = new DataServicePicture();
            dsPicture.DeletePicture(_picture);
        }

        #endregion
    }
}