using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using eindopdracht.model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace eindopdracht.viewmodel
{
    class LineUpVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Line-Up"; }
        }

        private ObservableCollection<Band> _lstBands;

        public ObservableCollection<Band> LstBands
        {
            get { return _lstBands; }
            set { _lstBands = value; OnPropertyChanged("LstBands"); }
        }

        public LineUpVM()
        {
            LstBands = Band.GetBands();
        }

        #region commands
        public ICommand LineUpBandCommand
        {
            get { return new RelayCommand<Band>(LineUpBandHandler); }
        }

        private void LineUpBandHandler(Band band)
        {
            Console.WriteLine(band.Name);
        }
        #endregion
    }
}
