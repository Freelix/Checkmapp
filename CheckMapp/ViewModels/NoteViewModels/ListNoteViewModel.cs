using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System;
using CheckMapp.Model.Tables;
using CheckMapp.Model.DataService;
using CheckMapp.KeyGroup;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace CheckMapp.ViewModels.NoteViewModels
{
    public class ListNoteViewModel : INotifyPropertyChanged
    {

        public ListNoteViewModel()
        {
            LoadAllNotesFromDatabase();
        }

        private List<Note> _noteList;
        public List<Note> NoteList
        {
            get { return _noteList; }
            set
            {
                _noteList = value;
                NotifyPropertyChanged("NoteList");
            }
        }

        public string TripName
        {
            get { return "Afrique 2014"; }
        }

        public List<KeyedList<string, Note>> GroupedNotes
        {
            get
            {
                var groupedNotes =
                    from note in NoteList
                    orderby note.Date
                    group note by note.Date.ToString("m") into notesByDay
                    select new KeyedList<string, Note>(notesByDay);

                return new List<KeyedList<string, Note>>(groupedNotes);
            }
        }

        private ICommand _deleteNoteCommand;
        public ICommand DeleteNoteCommand
        {
            get
            {
                if (_deleteNoteCommand == null)
                {
                    _deleteNoteCommand = new RelayCommand<Note>((note) => DeleteNote(note));
                }
                return _deleteNoteCommand;
            }

        }

        private ICommand _deleteNotesCommand;
        public ICommand DeleteNotesCommand
        {
            get
            {
                if (_deleteNotesCommand == null)
                {
                    _deleteNotesCommand = new RelayCommand<List<Note>>((noteList) => DeleteNotes(noteList));
                }
                return _deleteNotesCommand;
            }

        }


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

        public void DeleteNotes(List<Note> noteList)
        {
            DataServiceNote dsNote = new DataServiceNote();
            foreach (Note note in noteList)
            {
                dsNote.DeleteNote(note);
            }
        }

        public void DeleteNote(Note noteSelected)
        {
            DataServiceNote dsNote = new DataServiceNote();
            dsNote.DeleteNote(noteSelected);
        }

        public void LoadAllNotesFromDatabase()
        {
            DataServiceNote dsNote = new DataServiceNote();
            _noteList = dsNote.LoadNotes();
        }

        #endregion
    }
}