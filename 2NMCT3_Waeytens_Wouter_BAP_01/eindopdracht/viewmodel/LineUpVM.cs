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

        private string _StyleBandLineUp;

        public string StyleBandLineUp
        {
            get { return _StyleBandLineUp; }
            set { _StyleBandLineUp = value; OnPropertyChanged("StyleBandLineUp"); }
        }
        
   
        #endregion

        #region properties

        private Band _vorigeBand;

        public Band VorigeBand
        {
            get { return _vorigeBand; }
            set { _vorigeBand = value;}
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
            if (boolken)
            {
                VorigeBand = band;
                boolken = false;
            }
            if (VorigeBand == band)
            {
                StyleBandLineUp = "BandLineUp1";
                Console.WriteLine("zelfde band");
            }
            else
            {
                VorigeBand = band;
                StyleBandLineUp = "BandLineUp";
                Console.WriteLine("andere band");
            }
        }
        
        #endregion
    }
}
