using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class ShowSurveysController
    {


        public static Dictionary<int, String> getNazwy(int user_id)
        {
           
            Dictionary<int, String> nazwy = new Dictionary<int, string>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Id_ankiety,nazwa from ankieta where id_admin = " + user_id + ";";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nazwy.Add(reader.GetInt32(0), reader.GetString(1));
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getNazwy", e.Message);
                throw new Exception("Błąd podczas pobierania nazw dzieł ");
            }
           
            return nazwy;
        }
        public static Dictionary<int, String> getOpis(int user_id)
        {
            Dictionary<int, String> opisy = new Dictionary<int, string>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Id_ankiety,Opis_ankiety from ankieta where id_admin = " + user_id + ";";
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
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getOpis", e.Message);
                throw new Exception("Błąd podczas pobierania opisów");
            }
           
            return opisy;
        }
        public static Dictionary<int, String> getFirstURL(int user_id)
        {
            Dictionary<int, String> urle = new Dictionary<int, string>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select ankieta.id_ankiety, dzielo.url from ankieta outer apply ( select top(1) url from dzieło d join skład s on s.id_zdjecia = d.id_dzieło where s.id_ankiety = ankieta.id_ankiety) as dzielo where id_admin = " + user_id + ";";
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
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getFirstURL", e.Message);
                throw new Exception("Błąd podczas pobierania urla ");
            }
           
            return urle;
        }
        public static void removeSurvey(int id)
        {
            
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "DeleteAnkieta";

                        cmd.Parameters.Add("@idAnkiety", SqlDbType.Int);
                        cmd.Parameters["@idAnkiety"].Value = id;

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();
                    }
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("removeSurvey", e.Message);
                throw new Exception("Błąd podczas usuwania ankiety o Id " + id);
            }
           
        }
    }
}