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

        private ICommand _addNoteCommand;
        public ICommand AddNoteCommand
        {
            get
            {
                if (_addNoteCommand == null)
                {
                    _addNoteCommand = new RelayCommand(() => AddNote());
                }
                return _addNoteCommand;
            }

        }

        #region Properties

        public Mode Mode
        {
            get;
            set;
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

        public void AddNote()
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

        public void AddNoteInDB(Note note)
        {
            DataServiceNote dsNote = new DataServiceNote();
            dsNote.addNote(note);
        }

        #endregion
    }
}