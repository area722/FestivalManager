using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht.model
{
    public class Festival
    {
        private DateTime _startdata;

        public DateTime StartDate
        {
            get { return _startdata; }
            set { _startdata = value; }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
    }
}
