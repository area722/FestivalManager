using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using eindopdracht.model;

namespace eindopdracht.viewmodel
{
    class ApplicationVM: ObservableObject
    {
        public ApplicationVM()
        {
            Pages.Add(new ContactVM());
            Pages.Add(new LineUpVM());
            Pages.Add(new TicketsVM());
            Pages.Add(new SettingsVM());

            CurrentPage = Pages[0];

            //festname
            FestName = Festival.GetFestName();

            BackgroundImage = AppDomain.CurrentDomain.BaseDirectory + "pattern.png";
        }

        private IPage _currentpage;
        public IPage CurrentPage
        {
            get { return _currentpage; }
            set { _currentpage = value; OnPropertyChanged("CurrentPage"); }
        }

        private List<IPage> _pages;
        public List<IPage> Pages
        {
            get
            {
                if (_pages == null)
                    _pages = new List<IPage>();
                return _pages;
            }
        }

        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }

        private void ChangePage(IPage page)
        {
            CurrentPage = page;
        }

        //festname in title van window
        private string _festName;

        public string FestName
        {
            get { return _festName; }
            set { _festName = value; OnPropertyChanged("FestName"); }
        }

        //background pattern
        private string _backgroundImage;

        public string BackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; OnPropertyChanged("BackgroundImage"); }
        }
        
        
    }
}
