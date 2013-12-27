using DBHelper;
using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace REST_service.Models.DAL
{
    public class FestRepo
    {
        public static String GetFestName()
        {
            string sql = "SELECT * FROM FestName";
            DbDataReader reader = Database.GetData(sql);
            reader.Read();
            return (string)reader["FestName"];
        }
    }
}