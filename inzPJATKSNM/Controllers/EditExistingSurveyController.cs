using inzPJATKSNM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class EditExistingSurveyController
    {
        public static List<String> getSurveyPhotos(int id)
        {
            List<String> photoList = new List<String>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                Sqlcon.Open();
                string query = "select dzielo.url from ankieta outer apply ( select url from dzieło d join skład s on s.id_zdjecia = d.id_dzieło where s.id_ankiety = ankieta.id_ankiety) as dzielo where id_ankiety like @ID;";
                using (SqlCommand command = new SqlCommand(query, Sqlcon))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int);
                    command.Parameters["@ID"].Value = id;
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
        public static Ankieta getSurvey(int id)
        {
            Ankieta Survey = new Ankieta();
            
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                Sqlcon.Open();
                string query = "select Nazwa,Opis_ankiety,Data_rozp,Data_zak from Ankieta where id_ankiety like @ID;";
                using (SqlCommand command = new SqlCommand(query, Sqlcon))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int);
                    command.Parameters["@ID"].Value = id;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            Survey.Nazwa=reader.GetString(0);
                            Survey.Opis_ankiety = reader.GetString(1);
                            Survey.Data_rozp = reader.GetDateTime(2);
                            Survey.Data_zak = reader.GetDateTime(3);
   
                        }
                    }
                }
                Sqlcon.Close();
            }
            return Survey;
        }
    }
}