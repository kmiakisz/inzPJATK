using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class AddPhotoController
    {


        internal static String addPhoto(HttpPostedFile file)
        {
            //status zwracany do metody w AddNewPhoto 0 = ok, 1 = nie udalo sie, -1 zmieniono nazwe
            int status = 0;
            //string savePath = "\\inzPJATKSNM\\Images\\SurveyPhotos\\";
            string azurePath = "\\wwwroot\\Images\\SurveyPhotos\\";
            string fileName = inzPJATKSNM.Views.AddNewPhoto.fileupload2.FileName;
            //tu trzeba pobrac z bazy danych najwieksze ID zdjecia (albo i nie )
            string pathToCheck = azurePath + fileName;//
            string tempfileName = "";
            string toDBPath = "";
            try
            {
                if (System.IO.File.Exists(pathToCheck))
                {
                    int counter = 2;
                    while (System.IO.File.Exists(pathToCheck))
                    {
                        // if a file with this name already exists,
                        // prefix the filename with a number.
                        tempfileName = counter.ToString() + fileName;
                        pathToCheck = azurePath + tempfileName;//
                        counter++;
                    }

                    fileName = tempfileName;

                    // Notify the user that the file name was changed.
                    status = -1;
                }
                else
                {
                    // Notify the user that the file was saved successfully.
                    status = 0;
                }
                azurePath += fileName;//
                string startupPath = Path.GetDirectoryName(Path.GetDirectoryName(
                System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
                startupPath += azurePath;//
                string fullPath = startupPath;
                toDBPath = "../Images/SurveyPhotos/" + fileName;
                startupPath = startupPath.Remove(0, 6);
                inzPJATKSNM.Views.AddNewPhoto.fileupload2.SaveAs(startupPath);
            }catch(Exception e){
                throw new Exception("Dodanie zdjęcia się nie powiodło! " + e.Message);
            }
            
            return toDBPath;
        }

        public static String addThumbnail(HttpPostedFile file)
        {
            //status zwracany do metody w AddNewPhoto 0 = ok, 1 = nie udalo sie, -1 zmieniono nazwe
            int status = 0;
            string savePath = "\\inzPJATKSNM\\Images\\SurveyPhotos\\";
            string fileName = inzPJATKSNM.Views.AddNewPhoto.fileupload2.FileName;
            //tu trzeba pobrac z bazy danych najwieksze ID zdjecia (albo i nie )
            string pathToCheck = savePath + fileName;
            string tempfileName = "";
            string toDBPath = "";

            try
            {
                if (System.IO.File.Exists(pathToCheck))
                {
                    int counter = 2;
                    while (System.IO.File.Exists(pathToCheck))
                    {
                        // if a file with this name already exists,
                        // prefix the filename with a number.
                        tempfileName = counter.ToString() + fileName;
                        pathToCheck = savePath + tempfileName;
                        counter++;
                    }

                    fileName = tempfileName;

                    // Notify the user that the file name was changed.
                    status = -1;
                }
                else
                {
                    // Notify the user that the file was saved successfully.
                    status = 0;
                }
                savePath += fileName;
                string startupPath = Path.GetDirectoryName(Path.GetDirectoryName(
                System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
                startupPath += savePath;
                string fullPath = startupPath;
                toDBPath = "../Images/SurveyPhotos/" + fileName;
                startupPath = startupPath.Remove(0, 6);
                inzPJATKSNM.Views.AddNewPhoto.fileupload2.SaveAs(startupPath);
            }
            catch (Exception e)
            {
                throw new Exception("Dodanie miniarutki się nie powiodło!");
            }
            return toDBPath;
        }

        public static void storePhotoToDb(string URL, int idTechnika, int idKategorii, int idAutora, string tytul)
        {
            try
            {
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "insert_dzielo";

                        cmd.Parameters.Add("@URL", SqlDbType.VarChar);
                        cmd.Parameters["@URL"].Value = URL;

                        cmd.Parameters.Add("@technika", SqlDbType.Int);
                        cmd.Parameters["@technika"].Value = idTechnika;

                        cmd.Parameters.Add("@kategoria", SqlDbType.Int);
                        cmd.Parameters["@kategoria"].Value = idKategorii;

                        cmd.Parameters.Add("@autor", SqlDbType.Int);
                        cmd.Parameters["@autor"].Value = idAutora;

                        cmd.Parameters.Add("@tytuł", SqlDbType.VarChar);
                        cmd.Parameters["@tytuł"].Value = tytul;

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Zapis zdjęcia do bazy się nie powiódł!");
            }

        }
        public static List<String> TrimAuthor(String str)
        {
            List<String> listAuth = new List<string>();
            int len = str.Length;
            int len_minus = len - 1;
            int idxOfSpace = str.IndexOf(' ');
            int indexOfSpace_2 = str.IndexOf(' ') + 1;
            string part1 = str.Substring(0, idxOfSpace);
            string part2 = str.Substring(indexOfSpace_2, len - indexOfSpace_2);
            listAuth.Add(part1);
            listAuth.Add(part2);
            return listAuth;
        }

        public static Int32 getIdByNameAndSurname(String name, String surname)
        {
            int authId = 0;
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            string query = "SELECT ID_AUTORA FROM AUTOR WHERE IMIE = '" + name + "' AND NAZWISKO = '" + surname + "';";
            using(SqlConnection Sqlcon = new SqlConnection(connStr)){
                try
                {
                    SqlCommand command = new SqlCommand(query, Sqlcon);
                    Sqlcon.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        authId = Convert.ToInt32(reader[0]);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    Sqlcon.Close();
                }
            }
            return authId;
        }
        public static String CheckPhoto(string name, string url)
        {
            string checkPhoto = "not exists";
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            string query = "select * from dzieło where tytuł = '" + name + "' or url = '" + url + "' or (tytuł = '" + name + "' and url = '" + url + "')";
            using(SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, Sqlcon);
                    Sqlcon.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        checkPhoto = "exists";
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    Sqlcon.Close();
                }
            }
            return checkPhoto;
        }
    }
}