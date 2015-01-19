﻿using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CheckMapp.Model
{
    [Table(Name = "PointOfInterest")]
    public class PointOfInterest : INotifyPropertyChanged, INotifyPropertyChanging
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

        private string _city;

        [Column]
        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    NotifyPropertyChanging("City");
                    _city = value;
                    NotifyPropertyChanged("City");
                }
            }
        }

        private double _longitude;

        [Column]
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                if (_longitude != value)
                {
                    NotifyPropertyChanging("Longitude");
                    _longitude = value;
                    NotifyPropertyChanged("Longitude");
                }
            }
        }

        private double _latitude;

        [Column]
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                if (_latitude != value)
                {
                    NotifyPropertyChanging("Latitude");
                    _latitude = value;
                    NotifyPropertyChanged("Latitude");
                }
            }
        }

        private EntitySet<Note> _notes;

        [Association(Storage = "_notes", OtherKey = "_pointOfInterestId")]
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

        [Association(Storage = "_pictures", OtherKey = "_pointOfInterestId")]
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