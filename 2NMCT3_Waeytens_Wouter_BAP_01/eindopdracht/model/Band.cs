using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace eindopdracht.model
{
    public class Band
    {
        private int _id;

        public int ID
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

        private String _phone;

        public String Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private String _fax;

        public String Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }

        private String _email;

        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }
        
        private Byte[] _photo;

        public Byte[] Photo
        {
            get { return _photo; }
            set { _photo = value; }
        }

        private String _description;

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private String _twitter;

        public String Twitter
        {
            get { return _twitter; }
            set { _twitter = value; }
        }

        private String _facebook;

        public String Facebook
        {
            get { return _facebook; }
            set { _facebook = value; }
        }

        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set { _genres = value; }
        }

        public static ObservableCollection<Band> GetBands()
        {
            ObservableCollection<Band> lst = new ObservableCollection<Band>();
            string sql = "SELECT * FROM Bands";
            DbDataReader reader = DataBase.GetData(sql);
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
            DbParameter idBand = DataBase.addparameter("@idBand",band.ID);
            DbDataReader reader = DataBase.GetData(sql,idBand);
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

        public static void AddBand(Band band)
        {
            Console.WriteLine("add band");
            String sql = "INSERT INTO Bands (Name,Phone,Fax,Email,Description,Twitter,Facebook,Photo) VALUES (@Name,@Phone,@Fax,@Email,@Description,@Twitter,@Facebook,@Photo)";
            DbParameter par1 = DataBase.addparameter("@Name", band.Name);
            DbParameter par2 = DataBase.addparameter("@Phone", band.Phone);
            DbParameter par3 = DataBase.addparameter("@Fax", band.Fax);
            DbParameter par4 = DataBase.addparameter("@Email", band.Email);
            DbParameter par5 = DataBase.addparameter("@Photo", band.Photo);
            DbParameter par6 = DataBase.addparameter("@Description", band.Description);
            DbParameter par7 = DataBase.addparameter("@Twitter", band.Twitter);
            DbParameter par8 = DataBase.addparameter("@Facebook", band.Facebook);
            DataBase.modifyData(sql,par1,par2,par3,par4,par5,par6,par7,par8);
        }

        public static void EditBand(Band band)
        {
            Console.WriteLine("edit band");
            String sql = "UPDATE Bands SET Name=@Name,Phone=@Phone,Fax=@Fax,Email=@Email,Description=@Description,Twitter=@Twitter,Facebook=@Facebook,Photo=@Photo WHERE ID=@Id";
            DbParameter par = DataBase.addparameter("@Id",band.ID);
            DbParameter par1 = DataBase.addparameter("@Name", band.Name);
            DbParameter par2 = DataBase.addparameter("@Phone", band.Phone);
            DbParameter par3 = DataBase.addparameter("@Fax", band.Fax);
            DbParameter par4 = DataBase.addparameter("@Email", band.Email);
            DbParameter par5 = DataBase.addparameter("@Photo", band.Photo);
            DbParameter par6 = DataBase.addparameter("@Description", band.Description);
            DbParameter par7 = DataBase.addparameter("@Twitter", band.Twitter);
            DbParameter par8 = DataBase.addparameter("@Facebook", band.Facebook);
            DataBase.modifyData(sql, par, par1, par2, par3, par4, par5, par6, par7, par8);
        }

        public static void addGenreToBand(Band band,Genre genre)
        {
            String sql = "INSERT INTO GenreBand (IdBand,IdGenre) VALUES (@bandId,@genreId)";
            DbParameter par1 = DataBase.addparameter("@bandId",band.ID);
            DbParameter par2 = DataBase.addparameter("@genreId",genre.ID);
            DataBase.modifyData(sql,par1,par2);
        }

        public static Band GetBandByid(Band band)
        {
            string sql = "SELECT * FROM Bands WHERE ID=@id";
            DbParameter par1 = DataBase.addparameter("@id",band.ID);
            DbDataReader reader = DataBase.GetData(sql,par1);
            Band bnd = new Band();
            while (reader.Read())
            {
                bnd.ID = (int)reader["ID"];
                bnd.Name = (string)reader["Name"];
                bnd.Phone = (string)reader["Phone"];
                bnd.Fax = (string)reader["Fax"];
                bnd.Email = (string)reader["Email"];
                bnd.Photo = (Byte[])reader["Photo"];
                bnd.Description = (string)reader["Description"];
                bnd.Twitter = (string)reader["Twitter"];
                bnd.Facebook = (string)reader["Facebook"];
                bnd.Genres = getGenresByBandId(band);
            }
            return bnd;
        }

        public static void DeleteGenreFromBand(Band band,Genre genre)
        {
            string sql = "DELETE FROM GenreBand WHERE IdBand=@bandID AND IdGenre=@genreID";
            DbParameter idBand = DataBase.addparameter("@bandID",band.ID);
            DbParameter idGenre = DataBase.addparameter("@genreID",genre.ID);
            DataBase.modifyData(sql,idBand,idGenre);
        }

        public static void DeletBand(Band band)
        {
            string sql = "DELETE FROM Bands WHERE ID=@id";
            DbParameter par1 = DataBase.addparameter("@id",band.ID);
            DataBase.modifyData(sql,par1);


            //code voor lineup te deleten!!
            string sql1 = "DELETE FROM ";
        }
    }
}
