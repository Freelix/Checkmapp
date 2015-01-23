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

namespace CheckMapp.ViewModels.NoteViewModels
{
    public class AddEditNoteViewModel : PhoneApplicationPage, INotifyPropertyChanged
    {


        public AddEditNoteViewModel(Mode mode)
        {
            this.Mode = mode;
        }

        public void showInfo(Note noteToModify)
        {
            NoteId = noteToModify.Id;
            NoteName = noteToModify.Title;
            NoteDate = DateTime.Now;
            //POI = noteToModify.PointOfInterest.Name;
            Message = noteToModify.Message;
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

        private string _poi;
        public string POI
        {
            get { return _poi; }
            set
            {
                _poi = value;
                NotifyPropertyChanged("POI");
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
                        Date = DateTime.Now
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

        public void AddNoteInDB(Note note)
        {
            DataServiceNote dsNote = new DataServiceNote();
            dsNote.addNote(note);
        }

        public void UpdateExistingNote()
        {
            DataServiceNote dsNote = new DataServiceNote();

            Note updatedNote = new Note();
            updatedNote.Title = _name;
            updatedNote.Message = _message;
            updatedNote.Id = _id;

            dsNote.UpdateNote(updatedNote);
        }

        #endregion
    }
}