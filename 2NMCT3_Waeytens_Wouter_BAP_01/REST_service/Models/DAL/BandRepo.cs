using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Web;
using DBHelper;
using PortableLibrary;

namespace REST_service.Models.DAL
{
    public class BandRepo
    {
        public static ObservableCollection<Band> getBands()
        {
            ObservableCollection<Band> lst = new ObservableCollection<Band>();
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
                band.Genres = getGenresByBandId(band);
                lst.Add(band);
            }
            return lst;
        }

        public static ObservableCollection<Genre> getGenresByBandId(Band band)
        {
            ObservableCollection<Genre> lst = new ObservableCollection<Genre>();
            string sql = "SELECT GenreBand.IdBand, Genres.Name, GenreBand.IdGenre FROM GenreBand INNER JOIN Genres ON GenreBand.IdGenre=Genres.ID WHERE GenreBand.IdBand = @idBand";
            DbParameter idBand = Database.AddParameter("@idBand", band.ID);
            DbDataReader reader = Database.GetData(sql, idBand);
            while (reader.Read())
            {
                Genre genre = new Genre()
                {
                    ID = (int)reader["IdGenre"],
                    Name = (string)reader["Name"]
                };
                lst.Add(genre);
            }
            return lst;
        }
    }
}