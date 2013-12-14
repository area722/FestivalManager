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
    class SettingsTicketsVM:ObservableObject,IPage
    {
        public string Name
        {
            get { return "Tickets"; }
        }

        #region properties for binding
        private ObservableCollection<Ticket> _TicketList;

        public ObservableCollection<Ticket> TicketList
        {
            get { return _TicketList; }
            set { _TicketList = value; OnPropertyChanged("TicketList"); }
        }

        private ObservableCollection<TicketType> _ticketTypeList;

        public ObservableCollection<TicketType> TicketTypeList
        {
            get { return _ticketTypeList; }
            set { _ticketTypeList = value; OnPropertyChanged("TicketTypeList"); }
        }

        private TicketType _SaveTicketType;

        public TicketType SaveTicketType
        {
            get { return _SaveTicketType; }
            set { _SaveTicketType = value; OnPropertyChanged("SaveTicketType"); }
        }      

        private String _textEditSaveType;

        public String TextEditSaveType
        {
            get { return _textEditSaveType; }
            set { _textEditSaveType = value; OnPropertyChanged("TextEditSaveType"); }
        }

        private String _visibleButton;

        public String visibleButton
        {
            get { return _visibleButton; }
            set { _visibleButton = value; OnPropertyChanged("visibleButton"); }
        }
        
        #endregion

        #region ctor
        public SettingsTicketsVM()
        {
            _TicketList = Ticket.GetTickets();
            _ticketTypeList = TicketType.GetTypesTickets();
            SaveTicketType = new TicketType();
            TextEditSaveType = "Save";
            visibleButton = "Hidden";
        }
        #endregion

        #region commands
        public ICommand addTicketTypeCommand
        {
            get { return new RelayCommand(addTicketTypeHandler); }
        }

        private void addTicketTypeHandler()
        {
            if (TextEditSaveType == "Save")
            {
                TicketType.InstertTicketType(SaveTicketType);
            }
            if (TextEditSaveType == "Edit")
            { 
                
            }
            TicketTypeList = TicketType.GetTypesTickets();
        }

        public ICommand editTypeCommand
        {
            get { return new RelayCommand<TicketType>(editTypeHandler); }
        }

        private void editTypeHandler(TicketType type)
        {
            //Console.WriteLine(type.Name);
            SaveTicketType = type;
            TextEditSaveType = "Edit";
            visibleButton = "Visible";
        }


        public ICommand newTypeCommand
        {
            get { return new RelayCommand(newTypeHandler); }
        }

        private void newTypeHandler()
        {
            Console.WriteLine("command");
            visibleButton = "Hidden";
            SaveTicketType = new TicketType();
            TextEditSaveType = "Save";
        }
        #endregion
    }
}
