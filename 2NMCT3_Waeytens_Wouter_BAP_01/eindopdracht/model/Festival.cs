using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace eindopdracht.model
{
    public class Festival
    {
        private String _festName;

        public String FestName
        {
            get { return _festName; }
            set { _festName = value; }
        }
        
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

        public static Festival GetDates()
        {
            string sql = "SELECT * FROM Dates";
            DbDataReader reader = DataBase.GetData(sql);
            Festival fest = new Festival();
            while(reader.Read())
            {
                fest.EndDate = (DateTime)reader["EndDate"];
                fest.StartDate = (DateTime)reader["StartDate"];
            }
            return fest;
        }

        public static void UpdateDate(Festival fest)
        {
            string sql1 = "SELECT * FROM Dates";
            DbDataReader reader = DataBase.GetData(sql1);
            List<int> lst = new List<int>();
            while (reader.Read())
            {
                lst.Add((int)reader["ID"]);
            }

            string sql;
            DbParameter par3 = DataBase.addparameter("@id",1);
            if (lst.Count != 0)
            {
                sql = "UPDATE Dates SET EndDate=@endDate,StartDate=@startDate WHERE ID=@id";
                par3 = DataBase.addparameter("@id",lst[0]);
            }
            else
            {
                sql = "INSERT INTO Dates (EndDate,StartDate) VALUES (@endDate,@startDate)";
            }

            DbParameter par1 = DataBase.addparameter("@endDate", fest.EndDate);
            DbParameter par2 = DataBase.addparameter("@startDate", fest.StartDate);
            DataBase.modifyData(sql, par1, par2,par3);
        }

        public static void SaveFestName(String name)
        {
            string sql = "SELECT * FROM FestName";
            DbDataReader reader = DataBase.GetData(sql);
            List<int> lst = new List<int>();
            while (reader.Read())
            {
                lst.Add((int)reader["ID"]);
            }

            DbParameter idPar = DataBase.addparameter("@id",1);
            if (lst.Count == 0)
            {
                sql = "INSERT INTO FestName (FestName) VALUES (@name)";
            }
            else
            {
                sql = "UPDATE FestName SET FestName=@name WHERE id=@id";
                idPar = DataBase.addparameter("@id", lst[0]);
            }
            DbParameter namePar = DataBase.addparameter("@name", name);
            DataBase.modifyData(sql,namePar,idPar);
            MessageBox.Show(name+" opgeslagen.");
        }

        public static String GetFestName()
        {
            string sql = "SELECT * FROM FestName";
            DbDataReader reader = DataBase.GetData(sql);
            while (reader.Read())
            {
                return (string)reader["FestName"];
            }
            return "";
        }
    }
}
