using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System;
using CheckMapp.Model.Tables;
using CheckMapp.Resources;

namespace CheckMapp.Model.DataService
{
    public class DataServicePoi : IDataServicePoi
    {
        private DatabaseDataContext db;

        public DataServicePoi()
        {
            db = App.db;
        }

        public void addPoi(PointOfInterest poi)
        {
            db.pointsOfInterests.InsertOnSubmit(poi);
            db.SubmitChanges();
        }

        public PointOfInterest getPOIById(int id)
        {
            return db.pointsOfInterests.Where(x => x.Id == id).First();
        }

        public PointOfInterest getDefaultPOI()
        {
            PointOfInterest defaultPoi = db.pointsOfInterests.Where(x => x.Name.Equals(AppResources.DefaultPoi)).FirstOrDefault();

            if (defaultPoi != null)
                return defaultPoi;
            else
            {
                PointOfInterest poi = new PointOfInterest { Name = AppResources.DefaultPoi };
                addPoi(poi);
                return poi;
            }
        }

        public ObservableCollection<PointOfInterest> LoadPointOfInterestsFromTrip(Trip trip)
        {
            List<PointOfInterest> listPOI = db.pointsOfInterests.Where(x => x.Trip == trip &&
                    x.Name != AppResources.DefaultPoi).ToList();
            return new ObservableCollection<PointOfInterest>(listPOI);
        }

        public void DeletePoi(PointOfInterest poi)
        {
            var existing = db.pointsOfInterests.FirstOrDefault(x => x.Id == poi.Id);

            if (existing != null)
            {
                DataServiceCommon dsCommon = new DataServiceCommon();
                dsCommon.DeletePoiInCascade(poi);

                db.pointsOfInterests.DeleteOnSubmit(existing);
                db.SubmitChanges();
            }
        }

        public void DeleteDefaultPoiForATrip(Trip trip)
        {
            var existing = db.pointsOfInterests.Where(
                x => x.Name.Equals(AppResources.DefaultPoi) &&
                    x.Trip.Id == trip.Id).FirstOrDefault();

            if (existing != null)
            {
                db.pointsOfInterests.DeleteOnSubmit(existing);
                db.SubmitChanges();
            }
        }

        public void UpdatePoi(PointOfInterest poi)
        {
            PointOfInterest poiToUpdate = db.pointsOfInterests.First(x => x.Id == poi.Id);

            poiToUpdate.Location = poi.Location;
            poiToUpdate.Name = poi.Name;
            poiToUpdate.Latitude = poi.Latitude;
            poiToUpdate.Longitude = poi.Longitude;

            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to save a point of interest : " + e);
            }
        }
    }
}
