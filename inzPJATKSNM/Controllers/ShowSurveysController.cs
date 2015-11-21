using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class ShowSurveysController
    {
        

        public static Dictionary<int, String> getAnkiety()
        {
            Dictionary<int, String> ankiety = new Dictionary<int, string>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                Sqlcon.Open();
                string query = "select Id_ankiety,nazwa from ankieta;";
                using (SqlCommand command = new SqlCommand(query, Sqlcon))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ankiety.Add(reader.GetInt32(0),reader.GetString(1));
                        }
                    }
                }
                Sqlcon.Close();
            }
            return ankiety;
        }
    }
}