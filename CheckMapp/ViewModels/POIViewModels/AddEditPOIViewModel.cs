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
using Utility = CheckMapp.Utils.Utility;
using CheckMapp.Utils.EditableObject;

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
            this.Mode = mode;

            if (this.Mode == Mode.add && !Utility.IsTombstoned())
            {
                PointOfInterest = new Model.Tables.PointOfInterest();
                PointOfInterest.Trip = trip;
            }
            else if (this.Mode == Mode.addEdit && !Utility.IsTombstoned())
                PointOfInterest = poi;
            else
                PointOfInterest = poi;

            EditableObject = new Caretaker<PointOfInterest>(this.PointOfInterest);
            EditableObject.BeginEdit();

            InitialiseValidator();
        }

        private void InitialiseValidator()
        {
            _validator = new ValidatorFactory().GetValidator<PointOfInterest>();
        }

        /// <summary>
        /// Mon objet editable, nécessaire pour annuler les changements
        /// </summary>
        private Caretaker<PointOfInterest> EditableObject { get; set; }
        /// <summary>
        /// Ajout d'un point d'intérêt
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

        private ICommand _cancelPOICommand;
        public ICommand CancelPOICommand
        {
            get
            {
                if (_cancelPOICommand == null)
                {
                    _cancelPOICommand = new RelayCommand(() => CancelPOI());
                }
                return _cancelPOICommand;
            }
        }

        #region Properties

        private PointOfInterest _pointOfInterest;

        /// <summary>
        /// Le point d'intérêt courant
        /// </summary>
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

        /// <summary>
        /// Si la form est valid
        /// </summary>
        public bool IsFormValid
        {
            get { return _isFormValid; }
            set
            {
                _isFormValid = value;
            }
        }

        /// <summary>
        /// Mode édition ou ajout
        /// </summary>
        public Mode Mode
        {
            get;
            set;
        }

        /// <summary>
        /// Le nom de notre point d'intérêt
        /// </summary>
        public string PoiName
        {
            get { return PointOfInterest.Name; }
            set
            {
                PointOfInterest.Name = value;
                RaisePropertyChanged("PoiName");
            }
        }

        /// <summary>
        /// La localisation en texte
        /// </summary>
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

        /// <summary>
        /// La longitude
        /// </summary>
        public double Latitude
        {
            get { return PointOfInterest.Latitude; }
            set
            {
                PointOfInterest.Latitude = value;
            }
        }

        /// <summary>
        /// La latitude
        /// </summary>
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
                    AddPoiInDB();
                else if (Mode == Mode.addEdit)
                    AddPoiInDB();
                else
                    UpdateExistingPOI();

                EditableObject.EndEdit();
            }
        }

        public void CancelPOI()
        {
            if (Mode == ViewModels.Mode.add || Mode == ViewModels.Mode.addEdit)
            {
                PointOfInterest.Trip.PointsOfInterests.Remove(PointOfInterest);
                PointOfInterest.Trip = null;
            }

            EditableObject.CancelEdit();
        }

        /// <summary>
        /// Ajouter le point
        /// </summary>
        public void AddPoiInDB()
        {
            // Workaround explained in AddEditNoteViewModel
            PointOfInterest.Trip.PointsOfInterests.Add(PointOfInterest);
            PointOfInterest.Trip = Utility.GetAssociatedTrip(PointOfInterest.Trip.Id);

            DataServicePoi dsPoi = new DataServicePoi();
            dsPoi.addPoi(PointOfInterest);
        }

        /// <summary>
        /// Mettre à jour le point
        /// </summary>
        private void UpdateExistingPOI()
        {
            DataServicePoi dsPOI = new DataServicePoi();
            dsPOI.UpdatePoi(PointOfInterest);
        }

        #endregion

    }
}