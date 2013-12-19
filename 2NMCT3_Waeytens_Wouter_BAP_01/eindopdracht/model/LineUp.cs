using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

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
            //kijken of lineup al bestaat
            string sql1 = "SELECT * FROM LineUp WHERE Date = @date AND StartTime = @startTime AND EndTime = @endTime AND Stage = @stage";
            DbParameter datePar = DataBase.addparameter("@date", lineup.Date);
            DbParameter startPar = DataBase.addparameter("@startTime", lineup.From.ToShortTimeString());
            DbParameter endPar = DataBase.addparameter("@endTime", lineup.Until.ToShortTimeString());
            DbParameter stagePar = DataBase.addparameter("@stage", lineup.Stage.ID);
            DbDataReader reader = DataBase.GetData(sql1, datePar,startPar,endPar,stagePar);
            ObservableCollection<LineUp> lst = new ObservableCollection<LineUp>();
            while (reader.Read())
            {
                LineUp lu = new LineUp() 
                {
                    ID = (int)reader["ID"],
                    From = Convert.ToDateTime(reader["StartTime"]),
                    Until = Convert.ToDateTime(reader["EndTime"])
                };
                lst.Add(lu);
            }
            
            //kijken of linups niet overlappen
            string sql2 = "SELECT * FROM LineUp WHERE Date=@date AND Stage=@stage";
            DbParameter datePar1 = DataBase.addparameter("@date",lineup.Date);
            DbParameter stagePar1 = DataBase.addparameter("@stage",lineup.Stage.ID);
            DbDataReader reader1 = DataBase.GetData(sql2,datePar1,stagePar1);
            ObservableCollection<LineUp> lst1 = new ObservableCollection<LineUp>();
            while (reader1.Read())
            {
                LineUp lu = new LineUp() 
                {
                    ID = (int)reader1["ID"],
                    From = Convert.ToDateTime(reader1["StartTime"]),
                    Until = Convert.ToDateTime(reader1["EndTime"])
                };
                lst1.Add(lu);
            }
            Boolean overlapBool = false;
            foreach (LineUp lu in lst1)
	        {
                if (lu.From.TimeOfDay < lineup.From.TimeOfDay && lu.Until.TimeOfDay < lineup.Until.TimeOfDay)
                {
                    overlapBool = true;
                }
                if (lu.Until.TimeOfDay <= lineup.From.TimeOfDay)
                {
                    overlapBool = false;
                }
                if (lu.From.TimeOfDay < lineup.From.TimeOfDay && lu.Until.TimeOfDay > lineup.Until.TimeOfDay)
                {
                    overlapBool = true;
                }
                if(lu.From.TimeOfDay > lineup.From.TimeOfDay && lu.Until.TimeOfDay > lineup.Until.TimeOfDay)
                {
                    overlapBool = true;
                }
                if(lineup.From.TimeOfDay < lu.From.TimeOfDay && lineup.Until.TimeOfDay <= lu.From.TimeOfDay)
                {
                    overlapBool = false;
                }
                if (lu.From.TimeOfDay == lineup.From.TimeOfDay && lu.Until.TimeOfDay > lineup.Until.TimeOfDay)
                {
                    overlapBool = true;
                }
                if (lu.From.TimeOfDay < lineup.From.TimeOfDay && lu.Until.TimeOfDay == lineup.Until.TimeOfDay)
                {
                    overlapBool = true;
                }
	        }

            if (lst.Count > 0)
            {
                MessageBox.Show("Dit tijdslot bestaat al.");
            }
            if (overlapBool)
            {
                MessageBox.Show("Tijdslots mogen niet overlappen");
            }
            else
            {
                string sql = "INSERT INTO LineUp (Date,StartTime,EndTime,Stage) VALUES (@date,@startTime,@endTime,@stage)";
                DbParameter par1 = DataBase.addparameter("@date", lineup.Date);
                DbParameter par2 = DataBase.addparameter("@startTime", lineup.From.ToShortTimeString());
                DbParameter par3 = DataBase.addparameter("@endTime", lineup.Until.ToShortTimeString());
                DbParameter par4 = DataBase.addparameter("@stage", lineup.Stage.ID);
                DataBase.modifyData(sql, par1, par2, par3, par4);
            }
        }



        public static ObservableCollection<LineUp> getLineupsByStageAndDay(LineUp lineUp)
        {
            String sql = "SELECT LineUp.ID,LineUp.Date,LineUp.EndTime,LineUp.StartTime,Stages.Name,Stages.ID AS StageID FROM LineUp INNER JOIN Stages ON LineUp.Stage=Stages.ID WHERE Stage = @stageId AND Date = @date ORDER BY CAST(LineUp.StartTime AS datetime) ASC";
            DbParameter par1 = DataBase.addparameter("@stageId",lineUp.Stage.ID);
            DbParameter par2 = DataBase.addparameter("@date",lineUp.Date);
            DbDataReader reader = DataBase.GetData(sql,par1,par2);
            ObservableCollection<LineUp> lst = new ObservableCollection<LineUp>();
            while (reader.Read())
            {
                LineUp lineup = new LineUp() 
                {
                    ID = (int)reader["ID"],
                    Date = Convert.ToDateTime(reader["Date"]),
                    Until = Convert.ToDateTime(reader["EndTime"]),
                    From = Convert.ToDateTime(reader["StartTime"]),
                };
                lineup.Stage = new Stage()
                {
                    ID = (int)reader["StageID"],
                    Name = (string)reader["Name"]
                };
                lst.Add(lineup);
            }
            lst.OrderBy(p=>p.Until);
            return lst;
        }

    }
}
