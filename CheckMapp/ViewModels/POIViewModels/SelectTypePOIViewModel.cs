using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CheckMapp.ViewModels.POIViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SelectTypePOIViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the SelectTypePOIViewModel class.
        /// </summary>
        public SelectTypePOIViewModel()
        {
            POITypeList = new ObservableCollection<POIType>();
            foreach (POIType item in Enum.GetValues(typeof(POIType)))
            {
                POITypeList.Add(item);
            }
        }

        public ObservableCollection<POIType> POITypeList
        {
            get;
            set;
        }

        private POIType _selectedItem;

        public POIType SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }


    }
}