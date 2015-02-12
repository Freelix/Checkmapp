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
        public ListNoteViewModel(Trip trip)
        {
            this.Trip = trip;
        }

        public ListNoteViewModel(Trip trip, int poiId)
        {
            this.Trip = trip;
            this.PoiLoaded = poiId;
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


        public List<KeyedList<string, Note>> GroupedNotes
        {
            get
            {
                List<Note> noteList = null;
                if (PoiLoaded == null)
                    noteList = Trip.Notes.ToList();
                else
                    noteList = Trip.Notes.Where(x => (x.PointOfInterest != null) && (x.PointOfInterest.Id == PoiLoaded)).ToList();
                
                var groupedNotes =
                    from note in noteList
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

        public int? PoiLoaded
        {
            get;
            set;
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

        public void LoadNotesByPoiId(int poiId)
        {
            DataServiceNote dsNote = new DataServiceNote();
        }

        #endregion
    }
}