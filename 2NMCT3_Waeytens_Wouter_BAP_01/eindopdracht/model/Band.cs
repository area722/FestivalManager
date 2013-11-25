using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht.model
{
    public class Band
    {
        private String _id;

        public String ID
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

        private String _photo;

        public String Photo
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

        private String _genres;

        public String Genres
        {
            get { return _genres; }
            set { _genres = value; }
        }

        //private ObservableCollection<Genre> _genres;

        //public ObservableCollection<Genre> Genres
        //{
        //    get { return _genres; }
        //    set { _genres = value; }
        //}

        public static ObservableCollection<Band> GetBands()
        {
            ObservableCollection<Band> lst = new ObservableCollection<Band>();
            StreamReader reader = new StreamReader("bands.csv");
            reader.ReadLine();
            String line = reader.ReadLine();
            while (line != null)
            {
                String[] splitArr = line.Split(';');
                Band band = new Band() {ID=splitArr[0],Name=splitArr[1],Photo=splitArr[2],Description=splitArr[3],Twitter=splitArr[4],Facebook=splitArr[5],Genres=splitArr[6] };
                lst.Add(band);
                line = reader.ReadLine();
            }
            return lst;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
