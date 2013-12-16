﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using eindopdracht.model;
using System.Windows.Input;
using System.Windows;

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

        private string _viewPrintButton;

        public string viewPrintButton
        {
            get { return _viewPrintButton; }
            set { _viewPrintButton = value; OnPropertyChanged("viewPrintButton"); }
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
        #endregion

        #region ctor
        public TicketsVM()
        {
            _TicketList = Ticket.GetTickets();
            _ticketTypeList = TicketType.GetTypesTickets();
            viewPrintButton = "Hidden";

            //reserve ticket
            ReserveTicket = new Ticket();
            lstAantalTickets = aantalTicketsFill();
            TicketTypeList = TicketType.GetTypesTickets();
        }
        #endregion

        #region methods
        private ObservableCollection<int> aantalTicketsFill()
        {
            ObservableCollection<int> lst = new ObservableCollection<int>();
            for (int i = 1; i < 11; i++)
            {
                lst.Add(i);
            }
            return lst;
        }

        private void selectedTicketChange()
        {
            Console.WriteLine(SelectedTicket.TicketHolder);
            if (SelectedTicket != null)
            {
                //display print button
                viewPrintButton = "Visible";
            }
            else
            {
                viewPrintButton = "Hidden";
            }
        }
        #endregion

        #region commands

        public ICommand PrintTicketCommand
        {
            get { return new RelayCommand(printHandler); }
        }

        private void printHandler()
        {
            //code for generating word doc
        }

        public ICommand ReserveTicketCommand
        {
            get { return new RelayCommand(reserveHandler); }
        }

        private void reserveHandler()
        {
            Ticket.ReserveTicket(ReserveTicket);
            TicketList = Ticket.GetTickets();
            ReserveTicket = new Ticket();
            TicketTypeList = TicketType.GetTypesTickets();
        }


        public ICommand UpdateTicketTypesCommand
        {
            get { return new RelayCommand(UpdateTicketTypesHandler); }
        }

        private void UpdateTicketTypesHandler()
        {
            TicketTypeList = TicketType.GetTypesTickets();
        }

        public ICommand UpdateRerservedTicketsCommand
        {
            get { return new RelayCommand(UpdateReservedHandler); }
        }

        private void UpdateReservedHandler()
        {
            TicketList = Ticket.GetTickets();
        }

        #endregion
    }
}
