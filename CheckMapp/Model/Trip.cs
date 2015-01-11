using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMapp.Model
{
    /// <summary>
    /// Un voyage
    /// </summary>
    [Table("Trip")]
    public class Trip : INotifyPropertyChanged
    {
        #region Members
        int _id;
        string _name;
        IList<Country> _country;
        DateTime _beginDate;
        DateTime? _endDate;
        bool _active;
        #endregion

        /// <summary>
        /// Construction de la classe voyage
        /// </summary>
        public Trip()
        {
        }

        #region Properties

        /// <summary>
        /// Clé unique du voyage
        /// </summary>
        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary>
        /// Nom du voyage
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Voyage terminé ou pas
        /// </summary>
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
        
        /// <summary>
        /// Le pays visité
        /// </summary>
        public IList<Country> Country
        {
            get { return _country; }
            set { _country = value; }
        }

        /// <summary>
        /// Date de début du voyage
        /// </summary>
        public DateTime BeginDate
        {
            get { return _beginDate; }
            set
            {
                if (value.Equals(_beginDate)) return;
                _beginDate = value;
                OnPropertyChanged("BeginDate");
            }
        }

        /// <summary>
        /// Date de fin du voyage
        /// </summary>
        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                if (value.Equals(_endDate)) return;
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }


        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
