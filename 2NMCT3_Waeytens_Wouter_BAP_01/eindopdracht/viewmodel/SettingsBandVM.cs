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

        #region properties for binding
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

        private String _newGenreText;

        public String NewGenreText
        {
            get { return _newGenreText; }
            set { _newGenreText = value; OnPropertyChanged("NewGenreText"); }
        }
        

        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set { _selectedBand = value; OnPropertyChanged("SelectedBand"); selectionChanged(); Console.WriteLine("selectedBand: "+SelectedBand); }
        }

        private ObservableCollection<Genre> _listGenres;

        public ObservableCollection<Genre> ListGenres
        {
            get { return _listGenres; }
            set { _listGenres = value; OnPropertyChanged("ListGenres"); }
        }

        private String _newBandVisible;

        public String newBandVisible
        {
            get { return _newBandVisible; }
            set { _newBandVisible = value; OnPropertyChanged("newBandVisible"); }
        }

        private String _saveButtonText;

        public String SaveButtonText
        {
            get { return _saveButtonText; }
            set { _saveButtonText = value; OnPropertyChanged("SaveButtonText"); }
        }
          
        #endregion

        #region ctor
        public SettingsBandVM()
        {
            _bandsList = Band.GetBands();
            ListStages = Stage.GetStages();
            ListGenres = Genre.GetGenres();

            //Text properties
            NewStageText = "New Stage";
            NewGenreText = "New Genre";
            SaveButtonText = "Add";

            //Visible new button
            newBandVisible = "Hidden";
        }
        #endregion

        #region Commands
        //stages
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
            Stage.EditStage(stage);
        }

        //genres
        public ICommand AddGenreCommand
        {
            get { return new RelayCommand<String>(addGenreHandler); }
        }

        private void addGenreHandler(string genre)
        {
            if (genre != "New Genre")
            {
                Genre.AddGenre(genre);
                NewGenreText = "New Genre";
                ListGenres = Genre.GetGenres();
            }
        }

        public ICommand EditGenreCommand
        {
            get { return new RelayCommand<Genre>(EditGenreHandler); }
        }

        private void EditGenreHandler(Genre genre)
        {
            Genre.EditGenre(genre);
        }

        //Bands
        public ICommand NewBandCommand
        {
            get { return new RelayCommand(newBandHandler); }
        }

        private void newBandHandler()
        {
            //clear all fields
            SelectedBand = new Band();
            //hide button
            newBandVisible = "Hidden";
            SaveButtonText = "Add";
            Console.WriteLine("newbandvisible: " + newBandVisible);
        }

        private void selectionChanged()
        {
            newBandVisible = "Visible";
            SaveButtonText = "Edit";
        }
        #endregion
    }
}
