using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Windows.UI;

namespace CheckMapp.Model
{

    /// <summary>
    /// Classe des pays
    /// </summary>
    [Table("Country")]
    public class Country : INotifyPropertyChanged
    {
        #region Members
        int _id;
        string _code;
        string _name;
        string _language;
        bool _visited;
        int _nbrVisit;
        string _colorHex;
        #endregion

        #region Properties

        /// <summary>
        /// L'identifiant du pays
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
        /// Le code du pays
        /// </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                if (value == _code) return;
                _code = value;
                OnPropertyChanged("Code");
            }
        }

        /// <summary>
        /// Le nom du pays
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// La langue du pays
        /// </summary>
        public string Language
        {
            get { return _language; }
            set
            {
                if (value == _language) return;
                _language = value;
                OnPropertyChanged("Language");
            }
        }

        /// <summary>
        /// Si le pays a été visité
        /// </summary>
        public bool Visited
        {
            get { return _visited; }
            set
            {
                if (value == _visited) return;
                _visited = value;
                OnPropertyChanged("Visited");
            }
        }

        /// <summary>
        /// Nombre de visites
        /// </summary>
        public int NbrVisit
        {
            get { return _nbrVisit; }
            set
            {
                if (value == _nbrVisit) return;
                _nbrVisit = value;
                OnPropertyChanged("NbrVisit");
            }
        }

        /// <summary>
        /// Couleur associé au pays (quand check)
        /// </summary>
        public string ColorHex
        {
            get { return _colorHex; }
            set
            {
                if (value.Equals(_colorHex)) return;
                _colorHex = value;
                OnPropertyChanged("Color");
            }
        }

        [Ignore]
        public SolidColorBrush Color
        {
            get
            {
                System.Windows.Media.Color myColor = System.Windows.Media.Color.FromArgb(
                    Convert.ToByte(ColorHex.Substring(1, 2), 16),
                    Convert.ToByte(ColorHex.Substring(3, 2), 16),
                    Convert.ToByte(ColorHex.Substring(5, 2), 16), 0);

                return new SolidColorBrush(myColor);
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
