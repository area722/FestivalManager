using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Data.Common;
using System.Windows;
using eindopdracht.viewmodel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

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
            //totaal aantal voor dat type verminderen
            string sql1 = "SELECT Aantal FROM TicketTypes WHERE ID=@id";
            DbParameter par6 = DataBase.addparameter("@id",ticket.TicketType.Id);
            DbDataReader reader = DataBase.GetData(sql1,par6);
            int aantal = 0;
            while (reader.Read())
            {
                aantal = (int)reader["Aantal"];
            }

            aantal -= ticket.Amount;
            if (aantal >= 0)
            {
                //vermindering wegschijven in database.
                string sql2 = "UPDATE TicketTypes SET Aantal=@aantal WHERE ID=@id";
                DbParameter aantalPar = DataBase.addparameter("@aantal", aantal);
                DbParameter idPar = DataBase.addparameter("@id", ticket.TicketType.Id);
                DataBase.modifyData(sql2, aantalPar, idPar);

                string sql = "INSERT INTO Tickets(Holder,HolderEmail,Type,Aantal,Reserved) VALUES(@holder,@email,@type,@aantal,@reserved)";
                DbParameter par1 = DataBase.addparameter("@holder", ticket.TicketHolder);
                DbParameter par2 = DataBase.addparameter("@email", ticket.TicketHolderEmail);
                DbParameter par3 = DataBase.addparameter("@type", ticket.TicketType.Id);
                DbParameter par4 = DataBase.addparameter("@aantal", ticket.Amount);
                DbParameter par5 = DataBase.addparameter("@reserved", "Yes");
                DataBase.modifyData(sql, par1, par2, par3, par4, par5);

                MessageBox.Show("Ticket gereseveerd voor "+ticket.TicketHolder);
            }
            else
            {
                MessageBox.Show("Er kunnen geen tickets meer gereserveerd worden voor het type " + ticket.TicketType.Name);
            }
        }

        public static ObservableCollection<Ticket> SearchTicket(String term)
        { 
            ObservableCollection<Ticket> lst = new ObservableCollection<Ticket>();
            string sql = "SELECT * FROM Tickets WHERE HolderEmail LIKE @term OR Holder LIKE @term";
            DbParameter par1 = DataBase.addparameter("@term","%"+term+"%");
            DbDataReader reader = DataBase.GetData(sql,par1);
            while (reader.Read())
            {
                Ticket ticket = new Ticket() 
                {
                    Id = (int)reader["ID"],
                    TicketHolder = (string)reader["Holder"],
                    TicketHolderEmail = (string)reader["HolderEmail"],
                    TicketType = TicketType.GetById((int)reader["Type"]),
                    Amount = (int)reader["Aantal"],
                    Reserved = (string)reader["Reserved"]
                };
                lst.Add(ticket);
            }
            return lst;
        }

        public static ObservableCollection<Ticket> SearchType(TicketType type)
        {
            ObservableCollection<Ticket> lst = new ObservableCollection<Ticket>();
            string sql = "SELECT * FROM Tickets WHERE Type = @type";
            DbParameter par1 = DataBase.addparameter("@type",type.Id);
            DbDataReader reader = DataBase.GetData(sql,par1);
            while(reader.Read())
            {
                Ticket ticket = new Ticket() 
                {
                    Id = (int)reader["ID"],
                    TicketHolder = (string)reader["Holder"],
                    TicketHolderEmail = (string)reader["HolderEmail"],
                    TicketType = TicketType.GetById((int)reader["Type"]),
                    Amount = (int)reader["Aantal"],
                    Reserved = (string)reader["Reserved"]
                };
                lst.Add(ticket);
            }
            return lst;
        }

        public static void PrintTicket(Ticket SelectedTicket,String path)
        {
            string festname = Festival.GetFestName();

            //code for generating word doc
            String filename = path+SelectedTicket.TicketHolder + "_" + SelectedTicket.TicketType.Name + ".docx";
            File.Copy("template.docx", filename, true);
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
    }
}
