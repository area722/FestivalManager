using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace eindopdracht.model
{
    public class TicketType
    {
        private String _id;

        public String Id
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

        private Double _price;

        public Double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private int _availableTickets;

        public int AvailableTickets
        {
            get { return _availableTickets; }
            set { _availableTickets = value; }
        }

        public static ObservableCollection<TicketType> GetTypesTickets()
        {
            ObservableCollection<TicketType> lst = new ObservableCollection<TicketType>();
            StreamReader reader = new StreamReader("ticketType.csv");
            reader.ReadLine();
            String line = reader.ReadLine();
            while (line != null)
            {
                String[] splitArr = line.Split(';');
                TicketType type = new TicketType() {Id=splitArr[0],Name=splitArr[1],Price= Convert.ToDouble(splitArr[2]),AvailableTickets = Convert.ToInt32(splitArr[3])};
                lst.Add(type);
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
