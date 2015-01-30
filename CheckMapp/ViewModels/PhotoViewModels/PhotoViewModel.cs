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
        private Picture _picture;
        public PhotoViewModel(Picture picture)
        {
            this.Picture = picture;
        }

        #region Properties

        public Picture Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                NotifyPropertyChanged("Picture");
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
            dsPicture.DeletePicture(_picture);
        }

        #endregion
    }
}