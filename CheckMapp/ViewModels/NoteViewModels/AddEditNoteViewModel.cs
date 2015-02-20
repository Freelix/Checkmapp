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
            this.Trip = trip;
            this.Mode = mode;

            if (this.Mode == Mode.add)
            {
                Note = new Note();
                Note.Date = DateTime.Now;
            }
            else
                Note = note;

            if(this.Trip.PointsOfInterests!=null)
                _poiList = new List<PointOfInterest>(this.Trip.PointsOfInterests);

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
        /// Ma note
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

        /// <summary>
        /// Le id de ma note
        /// </summary>
        public int NoteId
        {
            get { return Note.Id; }
            set
            {
                Note.Id = value;
                RaisePropertyChanged("NoteId");
            }
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

        public Trip Trip
        {
            get;
            set;
        }

        public string TripName
        {
            get { return Trip.Name; }
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

                if (Mode == Mode.add)
                    AddNoteInDB(Note);
                else if (Mode == Mode.edit)
                    UpdateExistingNote();
            }
        }

        private void AddNoteInDB(Note note)
        {
            DataServiceNote dsNote = new DataServiceNote();
            note.Trip = Trip;
            Trip.Notes.Add(note);
            dsNote.addNote(note);
        }

        private void UpdateExistingNote()
        {
            DataServiceNote dsNote = new DataServiceNote();
            dsNote.UpdateNote(Note);
        }

        #endregion
    }
}