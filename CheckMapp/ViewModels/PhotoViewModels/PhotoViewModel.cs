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
            this.Trip = picture.Trip;
            SelectedPictureIndex = Trip.Pictures.OrderBy(x => x.Date).ToList().FindIndex(x => x.Id == picture.Id);
        }

        #region Properties

        /// <summary>
        /// Index de la photo en cours
        /// </summary>
        public int SelectedPictureIndex
        {
            get { return _selectedPictureIndex; }
            set
            {
                _selectedPictureIndex = value;

                if (_selectedPictureIndex < 0)
                    _selectedPictureIndex = Trip.Pictures.Count - 1;

                if (_selectedPictureIndex >= Trip.Pictures.Count)
                    _selectedPictureIndex = 0;
            }
        }

        /// <summary>
        /// Voyage courant
        /// </summary>
        public Trip Trip
        {
            get;
            set;
        }

        /// <summary>
        /// L'objet photo courant
        /// </summary>
        public Picture SelectedPicture
        {
            get
            {
                return Trip.Pictures.OrderBy(x => x.Date).ToList()[SelectedPictureIndex];
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