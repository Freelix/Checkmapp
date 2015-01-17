using CheckMapp.Model;
using GalaSoft.MvvmLight;
using System;

namespace CheckMapp.ViewModels.NoteViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class NoteViewModel : ViewModelBase
    {
        Note _note;

        /// <summary>
        /// Initializes a new instance of the NoteViewModel class.
        /// </summary>
        public NoteViewModel()
        {
            Note note = new Note();
            note.Titre = "Au resto";
            note.Date = DateTime.Now;
            note.Message = "Note exemple il fait beau dehors ehe he";

            _note = note;
        }

        /// <summary>
        /// La note du voyage
        /// </summary>
        public Note Note
        {
            get { return _note; }
        }

        /// <summary>
        /// Si la map doit s'afficher (point d'intéret)
        /// </summary>
        public bool IsVisible
        {
            get
            {
                //note has point dintéret
                return false;
            }
        }

        public string TripName
        {
            get { return "Africa 2014"; }
        }
    }
}