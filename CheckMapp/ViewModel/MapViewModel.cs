using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMapp.ViewModel
{
    public class MapViewModel
    {

        public MapViewModel()
        {
            countryList = new List<string>();
            countryList.Add("Canada");
            countryList.Add("United States");
        }


        public List<String> countryList = new List<String>();
        public List<String> CountryList
        {
            get
            {
                return countryList;
            }
        }
    }
}
