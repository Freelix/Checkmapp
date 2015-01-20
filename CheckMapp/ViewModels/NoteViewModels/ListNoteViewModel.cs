using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System;
using CheckMapp.Model.Tables;
using CheckMapp.Model.DataService;
using CheckMapp.KeyGroup;

namespace CheckMapp.ViewModels.NoteViewModels
{
    public class ListNoteViewModel : INotifyPropertyChanged
    {
        public ListNoteViewModel()
        {
            LoadAllNotesFromDatabase();
        }

        private ObservableCollection<Note> _noteList;
        public ObservableCollection<Note> NoteList
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

        public void LoadAllNotesFromDatabase()
        {
            DataServiceNote dsNote = new DataServiceNote();
            var allNotesInDB = dsNote.LoadNote();

            NoteList = new ObservableCollection<Note>(allNotesInDB);
        }

        #endregion
    }
}