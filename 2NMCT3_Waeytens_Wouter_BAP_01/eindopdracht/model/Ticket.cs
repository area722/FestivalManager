﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Data.Common;

namespace eindopdracht.model
{
    public class Ticket
    {
        private int _id;

        public int Id
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

        private TicketType _ticketType;

        public TicketType TicketType
        {
            get { return _ticketType; }
            set { _ticketType = value; }
        }

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private String _reserved;

        public String Reserved
        {
            get { return _reserved; }
            set { _reserved = value; }
        }
        

        public static ObservableCollection<Ticket> GetTickets()
        {
            string sql = "SELECT * FROM Tickets";
            DbDataReader reader = DataBase.GetData(sql);
            ObservableCollection<Ticket> lst = new ObservableCollection<Ticket>();
            while (reader.Read())
            {
                Ticket tick = new Ticket() 
                {
                    Id = (int)reader["ID"],
                    Amount = (int)reader["Aantal"],
                    TicketHolder = (string)reader["Holder"],
                    TicketHolderEmail = (string)reader["HolderEmail"],
                    TicketType = TicketType.GetById((int)reader["Type"]),
                    Reserved = (string)reader["Reserved"]
                };
                lst.Add(tick);
            }
            return lst;
        }

        public static void ReserveTicket(Ticket ticket)
        {
            string sql = "INSERT INTO Tickets(Holder,HolderEmail,Type,Aantal,Reserved) VALUES(@holder,@email,@type,@aantal,@reserved)";
            DbParameter par1 = DataBase.addparameter("@holder",ticket.TicketHolder);
            DbParameter par2 = DataBase.addparameter("@email",ticket.TicketHolderEmail);
            DbParameter par3 = DataBase.addparameter("@type",ticket.TicketType.Id);
            DbParameter par4 = DataBase.addparameter("@aantal",ticket.Amount);
            DbParameter par5 = DataBase.addparameter("@reserved","Yes");
            DataBase.modifyData(sql,par1,par2,par3,par4,par5);

            //totaal aantal voor dat type verminderen
        }


    }
}
