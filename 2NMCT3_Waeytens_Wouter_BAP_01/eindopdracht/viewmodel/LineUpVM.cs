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

        private ObservableCollection<DateTime> _listDays;

        public ObservableCollection<DateTime> ListDays
        {
            get { return _listDays; }
            set { _listDays = value; OnPropertyChanged("ListDays"); }
        }

        private ObservableCollection<int> _lstHours;

        public ObservableCollection<int> LstHours
        {
            get { return _lstHours; }
            set { _lstHours = value; OnPropertyChanged("LstHours"); }
        }
        
        #endregion

        #region properties
        private Festival _fest;

        public Festival Fest
        {
            get { return _fest; }
            set { _fest = value; }
        }      
        #endregion

        #region ctor
        public LineUpVM()
        {
            LstBands = Band.GetBands();
            LstPodiums = Stage.GetStages();
            ListDays = getListDays();
            LstHours = FillHours();
        }

        #endregion
        private ObservableCollection<DateTime> getListDays()
        {
            Fest = Festival.GetDates();
            ObservableCollection<DateTime> lst = new ObservableCollection<DateTime>();
            lst.Add(Fest.StartDate);
            TimeSpan lenght = Fest.EndDate - Fest.StartDate;
            for (int i = 1; i < lenght.Days; i++)
            {
                lst.Add(Fest.StartDate.AddDays(i));
            }
            lst.Add(Fest.EndDate);
            return lst;
        }

        private ObservableCollection<int> FillHours()
        {
            ObservableCollection<int> lst = new ObservableCollection<int>();
            for (int i = 1; i <= 24; i++)
            {
                lst.Add(i);
            }
            return lst;
        }
        #region methods


        #endregion

        #region commands
        public ICommand LineUpBandCommand
        {
            get { return new RelayCommand<Band>(LineUpBandHandler); }
        }

        private void LineUpBandHandler(Band obj)
        {

        }
        #endregion
    }
}
