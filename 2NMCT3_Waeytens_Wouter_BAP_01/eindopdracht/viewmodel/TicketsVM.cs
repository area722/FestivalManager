using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using eindopdracht.model;

namespace eindopdracht.viewmodel
{
    class TicketsVM:ObservableObject,IPage
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

        private Ticket _SelectedTicket;

        public Ticket SelectedTicket
        {
            get { return _SelectedTicket; }
            set { _SelectedTicket = value; OnPropertyChanged("SelectedTicket"); selectedTicketChange(); }
        }        
        #endregion

        private void selectedTicketChange()
        {
            Console.WriteLine(SelectedTicket.TicketHolder);
            if (SelectedTicket != null)
            { 
                //display print button
            }
        }

        #region ctor
        public TicketsVM()
        {
            _TicketList = Ticket.GetTickets();
            _ticketTypeList = TicketType.GetTypesTickets();

            //er moet een mogelijkheid bestaan om als een tickettype is opgeslagen of een ticket gereserveerd om deze lijst op te daten
        }
        #endregion

        #region commands

        #endregion
    }
}
