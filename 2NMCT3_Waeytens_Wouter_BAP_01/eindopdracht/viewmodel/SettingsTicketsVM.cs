using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using eindopdracht.model;

namespace eindopdracht.viewmodel
{
    class SettingsTicketsVM:ObservableObject,IPage
    {
        public string Name
        {
            get { return "Tickets"; }
        }

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

        public SettingsTicketsVM()
        {
            _TicketList = Ticket.GetTickets();
            _ticketTypeList = TicketType.GetTypesTickets();
        }
    }
}
