using PortableLibrary;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using DBHelper;

namespace REST_service.Models.DAL
{
    public class GenreRepo
    {
        public static List<Genre> GetGenres()
        {
            string sql = "SELECT * FROM Genres ORDER BY ID";
            List<Genre> lst = new List<Genre>();
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