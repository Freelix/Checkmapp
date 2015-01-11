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
    /// Statut relatif au pays
    /// </summary>
    [Table("Note")]
    public class Note : INotifyPropertyChanged
    {
        #region Members
        int _id;
        Country _country;
        DateTime _date;
        string _message;
        string _place;
        #endregion

        #region Properties

        /// <summary>
        /// Clé unique d'un note
        /// </summary>
        [PrimaryKey, AutoIncrement]
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
        /// Le pays visité
        /// </summary>
        public Country Country
        {
            get { return _country; }
            set { _country = value; }
        }

        /// <summary>
        /// Date de publication
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value.Equals(_date)) return;
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        /// <summary>
        /// Le message du statut
        /// </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                if (value == _message) return;
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        /// <summary>
        /// Endroit dans le pays de la visite
        /// </summary>
        public string Place
        {
            get { return _place; }
            set
            {
                if (value == _place) return;
                _place = value;
                OnPropertyChanged("Place");
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