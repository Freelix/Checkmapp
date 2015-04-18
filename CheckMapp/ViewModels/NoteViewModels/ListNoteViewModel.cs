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
using GalaSoft.MvvmLight;

namespace CheckMapp.ViewModels.NoteViewModels
{
    public class ListNoteViewModel : ViewModelBase
    {
        public ListNoteViewModel(int trip)
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            this.Trip = dsTrip.getTripById(trip);
        }

        public ListNoteViewModel(int trip, int poiId)
        {
            DataServiceTrip dsTrip = new DataServiceTrip();
            this.Trip = dsTrip.getTripById(trip);
            this.PoiLoaded = poiId;
        }

        /// <summary>
        /// Le voyage choisi
        /// </summary>
        public Trip Trip
        {
            get;
            set;
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
                    _deleteNotesCommand = new RelayCommand<List<object>>((noteList) => DeleteNotes(noteList));
                }
                return _deleteNotesCommand;
            }

        }

        /// <summary>
        /// Si les notes sont selon un poi
        /// </summary>
        public int? PoiLoaded
        {
            get;
            set;
        }


        #region DBMethods

        public void DeleteNotes(List<object> noteList)
        {
            DataServiceNote dsNote = new DataServiceNote();
            foreach (Note note in noteList)
            {
                Trip.Notes.Remove(note);
                dsNote.DeleteNote(note);
            }
        }

        public void DeleteNote(Note noteSelected)
        {
            DataServiceNote dsNote = new DataServiceNote();
            Trip.Notes.Remove(noteSelected);
            dsNote.DeleteNote(noteSelected);
        }

        #endregion
    }
}