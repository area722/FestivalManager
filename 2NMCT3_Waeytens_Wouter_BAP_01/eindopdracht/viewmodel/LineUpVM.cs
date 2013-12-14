using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using eindopdracht.model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows.Media;

namespace eindopdracht.viewmodel
{
    class LineUpVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Line-Up"; }
        }

        #region properties for binding
        private ObservableCollection<Band> _lstBands;

        public ObservableCollection<Band> LstBands
        {
            get { return _lstBands; }
            set { _lstBands = value; OnPropertyChanged("LstBands"); }
        }

        private ObservableCollection<Stage> _LstPodiums;

        public ObservableCollection<Stage> LstPodiums
        {
            get { return _LstPodiums; }
            set { _LstPodiums = value; OnPropertyChanged("LstPodiums"); }
        }

        #endregion

        #region properties
        private Band _vorigeBand;

        public Band VorigeBand
        {
            get { return _vorigeBand; }
            set { _vorigeBand = value;}
        }

        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set { _selectedBand = value; }
        }
        
        #endregion

        #region ctor
        public LineUpVM()
        {
            LstBands = Band.GetBands();
            LstPodiums = Stage.GetStages();
        }
        #endregion

        #region commands
        public ICommand LineUpBandCommand
        {
            get { return new RelayCommand<Band>(LineUpBandHandler); }
        }

        Boolean boolken = true;
        private void LineUpBandHandler(Band band)
        {
            SelectedBand = band;
            if (boolken)
            {
                VorigeBand = band;
                boolken = false;
                //Console.WriteLine("zelfde band");
            }
            if (VorigeBand == band)
            {
                //Console.WriteLine("zelfde band");
            }
            else
            {
                VorigeBand = band;
                //Console.WriteLine("andere band");
            }
            Console.WriteLine(SelectedBand.Name);
        }
        #endregion
    }
}
