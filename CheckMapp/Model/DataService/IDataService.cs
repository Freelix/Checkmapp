using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckMapp.Model.Tables;

namespace CheckMapp.Model.DataService
{
    public interface IDataServiceCountry
    {
        Country SaveCountry(Country newCountry);
        IList<Country> LoadCountries();
        Country UpdateCountry(Country selectedCountry);
        Country DeleteCountry(Country selectedCountry);
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
        Trip SaveTrip(Trip newTrip);
        IList<Trip> LoadTrip();
        Trip UpdateTrip(Trip selectedTrip);
        Trip DeleteTrip(Trip selectedTrip);
    }
}
