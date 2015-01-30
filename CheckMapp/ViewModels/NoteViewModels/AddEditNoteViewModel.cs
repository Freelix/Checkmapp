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

namespace CheckMapp.ViewModels.NoteViewModels
{
    public class AddEditNoteViewModel : PhoneApplicationPage, INotifyPropertyChanged
    {
        private Note _note;

        public AddEditNoteViewModel(Note note, Mode mode)
        {
            this.Mode = mode;

            if (this.Mode == Mode.add)
            {
                Note = new Note();
                Note.Date = DateTime.Now;
            }
            else
                Note = note;

            LoadAllPOIFromDatabase();
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
                NotifyPropertyChanged("NoteId");
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
                NotifyPropertyChanged("NoteName");
            }
        }

        private List<PointOfInterest> _poiList;
        public List<PointOfInterest> PoiList
        {
            get { return _poiList; }
            set
            {
                _poiList = value;
                NotifyPropertyChanged("PoiList");
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
                NotifyPropertyChanged("POISelected");
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
                NotifyPropertyChanged("Message");
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
                NotifyPropertyChanged("NoteDate");
            }
        }

        public string TripName
        {
            get { return "Africa 2014"; }
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
        /// Ajouter ou modifier une note
        /// </summary>
        public void AddEditNote()
        {
            // Adding a note

            if (!string.IsNullOrWhiteSpace(NoteName) && !string.IsNullOrWhiteSpace(Message))
            {
                if (Mode == Mode.add)
                    AddNoteInDB(Note);
                else if (Mode == Mode.edit)
                    UpdateExistingNote();
            }
            else
            {
                // Show an appropriate message
            }
        }


        private void AddNoteInDB(Note note)
        {
            DataServiceNote dsNote = new DataServiceNote();
            dsNote.addNote(note);
        }

        private void UpdateExistingNote()
        {
            DataServiceNote dsNote = new DataServiceNote();
            dsNote.UpdateNote(Note);
        }

        private void LoadAllPOIFromDatabase()
        {
            DataServicePoi dsPoi = new DataServicePoi();
            _poiList = dsPoi.LoadListBoxPointOfInterests();
            POISelected = dsPoi.getDefaultPOI();
        }


        #endregion
    }
}