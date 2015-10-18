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
            string savePath = "\\inzPJATKSNM\\Images\\SurveyPhotos\\";
            string fileName = inzPJATKSNM.Views.AddNewPhoto.fileupload2.FileName;
            //tu trzeba pobrac z bazy danych najwieksze ID zdjecia (albo i nie )
            string pathToCheck = savePath + fileName;
            string tempfileName = "";
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
            startupPath = startupPath.Remove(0, 6);
            inzPJATKSNM.Views.AddNewPhoto.fileupload2.SaveAs(startupPath);
            return savePath;
        }

        public static void storePhotoToDb(string URL, int idTechnika, int idKategorii, int idAutora)
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

                   cmd.Parameters.Add("@URL",SqlDbType.VarChar);
                   cmd.Parameters["@URL"].Value = URL;

                   cmd.Parameters.Add("@technika", SqlDbType.Int);
                   cmd.Parameters["@technika"].Value = idTechnika;

                   cmd.Parameters.Add("@kategoria", SqlDbType.Int);
                   cmd.Parameters["@kategoria"].Value = idKategorii;

                   cmd.Parameters.Add("@autor", SqlDbType.Int);
                   cmd.Parameters["@autor"].Value = idAutora;

                   cmd.ExecuteNonQuery();
                   Sqlcon.Close();
                   
                }
            }
        }
    }
}