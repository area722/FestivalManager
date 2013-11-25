using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using eindopdracht.model;

namespace eindopdracht.viewmodel
{
    class SettingsBandVM:ObservableObject, IPage
    {
        public string Name
        {
            get { return "Bands"; }
        }

        private ObservableCollection<Band> _bandsList;

        public ObservableCollection<Band> BandsList
        {
            get { return _bandsList; }
            set { _bandsList = value; OnPropertyChanged("BandList"); }
        }

        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set { _selectedBand = value; OnPropertyChanged("SelectedBand"); }
        }
        

        public SettingsBandVM()
        {
            _bandsList = Band.GetBands();
        }
    }
}
