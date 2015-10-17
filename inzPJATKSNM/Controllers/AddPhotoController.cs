using System;
using System.Collections.Generic;
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
            string tmp = "";
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

        public string storePhotoToDb(string URL, int idKategorii, int idKategoii, string rozmiar, int idAutora)
        {
            string wrt = "";
            return wrt;
        }
    }
}