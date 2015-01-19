using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CheckMapp.Model
{
    [Table(Name = "Country")]
    public class Country : INotifyPropertyChanged, INotifyPropertyChanging
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

        private string _code;

        [Column]
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    NotifyPropertyChanging("Code");
                    _code = value;
                    NotifyPropertyChanged("Code");
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

        private Language _language;

        [Column]
        public Language Language
        {
            get { return _language; }
            set
            {
                if (_language != value)
                {
                    NotifyPropertyChanging("Language");
                    _language = value;
                    NotifyPropertyChanged("Language");
                }
            }
        }

        private bool _visited;

        [Column]
        public bool Visited
        {
            get { return _visited; }
            set
            {
                if (_visited != value)
                {
                    NotifyPropertyChanging("Visited");
                    _visited = value;
                    NotifyPropertyChanged("Visited");
                }
            }
        }

        private int _nbrVisit;

        [Column]
        public int NbrVisit
        {
            get { return _nbrVisit; }
            set
            {
                if (_nbrVisit != value)
                {
                    NotifyPropertyChanging("NbrVisit");
                    _nbrVisit = value;
                    NotifyPropertyChanged("NbrVisit");
                }
            }
        }

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
