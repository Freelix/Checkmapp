﻿using System.Collections.Generic;
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
            db = App.db;
        }

        public void addPoi(PointOfInterest poi)
        {
            db.pointsOfInterests.InsertOnSubmit(poi);
            db.SubmitChanges();
        }

        /// <summary>
        /// Load all the poi except "None"
        /// "None" is a quick fix to be able to add Notes without
        /// adding a blank poi in ListPOIView
        /// </summary>
        /// <returns></returns>
        public List<PointOfInterest> LoadPointOfInterests()
        {
            return db.pointsOfInterests.Where(x => x.Name != "None").ToList();
        }

        /// <summary>
        /// Load all the poi for including "None"
        /// "None" is a quick fix to be able to add Notes without
        /// adding a blank poi in ListPOIView
        /// </summary>
        /// <returns></returns>
        public List<PointOfInterest> LoadListBoxPointOfInterests()
        {
            return db.pointsOfInterests.ToList();
        }

        public PointOfInterest getDefaultPOI()
        {
            return db.pointsOfInterests.Where(x => x.Name.Equals("None")).First();
        }

        public PointOfInterest getPOIById(int id)
        {
            return db.pointsOfInterests.Where(x => x.Id == id).First();
        }
    }
}