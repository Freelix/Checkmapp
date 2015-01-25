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
        public AddEditNoteViewModel(Mode mode)
        {
            this.Mode = mode;
            LoadAllPOIFromDatabase();
        }

        /// <summary>
        /// Show info on when updating
        /// </summary>
        /// <param name="noteToModify"></param>
        public void showInfo(Note noteToModify)
        {
            _id = noteToModify.Id;
            _name = noteToModify.Title;
            _noteDate = DateTime.Now;
            _message = noteToModify.Message;

            if (noteToModify.PointOfInterest != null)
            {
                _poiId = noteToModify.PointOfInterest.Id.ToString();
                _poiSelected = getPOIById(noteToModify.PointOfInterest.Id);
            }
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

        public Mode Mode
        {
            get;
            set;
        }

        private int _id;
        public int NoteId
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("NoteId");
            }
        }

        private string _name;
        public string NoteName
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("NoteName");
            }
        }

        private string _poiId;
        public string POIID
        {
            get { return _poiId; }
            set
            {
                _poiId = value;
                NotifyPropertyChanged("POIID");
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

        private PointOfInterest _poiSelected;
        public PointOfInterest POISelected
        {
            get { return _poiSelected; }
            set
            {
                _poiSelected = value;
                NotifyPropertyChanged("POISelected");
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }

        private DateTime _noteDate;
        public DateTime NoteDate
        {
            get { return _noteDate; }
            set
            {
                _noteDate = value;
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

        public void AddEditNote()
        {
            // Adding a note
            if (Mode == Mode.add)
            {
                if (!string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_message))
                {
                    Note newNote = new Note
                    {
                        Title = _name,
                        Message = _message,
                        Date = DateTime.Now,
                        PointOfInterest = RetrievePOI()
                    };

                    AddNoteInDB(newNote);

                    (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
                }
                else
                {
                    // Show an appropriate message
                }
            }
            else if (Mode == Mode.edit)
            {
                // Edit a note
                if (!string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_message))
                {
                    UpdateExistingNote();

                    (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
                }
                else
                {
                    // Show an appropriate message
                }
            }
        }

        private PointOfInterest RetrievePOI()
        {
            PointOfInterest p = new PointOfInterest();

            // If the point of interest is set
            if (!string.IsNullOrWhiteSpace(_poiId))
            {
                int id = Utility.StringToNumber(_poiId);

                if (id > -1)
                    p = getPOIById(id);
            }

            return p;
        }

        private void AddNoteInDB(Note note)
        {
            DataServiceNote dsNote = new DataServiceNote();
            dsNote.addNote(note);
        }

        private void UpdateExistingNote()
        {
            DataServiceNote dsNote = new DataServiceNote();

            Note updatedNote = new Note();
            updatedNote.Title = _name;
            updatedNote.Message = _message;
            updatedNote.Id = _id;
            updatedNote.PointOfInterest = RetrievePOI();

            dsNote.UpdateNote(updatedNote);
        }

        private void LoadAllPOIFromDatabase()
        {
            DataServicePoi dsPoi = new DataServicePoi();
            PoiList = dsPoi.LoadListBoxPointOfInterests();
        }

        private PointOfInterest getPOIById(int id)
        {
            DataServicePoi dsPoi = new DataServicePoi();
            return dsPoi.getPOIById(id);
        }

        #endregion
    }
}