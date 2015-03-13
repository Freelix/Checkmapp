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
    public class NoteViewModel : ViewModelBase
    {
        private Note _note;

        public NoteViewModel(Note note)
        {
            this.Note = note;
        }

        #region Properties

        /// <summary>
        /// La Note actuelle
        /// </summary>
        public Note Note
        {
            get { return _note; }
            set
            {
                _note = value;
                RaisePropertyChanged("Note");
            }
        }

        /// <summary>
        /// Si le point d'intérêt s'affiche
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return (Note.PointOfInterest != null);
            }
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

        #region DBMethods

        /// <summary>
        /// Supprimer une note
        /// </summary>
        public void DeleteNote()
        {
            DataServiceNote dsNote = new DataServiceNote();
            dsNote.DeleteNote(Note);
        }

        #endregion
    }
}