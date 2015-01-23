using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System;
using CheckMapp.Model.Tables;

namespace CheckMapp.Model.DataService
{
    public class DataServicePoi : IDataServicePoi
    {
        private DatabaseDataContext db;

        public DataServicePoi()
        {
            db = new DatabaseDataContext(App.DBConnectionString);
        }

        public void addPoi(PointOfInterest poi)
        {
            db.pointsOfInterests.InsertOnSubmit(poi);
            db.SubmitChanges();
        }

        public IQueryable<PointOfInterest> LoadPointOfInterests()
        {
            return from PointOfInterest pointOfInterest in db.pointsOfInterests select pointOfInterest;
        }
    }
}
