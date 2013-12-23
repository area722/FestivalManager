using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace eindopdracht.model
{
    public class Stage
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

        public static ObservableCollection<Stage> GetStages()
        {
            string sql = "SELECT * FROM Stages";
            ObservableCollection<Stage> lst = new ObservableCollection<Stage>();
            DbDataReader reader = DataBase.GetData(sql);
            while (reader.Read())
            {
                Stage stage = new Stage() 
                {
                    ID = (int)reader[0],
                    Name = (string)reader[1]
                };
                lst.Add(stage);
            }
            return lst;
        }

        public static void AddStage(string stage)
        {
            string sql = "INSERT INTO Stages (Name) VALUES (@Name)";
            DbParameter par = DataBase.addparameter("@Name",stage);
            DataBase.modifyData(sql,par);
        }

        public static void EditStage(Stage stage)
        {
            string sql = "UPDATE Stages SET Name=@Name WHERE ID=@Id";
            DbParameter par = DataBase.addparameter("@Name", stage.Name);
            DbParameter par1 = DataBase.addparameter("@Id", stage.ID);
            DataBase.modifyData(sql, par,par1);
        }

        public static void DeleteStage(Stage stage)
        {
            string sql = "DELETE FROM Stages WHERE ID=@stageID";
            DbParameter par = DataBase.addparameter("@stageID",stage.ID);
            DataBase.modifyData(sql,par);

            //ook uit de lineup verwijderen
            string sql1 = "DELETE FROM LineUp WHERE Stage=@stageID";
            DbParameter par1 = DataBase.addparameter("@stageID", stage.ID);
            DataBase.modifyData(sql1,par1);
        }
    }
}
