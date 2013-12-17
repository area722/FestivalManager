﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using eindopdracht.model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows.Media;
using System.Windows;

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

        private Stage _selectedStage;

        public Stage SelectedStage
        {
            get { return _selectedStage; }
            set { _selectedStage = value; OnPropertyChanged("SelectedStage"); SelectionChangedDayStage(); }
        }
        

        private ObservableCollection<DateTime> _listDays;

        public ObservableCollection<DateTime> ListDays
        {
            get { return _listDays; }
            set { _listDays = value; OnPropertyChanged("ListDays"); }
        }

        private DateTime _selectedDay;

        public DateTime SelectedDay
        {
            get { return _selectedDay; }
            set { _selectedDay = value; OnPropertyChanged("SelectedDay"); SelectionChangedDayStage(); }
        }

        private ObservableCollection<LineUp> _listLineUp;

        public ObservableCollection<LineUp> ListLineUp
        {
            get { return _listLineUp; }
            set { _listLineUp = value; OnPropertyChanged("ListLineUp"); }
        }
        

        private ObservableCollection<int> _lstHours;

        public ObservableCollection<int> LstHours
        {
            get { return _lstHours; }
            set { _lstHours = value; OnPropertyChanged("LstHours"); }
        }

        private ObservableCollection<int> _lstMinutes;

        public ObservableCollection<int> LstMinutes
        {
            get { return _lstMinutes; }
            set { _lstMinutes = value; OnPropertyChanged("LstMinutes"); }
        }

        //Times
        private int _startHour;

        public int StartHour
        {
            get { return _startHour; }
            set { _startHour = value; SelectionChanged(StartHour, "StartHour"); OnPropertyChanged("StartHour"); }
        }

        private int _startMinites;

        public int StartMinites
        {
            get { return _startMinites; }
            set { _startMinites = value; SelectionChanged(StartMinites, "StartMinites"); OnPropertyChanged("StartMinites"); }
        }

        private int _endHours;

        public int EndHours
        {
            get { return _endHours; }
            set { _endHours = value; SelectionChanged(EndHours, "EndHours"); OnPropertyChanged("EndHours"); }
        }

        private int _endMinites;

        public int EndMinites
        {
            get { return _endMinites; }
            set { _endMinites = value; SelectionChanged(EndMinites, "EndMinites"); OnPropertyChanged("EndMinites"); }
        }

        private int _durationHours;

        public int DurationHours
        {
            get { return _durationHours; }
            set { _durationHours = value; SelectionChangedDur(); OnPropertyChanged("DurationHours"); }
        }

        private int _durationMinites;

        public int DurationMinites
        {
            get { return _durationMinites; }
            set { _durationMinites = value; SelectionChangedDur(); OnPropertyChanged("DurationMinites"); }
        }
        #endregion

        #region properties
        private Festival _fest;

        public Festival Fest
        {
            get { return _fest; }
            set { _fest = value; }
        }

        private ObservableCollection<DateTime> _lstTimes;

        public ObservableCollection<DateTime> LstTimes
        {
            get { return _lstTimes; }
            set { _lstTimes = value; }
        }
        #endregion

        #region ctor
        public LineUpVM()
        {
            LstBands = Band.GetBands();
            LstPodiums = Stage.GetStages();
            ListDays = getListDays();
            LstTimes = FillTimes();
            LstHours = new ObservableCollection<int>();
            LstMinutes = new ObservableCollection<int>();
            fillShowTimes();
            ListLineUp = new ObservableCollection<LineUp>();
        }

        #endregion
       
        #region methods
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

        private ObservableCollection<DateTime> FillTimes()
        {
            ObservableCollection<DateTime> lst = new ObservableCollection<DateTime>();
            DateTime time = Fest.StartDate;
            TimeSpan timeSpan = Fest.EndDate - Fest.StartDate;
            for (int i = 0; i < timeSpan.TotalMinutes; i+=10)
            {
                DateTime time2 = time.AddMinutes(i);
                lst.Add(time2);
            }
            return lst;
        }

        private void fillShowTimes()
        {
            for (int i = 0; i <= 23; i++)
            {
                LstHours.Add(i);
            }
            for (int y = 0; y <= 50; y+=10)
            {
                LstMinutes.Add(y);
            }
        }

        //times
        private Boolean boolken = true;
        private void SelectionChanged(int time,string what)
        {
            if (what == "EndHours" || what == "EndMinites" || what == "StartHour" || what == "StartMinites")
            {
                if (EndHours < StartHour)
                {
                    EndHours = StartHour;
                }

                //calculate duration
                boolken = false;
                DateTime EndTime = new DateTime(2013,1,1,EndHours,EndMinites,0);
                DateTime StartTime = new DateTime(2013,1,1,StartHour,StartMinites,0);
                TimeSpan durationTime = EndTime - StartTime;
                DurationHours = (int)durationTime.TotalHours;
                DurationMinites = (int)durationTime.TotalMinutes - (int)durationTime.TotalHours*60;
                boolken = true;
            }
        }

        private void SelectionChangedDur()
        {
            //calculate endtime
            if(boolken)
            {
                DateTime startTime = new DateTime(2013, 1, 1, StartHour, StartMinites, 0);
                DateTime endTime = startTime.AddHours(DurationHours).AddMinutes(DurationMinites);
                EndHours = endTime.Hour;
                EndMinites = endTime.Minute;
            }
        }

        private void SelectionChangedDayStage()
        {
            if (SelectedStage != null && SelectedDay != new DateTime(0001, 1, 1, 0, 0, 0))
            {
                LineUp lu = new LineUp()
                {
                    Date = SelectedDay,
                    Stage = SelectedStage
                };
                ListLineUp = LineUp.getLineupsByStageAndDay(lu);
            }
        }

        #endregion

        #region commands
        public ICommand LineUpBandCommand
        {
            get { return new RelayCommand<Band>(LineUpBandHandler); }
        }

        private void LineUpBandHandler(Band obj)
        {

        }

        public ICommand AddLineUpCommand
        {
            get { return new RelayCommand(AddLineUpHandler); }
        }

        private void AddLineUpHandler()
        {
            if (SelectedDay == new DateTime(0001,1,1,0,0,0) || SelectedStage == null)
            {
                MessageBox.Show("Selecteer een stage en dag");
            }
            else 
            {
                LineUp lineup = new LineUp()
                {
                    Date = SelectedDay,
                    From = new DateTime(SelectedDay.Year, SelectedDay.Month, SelectedDay.Day, StartHour, StartMinites, 0),
                    Until = new DateTime(SelectedDay.Year, SelectedDay.Month, SelectedDay.Day, EndHours, EndMinites, 0),
                    Stage = SelectedStage
                };
                LineUp.InsertLinup(lineup);
                ListLineUp = LineUp.getLineupsByStageAndDay(lineup);
            }
        }
        #endregion
    }
}
