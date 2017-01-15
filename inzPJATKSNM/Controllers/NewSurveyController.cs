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
    public class NewSurveyController
    {
        public static List<Dzieło> getPhotoList()
        {
            List<Dzieło> photoList = new List<Dzieło>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Id_dzieło,URL,Id_Tech,Id_Kat,Id_Autora,Tytuł from dzieło;";//all photos from database should be visible during creating or editing survey
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dzieło dzielo = new Dzieło();
                                dzielo.Id_dzieło=reader.GetInt32(0);
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
        public static void saveSurveyAndSkładToDB(List<Dzieło> imagesSurveyToDB,String nazwa,String opis,String typ)
        {
            List<int> imageIdList = new List<int>();
            int i = imageIdList.Count();
            int ankietaId=0;
            try
            {
                foreach (Dzieło dzieło in imagesSurveyToDB)
                {
                    imageIdList.Add(getPhotoId(dzieło.URL));
                    i++;
                    if(i > 10)
                    {
                        throw new Exception("Limit zdjęć w ankiecie został przekroczony!");
                    }
                }
                //Tu wywolanie procedury dodajacej ankiete
                saveSurveyToDB(nazwa, opis, typ);
                ankietaId = getNewSurveyId();
                foreach (int zdjecieId in imageIdList)
                {
                    saveSkładToDB(ankietaId, zdjecieId);
                }
            }
            catch (Exception u)
            {
                throw new Exception(u.Message);
            }
           
        }
        public static int getNewSurveyId()
        {
            
            int SurveyId = 0;
            string getSurveyIdCmd=("SELECT TOP 1 * FROM Ankieta ORDER BY Id_ankiety DESC;");
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    SqlCommand command = new SqlCommand(getSurveyIdCmd, Sqlcon);
                    try
                    {
                        Sqlcon.Open();
                        SurveyId = (int)command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Błąd podczas pobierania Id nowo stworzonej ankiety");
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
                    throw new Exception("Błąd wyszukiwania dzieła o url " + url);
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
                catch (Exception e)
                {
                    throw new Exception("Błąd podczas zapisywania składu ankiety");
                }
                finally
                {
                    Sqlcon.Close();
                }
            
            }

        }
        public static void saveSurveyToDB(string nazwa, string opis,string typ)
        {
            
            DateTime data_rozp = DateTime.Now;
            DateTime data_zak = DateTime.Now.AddDays(30);
            string commandTextInsertAnkieta = "Insert into Ankieta (Nazwa,Opis_ankiety,Data_rozp,Data_zak,Id_admin,Active,Typ) values (@nazwa,@opis,@data_rozp,@data_zak,@Id_admin,@Active,@Typ);";
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
                command.Parameters.Add("@Active", SqlDbType.Bit);
                command.Parameters["@Active"].Value = 1;
                command.Parameters.Add("@Typ", SqlDbType.VarChar);
                command.Parameters["@Typ"].Value = typ;
                try
                {
                    Sqlcon.Open();
                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception("Błąd podczas zapisu ankiety do bazy!");
                }
                finally
                {
                    Sqlcon.Close();
                }
               
            }
        }
    }
}