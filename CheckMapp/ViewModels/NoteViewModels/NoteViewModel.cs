using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CheckMapp.Model;
using System;
using System.Linq;
using System.ComponentModel;
using System.Windows.Input;
using CheckMapp.Model.Tables;
using CheckMapp.Model.DataService;

namespace CheckMapp.ViewModels.NoteViewModels
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        private Note _note;

        public NoteViewModel(Note note)
        {
            this.Note = note;
        }

        #region Properties

        public Note Note
        {
            get { return _note; }
            set
            {
                _note = value;
                NotifyPropertyChanged("Note");
            }
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
            get { return Note.trip.Name; }
        }

        #endregion

        #region Buttons

        private ICommand _deleteNoteCommand;
        public ICommand DeleteNoteCommand
        {
            get
            {
                if (_deleteNoteCommand == null)
                {
                    _deleteNoteCommand = new RelayCommand(() => DeleteNote());
                }
                return _deleteNoteCommand;
            }

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

        public void DeleteNote()
        {
            DataServiceNote dsNote = new DataServiceNote();
            dsNote.DeleteNote(Note);
        }

        #endregion
    }
}