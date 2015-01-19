using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckMapp.Model.Tables;

namespace CheckMapp.Model.Utils
{
    public class LocalisationMap
    {
        #region Members
        int _id;
        Trip _trip;
        double _longitude;
        double _latitude;
        #endregion

        public LocalisationMap()
        {

        }

        #region Properties

        /// <summary>
        /// Identifiant unique de la position
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Le voyage associé à la position
        /// </summary>
        public Trip Trip
        {
            get { return _trip; }
            set { _trip = value; }
        }

        /// <summary>
        /// La longitude de la position
        /// </summary>
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        /// <summary>
        /// La latitude de la position
        /// </summary>
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        #endregion
    }
}
