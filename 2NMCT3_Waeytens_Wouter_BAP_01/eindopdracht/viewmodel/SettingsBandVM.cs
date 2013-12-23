﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using eindopdracht.model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.IO;
using System.Windows;

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
            set { _bandsList = value; OnPropertyChanged("BandsList"); }
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
            set { _selectedBand = value; OnPropertyChanged("SelectedBand"); selectionChanged(); }
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

        private Byte[] _photo;

        public Byte[] Photo
        {
            get {return _photo; }
            set { _photo = value; OnPropertyChanged("Photo"); }
        }

        private Festival _dates;

        public Festival Dates
        {
            get { return _dates; }
            set { _dates = value; OnPropertyChanged("Dates"); }
        }

        private Genre _selectedAddGenreToBand;

        public Genre SelectedAddGenreToBand
        {
            get { return _selectedAddGenreToBand; }
            set { _selectedAddGenreToBand = value; OnPropertyChanged("SelectedAddGenreToBand"); }
        }

        private String _festName;

        public String FestName
        {
            get { return _festName; }
            set { _festName = value; OnPropertyChanged("FestName"); }
        }
        #endregion

        #region ctor
        public SettingsBandVM()
        {
            _bandsList = Band.GetBands();
            ListStages = Stage.GetStages();
            ListGenres = Genre.GetGenres();
            SelectedBand = new Band();

            //Text properties
            NewStageText = "New Stage";
            NewGenreText = "New Genre";
            SaveButtonText = "Add";

            //Visible new button
            newBandVisible = "Hidden";

            //dates
            Dates = Festival.GetDates();
            if (Dates.StartDate == Convert.ToDateTime("1/01/0001 0:00:00") || Dates.EndDate == Convert.ToDateTime("1/01/0001 0:00:00"))
            {
                Dates.StartDate = DateTime.Today;
                Dates.EndDate = DateTime.Today.AddDays(1);
            }

            //festname
            FestName = Festival.GetFestName();
        }
        #endregion

        #region Commands

        #region stages
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

        public ICommand DeleteStage
        {
            get { return new RelayCommand<Stage>(deleteStageHandler); }
        }

        private void deleteStageHandler(Stage stage)
        {
            Stage.DeleteStage(stage);
            ListStages = Stage.GetStages();
        }
        #endregion

        #region genres
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
        #endregion

        #region bands
        //Bands
        public ICommand NewBandCommand
        {
            get { return new RelayCommand(newBandHandler); }
        }

        private void newBandHandler()
        {
            //clear all fields
            SelectedBand = null;
            SelectedBand = new Band();
            //hide button
            newBandVisible = "Hidden";
            SaveButtonText = "Add";
        }

        private void selectionChanged()
        {
            newBandVisible = "Visible";
            SaveButtonText = "Edit";
            if (SelectedBand != null)
            {
                Photo = SelectedBand.Photo;
            } 
        }

        public ICommand savaBandCommand
        {
            get { return new RelayCommand(saveBandHandler); }
        }

        private void saveBandHandler()
        {
            if (SaveButtonText == "Add")
            {
                if (Photo == null)
                {
                    //zwartVak
                    System.Drawing.Image black = System.Drawing.Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "black.jpg");
                    MemoryStream me = new MemoryStream();
                    black.Save(me, System.Drawing.Imaging.ImageFormat.Jpeg);
                    SelectedBand.Photo = me.ToArray();
                    Photo = SelectedBand.Photo;
                }
                else 
                {
                    SelectedBand.Photo = Photo;
                }
                Band.AddBand(SelectedBand);
                BandsList = Band.GetBands();
            }
            else if (SaveButtonText == "Edit")
            {
                if (Photo == null)
                {
                    Console.WriteLine("photo null edit");
                    //zwartVak
                    System.Drawing.Image black = System.Drawing.Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "black.jpg");
                    MemoryStream me = new MemoryStream();
                    black.Save(me, System.Drawing.Imaging.ImageFormat.Jpeg);
                    SelectedBand.Photo = me.ToArray();
                    Photo = SelectedBand.Photo;
                }
                else
                {
                    SelectedBand.Photo = Photo;
                }
                Band.EditBand(SelectedBand);
                BandsList = Band.GetBands();
                Photo = null;
            }
        }

        public ICommand DeletBandCommand
        {
            get { return new RelayCommand(DeleteBandHandler); }
        }

        private void DeleteBandHandler()
        {
            string sMessageBoxText = "Weet je zeker dat je " + SelectedBand.Name + " wil verwijderen? Alle lineups worden met deze band worden ook verwijderd.";
            string sCaption = "Bent u zeker?";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Question;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                Band.DeletBand(SelectedBand);
            }
            BandsList = Band.GetBands();
        }

        public ICommand GetPhotoCommand
        {
            get { return new RelayCommand(getPhoto); }
        }

        private void getPhoto()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = ("jpg|*.jpg");
            string path = "";
            if (dialog.ShowDialog() == true)
            {
                path = dialog.FileName;
            }
            if (path != string.Empty)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                MemoryStream me = new MemoryStream();
                img.Save(me, System.Drawing.Imaging.ImageFormat.Jpeg);
                Photo = me.ToArray();
                SelectedBand.Photo = Photo;
            }
            Photo = SelectedBand.Photo;
        }

        public ICommand ZoekCommand
        {
            get { return new RelayCommand<string>(searchBandHandler); }
        }

        private void searchBandHandler(string term)
        {
            if (term == "")
            {
                BandsList = Band.GetBands();
            }
            else
            {
                BandsList = Band.SearchBand(term);
            }
        }

        #endregion

        #region dates
        //dates
        public ICommand SaveDateCommand
        {
            get { return new RelayCommand(saveDateHandler); }
        }

        private void saveDateHandler()
        {
            //als date aangepast word de dagen van de lineup ook wegdoen!!
            string sMessageBoxText = "Weet je zeker dat je datums wilt aanpassen alle lineup dagen tussen deze datums worden verwijderd.";
            string sCaption = "Bent u zeker?";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Question;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                //remove lineups
                LineUp.removeLineUpsDates(Dates);
                Festival.UpdateDate(Dates);
                Dates = Festival.GetDates();
                MessageBox.Show("Datum " + Dates.StartDate.ToString("dd/MM/yyyy") + " - " + Dates.EndDate.ToString("dd/MM/yyyy") + " is succesvol opgeslagen");
            }
        }
        #endregion

        #region genres
        //genres
        public ICommand AddGenreToBand
        {
            get { return new RelayCommand(AddGenreBandHandler); }
        }

        private void AddGenreBandHandler()
        {
            Band.addGenreToBand(SelectedBand,SelectedAddGenreToBand);
            SelectedBand = Band.GetBandByid(SelectedBand);
        }

        public ICommand DeleteGenreFromBandCommand
        {
            get { return new RelayCommand<Genre>(deleteGenreFromBandHandler); }
        }

        private void deleteGenreFromBandHandler(Genre genre)
        {
            Band.DeleteGenreFromBand(SelectedBand,genre);
            SelectedBand = Band.GetBandByid(SelectedBand);
            BandsList = Band.GetBands();          
        }

        public ICommand DeleteGenre
        {
            get { return new RelayCommand<Genre>(deleteGenreHandler); }
        }

        private void deleteGenreHandler(Genre genre)
        {
            Genre.DeleteGenre(genre);
            ListGenres = Genre.GetGenres();
        }
        #endregion

        #region fest name
        //fest name
        public ICommand SaveFestivalName
        {
            get { return new RelayCommand(FestNameHandler); }
        }

        private void FestNameHandler()
        {
            Festival.SaveFestName(FestName);
        }
        #endregion

        #region social media
        //social media
        public ICommand CheckFacebook
        {
            get { return new RelayCommand(CheckFacebookHandler); }
        }

        private void CheckFacebookHandler()
        {
            System.Diagnostics.Process.Start("https://facebook.com/"+SelectedBand.Facebook);
        }

        public ICommand checkTwitter
        {
            get { return new RelayCommand(CheckTwitterHandler); }
        }

        private void CheckTwitterHandler()
        {
            System.Diagnostics.Process.Start("https://twitter.com/" + SelectedBand.Twitter);
        }
        #endregion

        #endregion
    }
}
