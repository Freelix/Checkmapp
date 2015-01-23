using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System;
using CheckMapp.Model.Tables;

namespace CheckMapp.Model.DataService
{
    public class DataServiceNote : IDataServiceNote
    {
        private DatabaseDataContext db;

        public DataServiceNote()
        {
            db = new DatabaseDataContext(App.DBConnectionString);
        }

        public void addNote(Note newNote)
        {
            db.notes.InsertOnSubmit(newNote);
            db.SubmitChanges();
        }

        public Note getNoteById(int id)
        {
            return db.notes.Where(x => x.Id == id).First();
        }

        public IQueryable<Note> LoadNotes()
        {
            return from Note note in db.notes select note;
        }

        public void UpdateNote(Note note)
        {
            Note noteToUpdate = db.notes.Where(x => x.Id == note.Id).First();

            noteToUpdate.Date = DateTime.Now;
            noteToUpdate.Message = note.Message;
            //query.PointOfInterest = note.PointOfInterest;
            noteToUpdate.Title = note.Title;

            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to save a note" + e);
            }
        }

        public void DeleteNote(Note note)
        {
            var existing = db.notes.Single(x => x.Id == note.Id);

            if (existing != null)
            {
                db.notes.DeleteOnSubmit(existing);
                db.SubmitChanges();
            }
        }
    }
}