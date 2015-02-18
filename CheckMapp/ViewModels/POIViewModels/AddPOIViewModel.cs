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
    public class AddPOIViewModel : PhoneApplicationPage, INotifyPropertyChanged
    {
        private ICommand _addPOICommand;

        // Used for validate the form
        private IValidator<PointOfInterest> _validator;

        /// <summary>
        /// Initializes a new instance of the AddPOIViewModel class.
        /// </summary>
        public AddPOIViewModel(Trip trip)
        {
            this.Trip = trip;
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

        public bool IsVisible
        {
            get
            {
                return true;
            }
        }

        public Trip Trip { get; set; }

        public string TripName
        {
            get { return Trip.Name; }
        }

        private string _name;
        public string PoiName
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("PoiName");
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

        #region DBMethods

        /// <summary>
        /// Ajout d'un point d'intérêt
        /// </summary>
        public void AddPOI()
        {
            // Adding a note
            if (Mode == Mode.add)
            {
                // TODO: Replace that part with a request using bing maps API (key needed)
                PointOfInterest newPOI = new PointOfInterest
                {
                    Name = _name,
                    City = "Sherbrooke",
                    Longitude = -71.88,
                    Latitude = 45.40
                };

                // If the form is not valid, a notification will appear
                if (ValidationErrorsHandler.IsValid(_validator, newPOI))
                {
                    _isFormValid = true;

                    AddPoiInDB(newPOI);
                }
            }
        }

        public void AddPoiInDB(PointOfInterest poi)
        {
            DataServicePoi dsPoi = new DataServicePoi();
            poi.Trip = Trip;
            Trip.PointsOfInterests.Add(poi);
            dsPoi.addPoi(poi);
        }

        #endregion

    }
}