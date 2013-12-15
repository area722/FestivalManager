using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using eindopdracht.model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;

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

        private Ticket _reserveTicket;

        public Ticket ReserveTicket
        {
            get { return _reserveTicket; }
            set { _reserveTicket = value; OnPropertyChanged("ReserveTicket"); }
        }

        private ObservableCollection<int> _lstAantalTickets;

        public ObservableCollection<int> lstAantalTickets
        {
            get { return _lstAantalTickets; }
            set { _lstAantalTickets = value; OnPropertyChanged("lstAantalTickets"); }
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
            ReserveTicket = new Ticket();
            TextEditSaveType = "Save";
            visibleButton = "Hidden";
            lstAantalTickets = aantalTicketsFill();
        }
        #endregion

        private ObservableCollection<int> aantalTicketsFill()
        {
            ObservableCollection<int> lst = new ObservableCollection<int>();
            for (int i = 1; i < 11; i++)
            {
                lst.Add(i);
            }
            return lst;
        }

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
                TicketType.EditTicketType(SaveTicketType);
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
            visibleButton = "Hidden";
            SaveTicketType = new TicketType();
            TextEditSaveType = "Save";
        }


        public ICommand DeleteTypeCommand
        {
            get { return new RelayCommand<TicketType>(deleteHandler); }
        }

        private void deleteHandler(TicketType type)
        {
            SaveTicketType = new TicketType();
            visibleButton = "Hidden";
            TextEditSaveType = "Save";
            //alle gereserveerde tickets met dat type ook verwijderen en waarschuwen met een dialogbox
            string sMessageBoxText = "Weet je zeker dat je het Type "+type.Name+" wil verwijderen alle gereserveerde tickets voor het type "+type.Name+" worden ook verwijderd.";
            string sCaption = "Bent u zeker?";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Asterisk;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                TicketType.DeleteTicketType(type);
            }

            TicketTypeList = TicketType.GetTypesTickets();
        }

        public ICommand ReserveTicketCommand
        {
            get { return new RelayCommand(reserveHandler); }
        }

        private void reserveHandler()
        {
            Ticket.ReserveTicket(ReserveTicket);
            MessageBox.Show("Ticket Reserved for " + ReserveTicket.TicketHolder);
            ReserveTicket = new Ticket();
        }

        #endregion
    }
}
