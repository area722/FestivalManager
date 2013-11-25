using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Data.Common;

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
            lst.Add(new ContactPersoonType() { Id=0,Name="none"});
            string sql = "SELECT * FROM ContactTypes";
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
            string sql = "INSERT INTO ContactTypes (Name) VALUES (@cat)";
            DbParameter par1 = DataBase.addparameter("@cat",categorie);
            DataBase.modifyData(sql,par1);
        }

        public static ObservableCollection<ContactPersoon> GetPersonsByType(ContactPersoonType type)
        {
            string sql = "SELECT * FROM ContactPersons LEFT JOIN ContactTypes ON ContactPersons.JobRole=ContactTypes.ID WHERE ContactTypes.ID = @type";
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
    }
}
