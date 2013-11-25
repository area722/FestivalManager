using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace eindopdracht.model
{
    public class Ticket
    {
        private String _id;

        public String Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _ticketHolder;

        public String TicketHolder
        {
            get { return _ticketHolder; }
            set { _ticketHolder = value; }
        }

        private String _ticketHolderEmail;

        public String TicketHolderEmail
        {
            get { return _ticketHolderEmail; }
            set { _ticketHolderEmail = value; }
        }

        private String _ticketType;

        public String TicketType
        {
            get { return _ticketType; }
            set { _ticketType = value; }
        }

        //private TicketType _ticketType;

        //public TicketType TicketType
        //{
        //    get { return _ticketType; }
        //    set { _ticketType = value; }
        //}

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public static ObservableCollection<Ticket> GetTickets()
        {
            return null;
        }
    }
}
