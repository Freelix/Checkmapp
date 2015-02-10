﻿using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace CheckMapp.Model.Tables
{
    [Table(Name = "Note")]
    [DataContract(IsReference = true)] 
    public class Note : INotifyPropertyChanged, INotifyPropertyChanging
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

        private string _title;

        [Column]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    NotifyPropertyChanging("Title");
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private string _message;

        [Column]
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    NotifyPropertyChanging("Message");
                    _message = value;
                    NotifyPropertyChanged("Message");
                }
            }
        }

        private DateTime _date;

        [Column]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (!_date.Equals(value))
                {
                    NotifyPropertyChanging("Date");
                    _date = value;
                    NotifyPropertyChanged("Date");
                }
            }
        }

        [Column]
        private int _pointOfInterestId;
        private EntityRef<PointOfInterest> _pointOfInterest;
        [Association(Storage = "_pointOfInterest", ThisKey = "_pointOfInterestId", OtherKey = "Id", IsForeignKey = true)]
        public PointOfInterest PointOfInterest
        {
            get { return _pointOfInterest.Entity; }
            set
            {
                NotifyPropertyChanging("PointOfInterest");
                _pointOfInterest.Entity = value;

                if (value != null)
                {
                    _pointOfInterestId = value.Id;
                }

                NotifyPropertyChanging("PointOfInterest");
            }
        }

        [Column]
        private int _tripId;
        private EntityRef<Trip> _trip;
        [Association(Storage = "_trip", ThisKey = "_tripId", OtherKey = "Id", IsForeignKey = true)]
        public Trip trip
        {
            get { return _trip.Entity; }
            set
            {
                NotifyPropertyChanging("trip");
                _trip.Entity = value;

                if (value != null)
                {
                    _tripId = value.Id;
                }

                NotifyPropertyChanging("trip");
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

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