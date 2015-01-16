using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace CheckMapp.ViewModels.POIViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ListPOIViewModel : ViewModelBase
    {
        public ObservableCollection<POI> POIList = new ObservableCollection<POI>();
        public class POI
        {
            public string nom;
            public string ville;
            public DateTime date;
            public String Nom
            {
                get { return nom; }
            }
            public String Ville
            {
                get { return ville; }
            }
            public DateTime Date
            {
                get { return date; }
            }

        }
        /// <summary>
        /// Initializes a new instance of the POIViewModel class.
        /// </summary>
        public ListPOIViewModel()
        {
            POI poi1 = new POI();
            poi1.date = DateTime.Now.AddDays(-2);
            poi1.nom = "Tour effeil";
            poi1.ville = "Paris";

            POI poi2 = new POI();
            poi2.date = DateTime.Now;
            poi2.nom = "Louvre";
            poi2.ville = "Paris";

            POIList.Add(poi1);
            POIList.Add(poi2);
        }
    }
}