using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class NewSurveyController
    {
        public static List<String> getPhotoList()
        {
            List<String> photoList = new List<String>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                Sqlcon.Open();
                string query = "SELECT URL FROM Dzieło";
                using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            photoList.Add(reader.GetString(0));
                        }
                    }
                }
                Sqlcon.Close();
            }
            return photoList;
        }
        
    }
}