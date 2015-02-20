using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.ComponentModel;
using CheckMapp.Model.Tables;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Controls;
using System.Windows;
using CheckMapp.Model.DataService;
using FluentValidation;
using CheckMapp.Utils.Validations;

namespace CheckMapp.ViewModels.POIViewModels
{
    public class AddEditPOIViewModel : ViewModelBase
    {
        private ICommand _addPOICommand;

        // Used for validate the form
        private IValidator<PointOfInterest> _validator;

        /// <summary>
        /// Initializes a new instance of the AddPOIViewModel class.
        /// </summary>
        public AddEditPOIViewModel(Trip trip, Mode mode, PointOfInterest poi)
        {
            this.Trip = trip;
            this.Mode = mode;

            if (this.Mode == Mode.add)
            {
                PointOfInterest = new Model.Tables.PointOfInterest();
            }
            else
                PointOfInterest = poi;

            InitialiseValidator();
        }

        private void InitialiseValidator()
        {
            _validator = new ValidatorFactory().GetValidator<PointOfInterest>();
        }

        /// <summary>
        /// Cache/Affiche le voyage courant
        /// </summary>
        public ICommand AddPOICommand
        {
            get
            {
                if (_addPOICommand == null)
                {
                    _addPOICommand = new RelayCommand(() => AddPOI());
                }
                return _addPOICommand;
            }

        }

        #region Properties

        private PointOfInterest _pointOfInterest;

        public PointOfInterest PointOfInterest
        {
            get
            {
                return _pointOfInterest;
            }
            set
            {
                _pointOfInterest = value;
            }
        }

        private bool _isFormValid;

        public bool IsFormValid
        {
            get { return _isFormValid; }
            set
            {
                _isFormValid = value;
            }
        }

        public Mode Mode
        {
            get;
            set;
        }

        public Trip Trip { get; set; }

        public string TripName
        {
            get { return Trip.Name; }
        }

        public string PoiName
        {
            get { return PointOfInterest.Name; }
            set
            {
                PointOfInterest.Name = value;
                RaisePropertyChanged("PoiName");
            }
        }

        public string PoiLocation
        {
            get
            {
                return PointOfInterest.Location;
            }
            set
            {
                PointOfInterest.Location = value;
                RaisePropertyChanged("PoiLocation");
            }
        }

        public double Latitude
        {
            get { return PointOfInterest.Latitude; }
            set
            {
                PointOfInterest.Latitude = value;
            }
        }

        public double Longitude
        {
            get { return PointOfInterest.Longitude; }
            set
            {
                PointOfInterest.Longitude = value;
            }
        }

        #endregion

        #region DBMethods

        /// <summary>
        /// Ajout d'un point d'intérêt
        /// </summary>
        public void AddPOI()
        {
            if (ValidationErrorsHandler.IsValid(_validator, PointOfInterest))
            {
                _isFormValid = true;
                // Adding a poi
                if (Mode == Mode.add)
                    AddPoiInDB(PointOfInterest);
                else
                    UpdateExistingPOI();
            }
        }

        public void AddPoiInDB(PointOfInterest poi)
        {
            DataServicePoi dsPoi = new DataServicePoi();
            poi.Trip = Trip;
            Trip.PointsOfInterests.Add(poi);
            dsPoi.addPoi(poi);
        }

        private void UpdateExistingPOI()
        {
            DataServicePoi dsPOI = new DataServicePoi();
            dsPOI.UpdatePoi(PointOfInterest);
        }

        #endregion

    }
}