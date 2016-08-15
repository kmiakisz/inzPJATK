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
        public static inzPJATKSNM.Models.Ankieta getSurvey(int id)
        {
            inzPJATKSNM.Models.Ankieta Survey = new inzPJATKSNM.Models.Ankieta();
            
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
        public static void saveEditsurvey(Ankieta ankieta,List<String> zdjPoEdycji)
        {
            List<int> listaIdPoEdycji = new List<int>();
            foreach (string url in zdjPoEdycji)
            {
                listaIdPoEdycji.Add(getPhotoId(url));
            }
            saveOnlyEditSurvey(ankieta);
            saveEditPhotos(listaIdPoEdycji, ankieta.Id_ankiety);
           
        }
        public static void saveOnlyEditSurvey(Ankieta ankieta)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlconn = new SqlConnection(connStr))
            {
                try
                {
                    Sqlconn.Open();
                    SqlCommand command = new SqlCommand("edit_survey", Sqlconn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id_ankiety", SqlDbType.Int).Value = ankieta.Id_ankiety;
                    command.Parameters.Add("@nazwa", SqlDbType.VarChar).Value = ankieta.Nazwa;
                    command.Parameters.Add("@opis", SqlDbType.VarChar).Value = ankieta.Opis_ankiety;
                    command.Parameters.Add("@dataZak", SqlDbType.DateTime).Value = ankieta.Data_zak;
                    command.Parameters.Add("@idMuzyka", SqlDbType.Int).Value = ankieta.Id_Muzyka;
                    command.Parameters.Add("@active", SqlDbType.Int).Value = 1;//do zmiay po zaktualizowaniu modelu
                    command.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error" + ex.Message.ToString());
                }
            }
        }
        public static void saveEditPhotos(List<int> zdjPoEdycji, int id)
        {
            deleteOldPhotos(id);
            foreach (int id_zdj in zdjPoEdycji)
            {
                insertNewPhotos(id_zdj, id);
            }
            

        }
        public static void deleteOldPhotos(int id)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                Sqlcon.Open();
                string query = "delete from Skład where id_ankiety like @ID;";
                using (SqlCommand command = new SqlCommand(query, Sqlcon))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int);
                    command.Parameters["@ID"].Value = id;
                    command.ExecuteNonQuery();
                }
                Sqlcon.Close();
            }
      
        }
        public static void insertNewPhotos(int Id_zdjecia,int id)
        {
            string commandTextInsertSklad = "Insert into Skład values (@Id_ankiety,@Id_zdjęcia);";
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(commandTextInsertSklad, Sqlcon);
                command.Parameters.Add("@Id_ankiety", SqlDbType.Int);
                command.Parameters["@Id_ankiety"].Value = id;
                command.Parameters.Add("@Id_zdjęcia", SqlDbType.Int);
                command.Parameters["@Id_zdjęcia"].Value = Id_zdjecia;
                try
                {
                    Sqlcon.Open();
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Sqlcon.Close();
            }
        }
        public static int getPhotoId(string url)
        {
            int imageId = 0;
            string commandText = "Select Id_dzieło from Dzieło where URL like '%' + @url + '%';";
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(commandText, Sqlcon);
                command.Parameters.Add("@url", SqlDbType.VarChar);
                command.Parameters["@url"].Value = url;
                try
                {
                    Sqlcon.Open();
                    imageId = (int)command.ExecuteScalar();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Sqlcon.Close();
            }
            if (imageId == 0)
            {
                throw new System.ArgumentException("Błąd wyszukiwania ID!");
            }
            else
            {
                return imageId;
            }

        }
        public static Dictionary<Int32,String> getFreePhotos()
        {
            Dictionary<Int32, String> freePhotos = new Dictionary<Int32, String>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "FREE_PHOTOS";
                        var returnParameter1 =  cmd.Parameters.Add("@ID_DZIEŁO", SqlDbType.Int);
                        returnParameter1.Direction = ParameterDirection.ReturnValue;
                        var returnParameter2 = cmd.Parameters.Add("@URL", SqlDbType.VarChar);
                        returnParameter2.Direction = ParameterDirection.ReturnValue;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                freePhotos.Add(reader.GetInt32(0), reader.GetString(1));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }                    
                    conn.Close();
                }
            }
            return freePhotos;
        }
        public static Dictionary<Int32, String> getUsedPhotos(int surveyId)
        {
            Dictionary<Int32, String> usedPhotos = new Dictionary<Int32, String>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "PHOTOS_IN_SURVEY";
                        cmd.Parameters.Add("@ID_SURV", SqlDbType.Int);
                        cmd.Parameters["@ID_SURV"].Value = surveyId;

                        var returnParameter1 = cmd.Parameters.Add("@S.ID_ZDJECIA", SqlDbType.Int);
                        returnParameter1.Direction = ParameterDirection.ReturnValue;
                        var returnParameter2 = cmd.Parameters.Add("@D.URL", SqlDbType.VarChar);
                        returnParameter2.Direction = ParameterDirection.ReturnValue;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usedPhotos.Add(reader.GetInt32(0), reader.GetString(1));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    conn.Close();
                }
            }
            return usedPhotos;
        }
       
    }
}