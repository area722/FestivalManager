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
            string sql = "INSERT INTO TicketTypes (Type,Aantal,Price) VALUES (@name,@aantal,@price)";
            DbParameter par1 = DataBase.addparameter("@name",type.Name);
            DbParameter par2 = DataBase.addparameter("@aantal",type.AvailableTickets);
            DbParameter par3 = DataBase.addparameter("@price",type.Price);
            DataBase.modifyData(sql,par1,par2,par3);
        }

        public static void EditTicketType(TicketType type)
        {
            string sql = "UPDATE TicketTypes SET Type=@type,Aantal=@aantal,Price=@price WHERE ID=@id";
            DbParameter par1 = DataBase.addparameter("@type",type.Name);
            DbParameter par2 = DataBase.addparameter("@aantal",type.AvailableTickets);
            DbParameter par3 = DataBase.addparameter("@price",type.Price);
            DbParameter par4 = DataBase.addparameter("@id", type.Id);
            DataBase.modifyData(sql,par1,par2,par3,par4);
        }

        public static void DeleteTicketType(TicketType type)
        {
            string sql = "DELETE FROM TicketTypes WHERE ID=@id";
            DbParameter par1 = DataBase.addparameter("@id",type.Id);
            DataBase.modifyData(sql,par1);

            //gereserveerde tickets verwijderen
            string sql2 = "DELETE FROM Tickets WHERE Type=@typeId";
            DbParameter par2 = DataBase.addparameter("@typeId",type.Id);
            DataBase.modifyData(sql2,par2);
        }

        public static TicketType GetById(int id)
        {
            string sql = "SELECT * FROM TicketTypes WHERE ID=@id";
            DbParameter par = DataBase.addparameter("@id",id);
            DbDataReader reader = DataBase.GetData(sql,par);
            TicketType type = new TicketType();
            while (reader.Read())
            {
                type = new TicketType()
                {
                    Id = (int)reader["ID"],
                    Name = (string)reader["Type"],
                    Price = (double)reader["Price"],
                    AvailableTickets =  (int)reader["Aantal"]
                };
            }
            return type;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
