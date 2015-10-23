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
    public class AddMusicController
    {
        
        internal static String addMusic(HttpPostedFile file)
        {
            //status zwracany do metody w AddNewPhoto 0 = ok, 1 = nie udalo sie, -1 zmieniono nazwe
            int status = 0;
            string savePath = "\\inzPJATKSNM\\Music\\";
            string fileName = inzPJATKSNM.Views.AddMusic.fileuploadPom.FileName;
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
            inzPJATKSNM.Views.AddMusic.fileuploadPom.SaveAs(startupPath);
            storeMusicToDb(savePath, fileName);
            return savePath;
        }
        public static void storeMusicToDb(string musicPath, string musicName)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    Sqlcon.Open();
                    cmd.Connection = Sqlcon;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "add_muzyka";

                    cmd.Parameters.Add("@url", SqlDbType.VarChar);
                    cmd.Parameters["@url"].Value = musicPath;

                    cmd.Parameters.Add("@tytul", SqlDbType.VarChar);
                    cmd.Parameters["@tytul"].Value = musicName;

                    cmd.ExecuteNonQuery();
                    Sqlcon.Close();

                }
            }
        }
    }
}