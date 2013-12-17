using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace eindopdracht.model
{
    public class LineUp
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private DateTime _from;

        public DateTime From
        {
            get { return _from; }
            set { _from = value; }
        }

        private DateTime _until;

        public DateTime Until
        {
            get { return _until; }
            set { _until = value; }
        }

        private Stage _stage;

        public Stage Stage
        {
            get { return _stage; }
            set { _stage = value; }
        }

        private Band _band;

        public Band Band
        {
            get { return _band; }
            set { _band = value; }
        }

        public static void InsertLinup(LineUp lineup)
        {
            string sql = "INSERT INTO LineUp (Date,StartTime,EndTime,Stage) VALUES (@date,@startTime,@endTime,@stage)";
            DbParameter par1 = DataBase.addparameter("@date",lineup.Date);
            DbParameter par2 = DataBase.addparameter("@startTime",lineup.From.ToShortTimeString());
            DbParameter par3 = DataBase.addparameter("@endTime",lineup.Until.ToShortTimeString());
            DbParameter par4 = DataBase.addparameter("@stage",lineup.Stage.ID);
            DataBase.modifyData(sql,par1,par2,par3,par4);
        }

        public static ObservableCollection<LineUp> getLineupsByStageAndDay(LineUp lineUp)
        {
            //kijken of lineup al bestaat
            String sql = "SELECT LineUp.ID,LineUp.Date,LineUp.EndTime,LineUp.StartTime,Stages.Name,Stages.ID AS StageID FROM LineUp INNER JOIN Stages ON LineUp.Stage=Stages.ID WHERE Stage = @stageId AND Date = @date";
            DbParameter par1 = DataBase.addparameter("@stageId",lineUp.Stage.ID);
            DbParameter par2 = DataBase.addparameter("@date",lineUp.Date);
            DbDataReader reader = DataBase.GetData(sql,par1,par2);
            ObservableCollection<LineUp> lst = new ObservableCollection<LineUp>();
            while (reader.Read())
            {
                Console.WriteLine(reader["EndTime"]);
                LineUp lineup = new LineUp()
                {
                    ID = (int)reader["ID"],
                    Date = Convert.ToDateTime(reader["Date"]),
                    Until = Convert.ToDateTime(reader["EndTime"]),
                    From = (DateTime)reader["StartTime"],
                };
                lineup.Stage = new Stage()
                {
                    ID = (int)reader["StageID"],
                    Name = (string)reader["Name"]
                };
                lst.Add(lineup);
            }
            return lst;
        }

    }
}
