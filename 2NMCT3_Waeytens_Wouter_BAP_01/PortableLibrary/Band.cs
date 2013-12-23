
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PortableLibrary
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

        //private ObservableCollection<Genre> _genres;

        //public ObservableCollection<Genre> Genres
        //{
        //    get { return _genres; }
        //    set { _genres = value; }
        //}
    }
}
