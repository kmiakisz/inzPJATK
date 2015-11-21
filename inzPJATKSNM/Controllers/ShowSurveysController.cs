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
        

        public static Dictionary<int, String> getNazwy()
        {
            Dictionary<int, String> nazwy = new Dictionary<int, string>();
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
                            nazwy.Add(reader.GetInt32(0),reader.GetString(1));
                        }
                    }
                }
                Sqlcon.Close();
            }
            return nazwy;
        }
        public static Dictionary<int, String> getOpis()
        {
            Dictionary<int, String> opisy = new Dictionary<int, string>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                Sqlcon.Open();
                string query = "select Id_ankiety,Opis_ankiety from ankieta;";
                using (SqlCommand command = new SqlCommand(query, Sqlcon))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            opisy.Add(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                }
                Sqlcon.Close();
            }
            return opisy;
        }
        public static Dictionary<int, String> getFirstURL()
        {
            Dictionary<int, String> urle = new Dictionary<int, string>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                Sqlcon.Open();
                string query = "select ankieta.Id_ankiety,url from dzieło inner join skład on Id_dzieło = Id_zdjecia inner join ankieta on ankieta.Id_ankiety = skład.Id_ankiety;";
                using (SqlCommand command = new SqlCommand(query, Sqlcon))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            urle.Add(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                }
                Sqlcon.Close();
            }
            return urle;
        }
    }
}