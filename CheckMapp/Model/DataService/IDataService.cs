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
        void UpdatePicture(Picture picture);
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
        void addTrip(Trip newTrip);
        Trip getTripById(int id);
        IQueryable<Trip> LoadTrip();
        void UpdateTrip(Trip selectedTrip);
        void DeleteTrip(Trip selectedTrip);
    }

    public interface IDataServicePoi
    {
        void addPoi(PointOfInterest poi);
        List<PointOfInterest> LoadPointOfInterests();
        List<PointOfInterest> LoadListBoxPointOfInterests();
        PointOfInterest getPOIById(int id);
        PointOfInterest getDefaultPOI();
    }
}
