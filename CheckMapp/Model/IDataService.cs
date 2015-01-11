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
        Note SaveNote(Model.Note newNote);
        IList<Model.Note> LoadNote();
        Note UpdateNote(Model.Note selectedNote);
        Note DeleteNote(Model.Note selectedNote);
    }

    public interface IDataServiceTrip
    {
        Trip SaveTrip(Model.Trip newTrip);
        IList<Model.Trip> LoadTrip();
        Trip UpdateTrip(Model.Trip selectedTrip);
        Trip DeleteTrip(Model.Trip selectedTrip);
    }
}
