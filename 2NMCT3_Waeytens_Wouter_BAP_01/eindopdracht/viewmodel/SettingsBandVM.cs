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

        private ObservableCollection<Stage> _listStages;

        public ObservableCollection<Stage> ListStages
        {
            get { return _listStages; }
            set { _listStages = value; OnPropertyChanged("ListStages"); }
        }

        private String _newStageText;

        public String NewStageText
        {
            get { return _newStageText; }
            set { _newStageText = value; OnPropertyChanged("NewStageText"); }
        }
        
        

        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set { _selectedBand = value; OnPropertyChanged("SelectedBand"); }
        }

        #region ctor
        public SettingsBandVM()
        {
            _bandsList = Band.GetBands();
            ListStages = Stage.GetStages();

            //Text properties
            NewStageText = "New Stage";
        }
        #endregion

        #region Commands
        public ICommand AddStageCommand
        {
            get { return new RelayCommand<String>(addStageHandler); }
        }

        private void addStageHandler(string stage)
        {
            if (stage != "New Stage")
            {
                Stage.AddStage(stage);
                NewStageText = "New Stage";
                ListStages = Stage.GetStages();
            }
        }

        public ICommand EditStageCommand
        {
            get { return new RelayCommand<Stage>(editStageHandler); }
        }

        private void editStageHandler(Stage stage)
        {
            //Console.WriteLine(stage.Name);
            Stage.EditStage(stage);
        }
        #endregion
    }
}
