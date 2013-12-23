using DBHelper;
using PortableLibrary;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace REST_service.Models.DAL
{
    public class BandRepo
    {
        public static List<Band> getBands()
        {
            List<Band> lst = new List<Band>();
            string sql = "SELECT * FROM Bands";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                Band band = new Band()
                {
                    ID = (int)reader[0],
                    Name = (string)reader[1],
                    Phone = (string)reader[2],
                    Fax = (string)reader[3],
                    Email = (string)reader[4],
                    Photo = (Byte[])reader[5],
                    Description = (string)reader[6],
                    Twitter = (string)reader[7],
                    Facebook = (string)reader[8],
                };
               // band.Genres = getGenresByBandId(band);
                lst.Add(band);
            }
            return lst;
        }
    }
}