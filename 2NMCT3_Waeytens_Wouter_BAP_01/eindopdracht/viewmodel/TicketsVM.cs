using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using eindopdracht.model;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

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

        private TicketType _selectedType;

        public TicketType SelectedType
        {
            get { return _selectedType; }
            set { _selectedType = value; SelectedTypeChange(); OnPropertyChanged("SelectedType"); }
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

        private void SelectedTypeChange()
        {
            TicketList = Ticket.SearchType(SelectedType);
        }
        #endregion

        #region commands

        public ICommand PrintTicketCommand
        {
            get { return new RelayCommand(printHandler); }
        }

        private void printHandler()
        {
            //Showfolderdialog met map voor op te slaan!!!!

            string festname = Festival.GetFestName();

            //code for generating word doc
            String filename = SelectedTicket.TicketHolder+"_"+SelectedTicket.TicketType.Name+".docx";
            File.Copy("template.docx",filename,true);
            WordprocessingDocument newdoc = WordprocessingDocument.Open(filename, true);
            IDictionary<String, BookmarkStart> bookmarks = new Dictionary<String, BookmarkStart>();
            foreach (BookmarkStart bms in newdoc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
            {
                bookmarks[bms.Name] = bms;
            }
            bookmarks["Holder"].Parent.InsertAfter<Run>(new Run(new Text(SelectedTicket.TicketHolder)), bookmarks["Holder"]);
            bookmarks["FestName"].Parent.InsertAfter<Run>(new Run(new Text(festname)), bookmarks["FestName"]);
            bookmarks["TicketNumber"].Parent.InsertAfter<Run>(new Run(new Text(Convert.ToString(SelectedTicket.Id))), bookmarks["TicketNumber"]);
            bookmarks["TicketTypePrice"].Parent.InsertAfter<Run>(new Run(new Text(Convert.ToString(SelectedTicket.TicketType.Price))), bookmarks["TicketTypePrice"]);
            bookmarks["TicketType"].Parent.InsertAfter<Run>(new Run(new Text(SelectedTicket.TicketType.Name)), bookmarks["TicketType"]);
            //barcode
            Run run = new Run(new Text(Convert.ToString(DateTime.UtcNow.Ticks)));
            RunProperties prop = new RunProperties();
            RunFonts font = new RunFonts() { Ascii = "Free 3 of 9 Extended", HighAnsi = "Free 3 of 9 Extended" };
            FontSize size = new FontSize() { Val = "96" };
            prop.Append(font);
            prop.Append(size);
            run.PrependChild<RunProperties>(prop);
            bookmarks["BarCode"].Parent.InsertAfter<Run>(run, bookmarks["BarCode"]);
            newdoc.Close();
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


        public ICommand ZoekCommand
        {
            get { return new RelayCommand<string>(zoekHandler); }
        }

        private void zoekHandler(string term)
        {
            if (term != string.Empty && term != "Search")
            {
                Console.WriteLine("term: " + term);
                TicketList = Ticket.SearchTicket(term);
            }
            if (term == string.Empty)
            {
                TicketList = Ticket.GetTickets();
            }
        }


        #endregion
    }
}
