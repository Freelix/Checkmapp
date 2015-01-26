using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckMapp.Model.Tables;

namespace CheckMapp.Model.DataService
{
    public interface IDataServiceCountry
    {
        
    }

    public interface IDataServicePicture
    {
        void addPicture(Picture newPicture);
        List<Picture> LoadPictures();
        Picture getPictureById(int id);
        void DeletePicture(Picture picture);
    }

    public interface IDataServiceNote
    {
        void addNote(Note newNote);
        Note getNoteById(int id);
        List<Note> LoadNotes();
        void UpdateNote(Note note);
        void DeleteNote(Note note);
    }

    public interface IDataServiceTrip
    {
        Trip SaveTrip(Trip newTrip);
        IList<Trip> LoadTrip();
        Trip UpdateTrip(Trip selectedTrip);
        Trip DeleteTrip(Trip selectedTrip);
    }

    public interface IDataServicePoi
    {
        void addPoi(PointOfInterest poi);
        List<PointOfInterest> LoadPointOfInterests();
        PointOfInterest getPOIById(int id);
    }
}
