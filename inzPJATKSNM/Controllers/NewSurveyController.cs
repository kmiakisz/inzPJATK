using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        public static void saveSurveyAndSkładToDB(List<String> imagesSurveyToDB,int musicID,String nazwa,String opis,String typ)
        {
            List<int> imageIdList = new List<int>();

            int ankietaId=0;
            
            foreach (string url in imagesSurveyToDB)
            {
                imageIdList.Add(getPhotoId(url));
            }
            //Tu wywolanie procedury dodajacej ankiete
            saveSurveyToDB(musicID, nazwa, opis,typ);
            ankietaId = getNewSurveyId();
            foreach (int zdjecieId in imageIdList)
            {
                saveSkładToDB(ankietaId, zdjecieId);
            }
        }
        public static int getNewSurveyId()
        {
            int SurveyId = 0;
            string getSurveyIdCmd=("SELECT TOP 1 * FROM Ankieta ORDER BY Id_ankiety DESC;");
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(getSurveyIdCmd, Sqlcon);
                try
                {
                    Sqlcon.Open();
                    SurveyId = (int) command.ExecuteScalar();
                }
                 catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            
                Sqlcon.Close();
            }
            return SurveyId;
        }
        public static int getPhotoId(string url)
        {
            int imageId =0;
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
                    //SqlDataReader rd = command.ExecuteReader();

                    //if (rd.HasRows)
                    //    imageId = (int)rd.GetInt32(1);

                    //rd.Close();
                    imageId = (int) command.ExecuteScalar();

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
            }else{
                return imageId;
            }
            
        }
        public static void saveSkładToDB(int Id_ankiety,int Id_zdjęcia)
        {
            string commandTextInsertSklad = "Insert into Skład values (@Id_ankiety,@Id_zdjęcia);";
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(commandTextInsertSklad, Sqlcon);
                command.Parameters.Add("@Id_ankiety", SqlDbType.Int);
                command.Parameters["@Id_ankiety"].Value = Id_ankiety;
                command.Parameters.Add("@Id_zdjęcia", SqlDbType.Int);
                command.Parameters["@Id_zdjęcia"].Value = Id_zdjęcia;
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
        public static void saveSurveyToDB(int musicID, string nazwa, string opis,string typ)
        {
            DateTime data_rozp = DateTime.Now;
            DateTime data_zak = DateTime.Now.AddDays(30);
            string commandTextInsertAnkieta = "Insert into Ankieta (Nazwa,Opis_ankiety,Data_rozp,Data_zak,Id_admin,Id_Muzyka,Active) values (@nazwa,@opis,@data_rozp,@data_zak,@Id_admin,@Id_Muzyka,@Active);";
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(commandTextInsertAnkieta, Sqlcon);
                command.Parameters.Add("@nazwa", SqlDbType.VarChar);
                command.Parameters["@nazwa"].Value = nazwa;
                command.Parameters.Add("@opis", SqlDbType.VarChar);
                command.Parameters["@opis"].Value = opis;
                command.Parameters.Add("@data_rozp", SqlDbType.DateTime);
                command.Parameters["@data_rozp"].Value = data_rozp;
                command.Parameters.Add("@data_zak", SqlDbType.DateTime);
                command.Parameters["@data_zak"].Value = data_zak;
                command.Parameters.Add("@Id_admin", SqlDbType.Int);
                command.Parameters["@Id_admin"].Value = 1;
                command.Parameters.Add("@Id_Muzyka", SqlDbType.Int);
                command.Parameters["@Id_Muzyka"].Value = musicID;
                command.Parameters.Add("@Active", SqlDbType.Bit);
                command.Parameters["@Active"].Value = 1;
                command.Parameters.Add("@Typ", SqlDbType.VarChar);
                command.Parameters["@Typ"].Value = typ;
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
        
    }
}