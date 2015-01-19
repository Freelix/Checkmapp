using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CheckMapp.Model
{
    public class DatabaseDataContext : DataContext
    {
        public DatabaseDataContext(string connectionString)
            : base(connectionString)
        { }

        public Table<Note> notes;
        public Table<PointOfInterest> pointsOfInterests;
    }
}
