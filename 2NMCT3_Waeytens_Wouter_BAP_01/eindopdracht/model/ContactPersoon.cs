using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Data.Common;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Drawing;
using System.Windows.Controls;

namespace eindopdracht.model
{
    public class ContactPersoon
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

        private String _company;

        public String Company
        {
            get { return _company; }
            set { _company = value; }
        }

        private ContactPersoonType _jobRole;

        public ContactPersoonType JobRole
        {
            get { return _jobRole; }
            set { _jobRole = value; }
        }

        private String _city;

        public String City
        {
            get { return _city; }
            set { _city = value; }
        }

        private String _email;

        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private String _phone;

        public String Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private Byte[] _imageByte;

        public Byte[] ImageByte
        {
            get { return _imageByte; }
            set { _imageByte = value; }
        }
        
        public static ObservableCollection<ContactPersoon> GetContacts()
        {
            ObservableCollection<ContactPersoon> lst = new ObservableCollection<ContactPersoon>();
            string sql = "SELECT * FROM ContactPersons LEFT JOIN ContactTypes ON ContactPersons.JobRole=ContactTypes.ID";
            DbDataReader reader = DataBase.GetData(sql);
            while (reader.Read())
            {
                ContactPersoonType type = new ContactPersoonType()
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
                    JobRole = type
                };
                if (!Convert.IsDBNull(reader[7]))
                {
                    person.ImageByte = (byte[])reader[7];
                }
                lst.Add(person);
            }
            return lst;
        }

        public static void addContact(ContactPersoon person)
        {
            string sql = "INSERT INTO ContactPersons (Name,Company,JobRole,City,Email,Phone) VALUES (@Name,@Company,@JobRole,@City,@Email,@Phone)";
            DbParameter par1 = DataBase.addparameter("@Name", person.Name);
            DbParameter par2 = DataBase.addparameter("@Company", person.Company);
            DbParameter par3 = DataBase.addparameter("@Jobrole", person.JobRole.Id);
            DbParameter par4 = DataBase.addparameter("@City", person.City);
            DbParameter par5 = DataBase.addparameter("@Email", person.Email);
            DbParameter par6 = DataBase.addparameter("@Phone", person.Phone);
            DataBase.modifyData(sql, par1, par2, par3, par4, par5, par6);
        }

        public static void editContact(ContactPersoon person)
        {
            string sql = "UPDATE ContactPersons SET Name=@Name,Company=@Company,JobRole=@JobRole,City=@City,Email=@Email,Phone=@Phone,Image=@Image WHERE ID=@Id";
            DbParameter par1 = DataBase.addparameter("@Name", person.Name);
            DbParameter par2 = DataBase.addparameter("@Company", person.Company);
            DbParameter par3 = DataBase.addparameter("@Jobrole", person.JobRole.Id);
            DbParameter par4 = DataBase.addparameter("@City", person.City);
            DbParameter par5 = DataBase.addparameter("@Email", person.Email);
            DbParameter par6 = DataBase.addparameter("@Phone", person.Phone);
            DbParameter par7 = DataBase.addparameter("@Id", person.Id);
            DbParameter par8 = DataBase.addparameter("@Image", person.ImageByte);
            DataBase.modifyData(sql, par1, par2, par3, par4, par5, par6,par7,par8);
        }

        public static ObservableCollection<ContactPersoon> SearchContactPerson(string searchTerm,ContactPersoonType contactType)
        {
            string sql = "";
            DbDataReader reader = null;
            if (contactType != null)
            {
                sql = "SELECT * FROM ContactPersons LEFT JOIN ContactTypes ON ContactPersons.JobRole=ContactTypes.ID WHERE ContactPersons.Name LIKE @term OR City LIKE @term OR Company LIKE @term OR email LIKE @term AND ContactTypes.ID=@searchType";
                DbParameter par1 = DataBase.addparameter("@term", "%" + searchTerm + "%");
                DbParameter par2 = DataBase.addparameter("@searchType", contactType.Id);
                reader = DataBase.GetData(sql, par1, par2);
            }
            else 
            {
                sql = "SELECT * FROM ContactPersons LEFT JOIN ContactTypes ON ContactPersons.JobRole=ContactTypes.ID WHERE ContactPersons.Name LIKE @term OR City LIKE @term OR Company LIKE @term OR email LIKE @term";
                DbParameter par = DataBase.addparameter("@term", "%" + searchTerm + "%");     
                reader = DataBase.GetData(sql, par);
            }
            ObservableCollection<ContactPersoon> lst = new ObservableCollection<ContactPersoon>();
            while (reader.Read())
            {
                ContactPersoonType type = new ContactPersoonType()
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
                    JobRole = type
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
