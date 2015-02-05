﻿using System;
using CheckMapp.Model.Tables;
using System.Linq;
using System.Collections.Generic;

namespace CheckMapp.Model.DataService
{
    public class DataServiceTrip : IDataServiceTrip
    {
    private DatabaseDataContext db;

        public DataServiceTrip()
        {
            db = App.db;
        }

        public void addTrip(Trip newTrip)
        {
            db.trips.InsertOnSubmit(newTrip);
            db.SubmitChanges();
        }

        public Trip getTripById(int id)
        {
            return db.trips.Where(x => x.Id == id).First();
        }

        public List<Trip> LoadTrip()
        {
            return db.trips.ToList();
        }

        public void UpdateTrip(Trip trip)
        {
            Trip tripToUpdate = db.trips.Where(x => x.Id == trip.Id).First();
           
            tripToUpdate.Name = trip.Name;
            tripToUpdate.BeginDate = trip.BeginDate;
            //tripToUpdate.Departure = trip.Departure;
            //tripToUpdate.Destination = trip.Destination;
            tripToUpdate.EndDate = trip.EndDate;

            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to save a trip" + e);
            }
        }

        public void DeleteTrip(Trip trip)
        {
            var existing = db.trips.Single(x => x.Id == trip.Id);

            if (existing != null)
            {
                db.trips.DeleteOnSubmit(existing);
                db.SubmitChanges();
            }
        }
    }
}