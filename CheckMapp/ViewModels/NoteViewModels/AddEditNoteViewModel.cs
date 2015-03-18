using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.ComponentModel;
using CheckMapp.Model.Tables;
using CheckMapp.Model.DataService;
using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Utility = CheckMapp.Utils.Utility;
using FluentValidation;
using CheckMapp.Utils.Validations;

namespace CheckMapp.ViewModels.NoteViewModels
{
    public class AddEditNoteViewModel : ViewModelBase
    {
        private Note _note;

        // Used for validate the form
        private IValidator<Note> _validator;

        public AddEditNoteViewModel(Trip trip, Note note, Mode mode)
        {
            this.Mode = mode;

            if (this.Mode == Mode.add)
            {
                Note = new Note();
                Note.Trip = trip;
                trip.Notes.Add(Note);
                Note.Date = DateTime.Now;
            }
            else
                Note = note;

            if (trip.PointsOfInterests != null)
            {
                _poiList = new List<PointOfInterest>(trip.PointsOfInterests);
            }

            InitialiseValidator();
        }

        private void InitialiseValidator()
        {
            _validator = new ValidatorFactory().GetValidator<Note>();
        }

        private ICommand _addEditNoteCommand;
        public ICommand AddEditNoteCommand
        {
            get
            {
                if (_addEditNoteCommand == null)
                {
                    _addEditNoteCommand = new RelayCommand(() => AddEditNote());
                }
                return _addEditNoteCommand;
            }

        }

        private ICommand _cancelNoteCommand;
        public ICommand CancelNoteCommand
        {
            get
            {
                if (_cancelNoteCommand == null)
                {
                    _cancelNoteCommand = new RelayCommand(() => CancelNote());
                }
                return _cancelNoteCommand;
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

        /// <summary>
        /// La note
        /// </summary>
        public Note Note
        {
            get { return _note; }
            set
            {
                _note = value;
            }
        }
        public Mode Mode
        {
            get;
            set;
        }

        private bool _noneCheck;
        /// <summary>
        /// Aucun poi sélectionné
        /// </summary>
        public bool NoneCheck
        {
            get { return _noneCheck; }
            set { _noneCheck = value; }
        }

        /// <summary>
        /// Le titre de ma note
        /// </summary>
        public string NoteName
        {
            get { return Note.Title; }
            set
            {
                Note.Title = value;
                RaisePropertyChanged("NoteName");
            }
        }

        private List<PointOfInterest> _poiList;
        /// <summary>
        /// Liste des points d'intérêts
        /// </summary>
        public List<PointOfInterest> PoiList
        {
            get { return _poiList; }
            set
            {
                _poiList = value;
                RaisePropertyChanged("PoiList");
            }
        }

        /// <summary>
        /// Le point d'intérêt
        /// </summary>
        public PointOfInterest POISelected
        {
            get { return Note.PointOfInterest; }
            set
            {
                Note.PointOfInterest = value;
                RaisePropertyChanged("POISelected");
            }
        }

        /// <summary>
        /// Le contenu de ma note
        /// </summary>
        public string Message
        {
            get { return Note.Message; }
            set
            {
                Note.Message = value;
                RaisePropertyChanged("Message");
            }
        }

        /// <summary>
        /// La date de ma note
        /// </summary>
        public DateTime NoteDate
        {
            get { return Note.Date; }
            set
            {
                Note.Date = value;
                RaisePropertyChanged("NoteDate");
            }
        }

        #endregion

        #region DBMethods

        /// <summary>
        /// Ajouter ou modifier une note
        /// </summary>
        public void AddEditNote()
        {
            // If the form is not valid, a notification will appear
            if (ValidationErrorsHandler.IsValid(_validator, Note))
            {
                _isFormValid = true;

                if (NoneCheck)
                    Note.PointOfInterest = null;

                if (Mode == Mode.add)
                    AddNoteInDB();
                else if (Mode == Mode.edit)
                    UpdateExistingNote();
            }
        }

        public void CancelNote()
        {
            if (Mode == ViewModels.Mode.add)
            {
                Note.Trip.Notes.Remove(Note);
                Note.Trip = null;
            }
        }

        private void AddNoteInDB()
        {
            DataServiceNote dsNote = new DataServiceNote();
            dsNote.addNote(Note);
        }

        private void UpdateExistingNote()
        {
            DataServiceNote dsNote = new DataServiceNote();
            dsNote.UpdateNote(Note);
        }

        #endregion
    }
}