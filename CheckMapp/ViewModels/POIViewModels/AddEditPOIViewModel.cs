﻿using GalaSoft.MvvmLight;
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
            this.Mode = mode;

            if (this.Mode == Mode.add)
            {
                PointOfInterest = new Model.Tables.PointOfInterest();
                PointOfInterest.Trip = trip;
                trip.PointsOfInterests.Add(PointOfInterest);
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
                else
                    UpdateExistingPOI();
            }
        }

        /// <summary>
        /// Ajouter le point
        /// </summary>
        public void AddPoiInDB()
        {
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