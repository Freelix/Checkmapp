using CheckMapp.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

namespace CheckMapp.ViewModels.NoteViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ListNoteViewModel : ViewModelBase
    {

        public List<Note> _noteList;

        /// <summary>
        /// Initializes a new instance of the ListNoteViewModel class.
        /// </summary>
        public ListNoteViewModel()
        {
            Note note = new Note();
            note.Titre = "Au resto";
            note.Date = DateTime.Now;
            note.Message = "Note exemple il fait beau dehors ehe he";

            Note note2 = new Note();
            note2.Titre = "Au resto";
            note2.Date = DateTime.Now;
            note2.Message = "Note exemple il fait beau dehors ehe he";

            _noteList.Add(note);
            _noteList.Add(note2);

        }


        /// <summary>
        /// Liste de mes notes
        /// </summary>
        public List<Note> NoteList
        {
            get { return _noteList; }
        }

        /// <summary>
        /// Nom du voyage
        /// </summary>
        public string TripName
        {
            get { return "Afrique 2014"; }
        }


    }
}