﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableLibrary
{
    public class Genre
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
    }
}
