using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using CheckMapp.Model.Utils;
using System.Runtime.Serialization;

namespace CheckMapp.Model.Tables
{

    [Table(Name = "Trip")]
    [DataContract(IsReference = true)] 
    public class Trip : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Members

        private int _id;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    NotifyPropertyChanging("Id");
                    _id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        private string _name;

        [Column]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    NotifyPropertyChanging("Name");
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private DateTime _beginDate;

        [Column]
        public DateTime BeginDate
        {
            get { return _beginDate; }
            set
            {
                if (!_beginDate.Equals(value))
                {
                    NotifyPropertyChanging("BeginDate");
                    _beginDate = value;
                    NotifyPropertyChanged("BeginDate");
                }
            }
        }

        private DateTime? _endDate;

        [Column]
        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                if (!_endDate.Equals(value))
                {
                    NotifyPropertyChanging("EndDate");
                    _endDate = value;
                    NotifyPropertyChanged("EndDate");
                }
            }
        }

        /* private TripLocalisation _departure;

         [Column]
         public TripLocalisation Departure
         {
             get { return _departure; }
             set
             {
                 if (_departure != value)
                 {
                     NotifyPropertyChanging("Departure");
                     _departure = value;
                     NotifyPropertyChanged("Departure");
                 }
             }
         }

         private TripLocalisation _destination;

         [Column]
         public TripLocalisation Destination
         {
             get { return _destination; }
             set
             {
                 if (_destination != value)
                 {
                     NotifyPropertyChanging("Destination");
                     _destination = value;
                     NotifyPropertyChanged("Destination");
                 }
             }
         }*/

        /*private EntitySet<Note> _notes;

        [Association(Storage = "_notes", OtherKey = "_tripId")]
        public EntitySet<Note> Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    NotifyPropertyChanging("Notes");
                    _notes.Assign(value);
                    NotifyPropertyChanged("Notes");
                }
            }
        }

        private EntitySet<Note> _pictures;

        [Association(Storage = "_pictures", OtherKey = "_tripId")]
        public EntitySet<Note> Pictures
        {
            get { return _pictures; }
            set
            {
                if (_pictures != value)
                {
                    NotifyPropertyChanging("Pictures");
                    _pictures.Assign(value);
                    NotifyPropertyChanged("Pictures");
                }
            }
        }

        private EntitySet<Note> _pointsOfInterests;

        [Association(Storage = "_pointsOfInterests", OtherKey = "_tripId")]
        public EntitySet<Note> PointsOfInterests
        {
            get { return _pointsOfInterests; }
            set
            {
                if (_pointsOfInterests != value)
                {
                    NotifyPropertyChanging("PointsOfInterests");
                    _pointsOfInterests.Assign(value);
                    NotifyPropertyChanged("PointsOfInterests");
                }
            }
        }*/

        #endregion


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
