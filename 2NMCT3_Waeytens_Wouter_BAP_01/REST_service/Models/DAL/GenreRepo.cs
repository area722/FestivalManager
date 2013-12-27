using PortableLibrary;
using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Web;
using DBHelper;

namespace REST_service.Models.DAL
{
    public class GenreRepo
    {
        public static ObservableCollection<Genre> GetGenres()
        {
            string sql = "SELECT * FROM Genres ORDER BY Name";
            ObservableCollection<Genre> lst = new ObservableCollection<Genre>();
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                Genre genre = new Genre()
                {
                    ID = (int)reader["ID"],
                    Name = (string)reader["Name"]
                };
                lst.Add(genre);
            }
            return lst;
        }
    }
}