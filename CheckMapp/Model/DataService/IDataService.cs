using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckMapp.Model
{
    public interface IDataServiceCountry
    {
        Country SaveCountry(Model.Country newCountry);
        IList<Model.Country> LoadCountries();
        Country UpdateCountry(Model.Country selectedCountry);
        Country DeleteCountry(Model.Country selectedCountry);
    }

    public interface IDataServiceNote
    {
        void addNote(Note newNote);
        Note getNoteById(int id);
        IQueryable<Note> LoadNote();
        Note UpdateNote(Note note);
        void DeleteNote(Note note);
    }

    public interface IDataServiceTrip
    {
        Trip SaveTrip(Model.Trip newTrip);
        IList<Model.Trip> LoadTrip();
        Trip UpdateTrip(Model.Trip selectedTrip);
        Trip DeleteTrip(Model.Trip selectedTrip);
    }
}
