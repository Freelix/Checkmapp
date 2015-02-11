using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CheckMapp.Model.Tables;
using System.ComponentModel;
using System.Windows.Input;
using CheckMapp.Model.DataService;
using System.Windows.Media.Imaging;
using Utility = CheckMapp.Utils.Utility;
using System.Collections.Generic;

namespace CheckMapp.ViewModels.PhotoViewModels
{
    public class PhotoViewModel : INotifyPropertyChanged
    {
        private List<Picture> _pictureList;
        private int _picture;
        public PhotoViewModel(int picture)
        {
            this.SelectedPicture = picture;
            LoadPictures();
        }

        #region Properties

        public List<Picture> PictureList
        {
            get { return _pictureList; }
            set
            {
                _pictureList = value;
                NotifyPropertyChanged("PictureList");
            }
        }

        public int SelectedPicture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                NotifyPropertyChanged("SelectedPicture");
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

        /// <summary>
        /// Suppression d'une photo
        /// </summary>
        public void DeletePicture()
        {
            DataServicePicture dsPicture = new DataServicePicture();
            dsPicture.DeletePicture(_pictureList[SelectedPicture]);
        }

        public void LoadPictures()
        {
            DataServicePicture dsPicture = new DataServicePicture();
            _pictureList = dsPicture.LoadPictures();
        }


        #endregion
    }
}