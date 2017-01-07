using inzPJATKSNM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class EditExistingSurveyController
    {
        public static Dictionary<string, Dzieło> getSurveyPhotos(int id)
        {
            Dictionary<string, Dzieło> photoList = new Dictionary<string, Dzieło>();
            try
            {
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Id_dzieło,URL,Id_Tech,Id_Kat,Id_Autora,Tytuł from ankieta outer apply ( select Id_dzieło,URL,Id_Tech,Id_Kat,Id_Autora,Tytuł from dzieło d join skład s on s.id_zdjecia = d.id_dzieło where s.id_ankiety = ankieta.id_ankiety) as dzielo where id_ankiety like @ID;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int);
                        command.Parameters["@ID"].Value = id;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dzieło dzielo = new Dzieło();
                                dzielo.Id_dzieło = reader.GetInt32(0);
                                dzielo.URL = reader.GetString(1);
                                dzielo.Id_Tech = reader.GetInt32(2);
                                dzielo.Id_Kat = reader.GetInt32(3);
                                dzielo.Id_Autora = reader.GetInt32(4);
                                dzielo.tytuł = reader.GetString(5);
                                photoList.Add(dzielo.URL, dzielo);
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Pobieranie zdjęć się nie powiodło!");
            }



            return photoList;
        }
        public static inzPJATKSNM.Models.Ankieta getSurvey(int id)
        {
            inzPJATKSNM.Models.Ankieta Survey = new inzPJATKSNM.Models.Ankieta();
            try
            {
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

                                Survey.Nazwa = reader.GetString(0);
                                Survey.Opis_ankiety = reader.GetString(1);
                                Survey.Data_rozp = reader.GetDateTime(2);
                                Survey.Data_zak = reader.GetDateTime(3);

                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Nie udało się pobrać ankiety o ID " + id);
            }

            return Survey;
        }
        public static void saveEditsurvey(Ankieta ankieta, Dictionary<string, Dzieło> zdjPoEdycji)
        {

            List<int> listaIdPoEdycji = new List<int>();
            try
            {
                foreach (string url in zdjPoEdycji.Keys)
                {
                    listaIdPoEdycji.Add(getPhotoId(url));
                }
                saveOnlyEditSurvey(ankieta);
                saveEditPhotos(listaIdPoEdycji, ankieta.Id_ankiety);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


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
                    command.Parameters.Add("@active", SqlDbType.Int).Value = 1;//do zmiay po zaktualizowaniu modelu
                    command.Parameters.Add("@Typ", SqlDbType.VarChar).Value = ankieta.Type;
                    command.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    throw new Exception("Zapis ankiety o ID " + ankieta.Id_ankiety + " nie powiódł się");
                }
            }
        }
        public static void saveEditPhotos(List<int> zdjPoEdycji, int id)
        {
            try
            {
                deleteOldPhotos(id);
                foreach (int id_zdj in zdjPoEdycji)
                {
                    insertNewPhotos(id_zdj, id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }
        public static void deleteOldPhotos(int id)
        {
            try
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
            catch (Exception e)
            {
                throw new Exception("Wystąpił błąd podczas usuwania starych zdjęć");
            }


        }
        public static void insertNewPhotos(int Id_zdjecia, int id)
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
                    throw new Exception("Błąd poczas dodawania nowych zdjęć");
                }
                finally
                {
                    Sqlcon.Close();
                }

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
                    throw new Exception("Błąd wyszukiwania zdjęcia o URl " + url);
                }
                finally
                {
                    Sqlcon.Close();
                }

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
        public static Dictionary<Int32, String> getFreePhotos()
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
                        var returnParameter1 = cmd.Parameters.Add("@ID_DZIEŁO", SqlDbType.Int);
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
                        throw new Exception("Błąd poczas pobierania dostępnych zdjęć!");
                    }
                    finally
                    {
                        conn.Close();
                    }

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
                        throw new Exception("Błąd poczas pobierania aktualnie używanych zdjęć");
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
            return usedPhotos;
        }
        public static List<Dzieło> getPhotoList(int SurveyId)
        {
            List<Dzieło> photoList = new List<Dzieło>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Id_dzieło,URL,Id_Tech,Id_Kat,Id_Autora,Tytuł from dzieło where id_dzieło not in (select id_zdjecia from skład where id_ankiety = @ID);";//all photos from database should be visible during creating or editing survey
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int);
                        command.Parameters["@ID"].Value = SurveyId;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dzieło dzielo = new Dzieło();
                                dzielo.Id_dzieło = reader.GetInt32(0);
                                dzielo.URL = reader.GetString(1);
                                dzielo.Id_Tech = reader.GetInt32(2);
                                dzielo.Id_Kat = reader.GetInt32(3);
                                dzielo.Id_Autora = reader.GetInt32(4);
                                dzielo.tytuł = reader.GetString(5);
                                photoList.Add(dzielo);
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Błąd poczas pobierania listy dzieł");
            }
            return photoList;
        }

        public static String getStatus(int surveyId)
        {
            String status = "";
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                Sqlcon.Open();
                string query = "select Active from ankieta where id_ankiety = @ID";
                using (SqlCommand command = new SqlCommand(query, Sqlcon))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int);
                    command.Parameters["@ID"].Value = surveyId;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            status = reader.GetBoolean(0).ToString();
                        }
                    }
                }
                Sqlcon.Close();
            }
            return status;
        }

        public static void invokeUpdateStatus()
        {
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
                        cmd.CommandText = "updateSurveyStatus";
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }

        }
        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        public static void updateSurveyStatusAndReturnVotes()
        {
            List<String> list = new List<String>();
            List<int> alreadyUsedIds = new List<int>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {

                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "updateSurveyStatusAndReturnVotes";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (isValidEmail(reader.GetString(1)))
                                {
                                    list.Add(reader.GetInt32(0).ToString());
                                    list.Add(reader.GetString(1));
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }

                conn.Close();
                int i =0;
                if (list.Count > 0)
                {
                    for (i = 0; i < list.Count; i += 2)
                    {
                        try
                        {
                            inzPJATKSNM.Controllers.Statistic s = new Controllers.Statistic();
                            s = inzPJATKSNM.Controllers.StatisticsController.StatisticPerSurvey(Int32.Parse(list[i]));
                            string surveyName = inzPJATKSNM.Controllers.StatisticsController.GetSurveyName(Int32.Parse(list[i]));

                            String subject = "Podsumowanie zakończonej ankiety : " + surveyName + " w systemie ankiet SNM";
                            String body = "Ankieta o nazwie : " + surveyName + " własnie się zakończyła.\nWzięło w niej udział " + s.NumOfVotersOnSurvey + " osób. Statystyki odnośnie dzieł przedstawiają się następująco:\n-Nazwa zdjęcia z największą sumą głosów : " + s.ImgMaxVoteNumName + "\n-Nazwa zdjęcia z najmniejszą sumą głosów : " + s.ImgMinVoteNumName + "\n\nDziękujemy za wzięcie udziału w ankiecie i poświęcony czas. Zapraszamy do uczestniczenia w kolejnych ankietach.\n\nPozdrawiamy\n~zespoł SNM.";
                            MailMessage message = new MailMessage();
                            message.From = new MailAddress("ankietySNM@gmail.com");
                            message.To.Add(new MailAddress(list[i + 1]));
                            message.Subject = subject;
                            message.Body = body;

                            SmtpClient client = new SmtpClient();
                            client.Send(message);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        if (!alreadyUsedIds.Contains(Int32.Parse(list[i])))
                        {
                            inzPJATKSNM.Controllers.SurveyController.updateMailSend(Int32.Parse(list[i]));
                            alreadyUsedIds.Add(Int32.Parse(list[i]));
                        }
                       
                    }
                   
                }
            }
        }

    }
}