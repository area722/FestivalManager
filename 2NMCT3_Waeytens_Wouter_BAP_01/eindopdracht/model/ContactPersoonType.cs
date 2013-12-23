using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Data.Common;
using System.Windows;

namespace eindopdracht.model
{
    public class ContactPersoonType
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

        public static ObservableCollection<ContactPersoonType> GetTypes()
        {
            ObservableCollection<ContactPersoonType> lst = new ObservableCollection<ContactPersoonType>();
            string sql = "SELECT * FROM ContactTypes ORDER BY Name ASC";
            DbDataReader reader = DataBase.GetData(sql);
            while (reader.Read())
            {
                ContactPersoonType type = new ContactPersoonType()
                {
                    Id = (int)reader[0],
                    Name = (string)reader[1]
                };
                lst.Add(type);
            }
            return lst;
        }

        public static void AddCategorie(String categorie)
        {
            string sql1 = "SELECT * FROM ContactTypes WHERE Name = @catName";
            DbParameter par2 = DataBase.addparameter("@catName",categorie);
            DbDataReader reader = DataBase.GetData(sql1,par2);
            ObservableCollection<ContactPersoonType> lst = new ObservableCollection<ContactPersoonType>();
            while (reader.Read())
            {
                ContactPersoonType type = new ContactPersoonType() 
                {
                    Id = (int)reader["ID"],
                    Name = (string)reader["Name"]
                };
                lst.Add(type);
            }

            if (lst.Count == 0)
            {
                string sql = "INSERT INTO ContactTypes (Name) VALUES (@cat)";
                DbParameter par1 = DataBase.addparameter("@cat", categorie);
                DataBase.modifyData(sql, par1);
            }
            else
            {
                MessageBox.Show("Het type "+categorie+" bestaat al.");
            }
        }

        public static void EditType(ContactPersoonType type)
        {
            string sql = "UPDATE ContactTypes SET Name=@name WHERE ID=@id";
            DbParameter parName = DataBase.addparameter("@name",type.Name);
            DbParameter parId = DataBase.addparameter("@id",type.Id);
            DataBase.modifyData(sql,parName,parId);
        }

        public static ObservableCollection<ContactPersoon> GetPersonsByType(ContactPersoonType type)
        {
            string sql = "SELECT * FROM ContactPersons LEFT JOIN ContactTypes ON ContactPersons.JobRole=ContactTypes.ID WHERE ContactTypes.ID = @type ORDER BY ContactPersons.Name ASC";
            ObservableCollection<ContactPersoon> lst = new ObservableCollection<ContactPersoon>();
            DbParameter par = DataBase.addparameter("@type",type.Id);
            DbDataReader reader = DataBase.GetData(sql,par);
            while (reader.Read())
            {
                ContactPersoonType personType = new ContactPersoonType()
                {
                    Id = (int)reader[8],
                    Name = (string)reader[9]
                };
                ContactPersoon person = new ContactPersoon()
                {
                    Id = (int)reader[0],
                    Name = (string)reader[1],
                    Company = (string)reader[2],
                    City = (string)reader[4],
                    Email = (string)reader[5],
                    Phone = (string)reader[6],
                    JobRole = personType
                };
                if (!Convert.IsDBNull(reader[7]))
                {
                    person.ImageByte = (byte[])reader[7];
                }
                lst.Add(person);
            }
            return lst;
        }

        public static void deleteType(ContactPersoonType type)
        {
            string sql = "DELETE FROM ContactTypes WHERE ID=@id";
            DbParameter par = DataBase.addparameter("@id",type.Id);
            DataBase.modifyData(sql,par);

            //alle contactpersonen met dit type ook deleten
            string sql1 = "DELETE FROM ContactPersons WHERE JobRole=@typeID";
            DbParameter par1 = DataBase.addparameter("@typeID",type.Id);
            DataBase.modifyData(sql1,par1);

            MessageBox.Show(type.Name+ " deleted.");
        }
    }
}
