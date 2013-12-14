using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using eindopdracht.model;
using System.Data.Common;

namespace eindopdracht.model
{
    public class TicketType
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private Double _price;

        public Double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private int _availableTickets;

        public int AvailableTickets
        {
            get { return _availableTickets; }
            set { _availableTickets = value; }
        }

        public static ObservableCollection<TicketType> GetTypesTickets()
        {
            ObservableCollection<TicketType> lst = new ObservableCollection<TicketType>();
            string sql = "SELECT * FROM TicketTypes";
            DbDataReader reader = DataBase.GetData(sql);
            while (reader.Read())
            {
                TicketType type = new TicketType()
                { 
                    Id = Convert.ToInt32(reader["ID"]),
                    Name = (string)reader["Type"],
                    Price = (double)reader["Price"],
                    AvailableTickets = (int)reader["Aantal"]
                };
                lst.Add(type);
            }
            return lst;
        }

        public static void InstertTicketType(TicketType type)
        {
            //Console.WriteLine(type.Name);
        }
    }
}
