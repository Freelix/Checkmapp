using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System;
using CheckMapp.Model.Tables;

namespace CheckMapp.Model.DataService
{
    public class DataServicePicture : IDataServicePicture
    {
        private DatabaseDataContext db;

        public DataServicePicture()
        {
            db = App.db;
        }

        public void addPicture(Picture newPicture)
        {
            db.pictures.InsertOnSubmit(newPicture);
            db.SubmitChanges();
        }

        public List<Picture> LoadPictures()
        {
            return db.pictures.ToList();
        }

        public Picture getPictureById(int id)
        {
            return db.pictures.Where(x => x.Id == id).First();
        }

        public void DeletePicture(Picture picture)
        {
            var existing = db.pictures.Single(x => x.Id == picture.Id);

            if (existing != null)
            {
                db.pictures.DeleteOnSubmit(existing);
                db.SubmitChanges();
            }
        }
    }
}
