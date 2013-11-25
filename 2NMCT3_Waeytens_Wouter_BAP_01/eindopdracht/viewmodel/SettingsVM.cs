using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace eindopdracht.viewmodel
{
    class SettingsVM:ObservableObject,IPage
    {
        public string Name
        {
            get { return "Settings"; }
        }

        public SettingsVM()
        {
            PagesSettings.Add(new SettingsTicketsVM());
            PagesSettings.Add(new SettingsBandVM());

            CurrentPageSettings = PagesSettings[1];
        }

        private IPage _currentpageSettings;
        public IPage CurrentPageSettings
        {
            get { return _currentpageSettings; }
            set { _currentpageSettings = value; OnPropertyChanged("CurrentPageSettings"); }
        }

        private List<IPage> _pagesSettings;
        public List<IPage> PagesSettings
        {
            get
            {
                if (_pagesSettings == null)
                    _pagesSettings = new List<IPage>();
                return _pagesSettings;
            }
        }

        public ICommand ChangePageCommandSettings
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }

        private void ChangePage(IPage page)
        {
            CurrentPageSettings = page;
        }           
    }
}
