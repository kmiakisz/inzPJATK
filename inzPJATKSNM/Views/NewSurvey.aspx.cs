using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class NewSurvey : System.Web.UI.Page
    {
        private List<String> photoFromDB;
        private static List<String> photoToSurvey = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            loadPhotosFromDB();
            

        }
        
        public void loadPhotosFromDB()
        {

            photoFromDB = inzPJATKSNM.Controllers.NewSurveyController.getPhotoList();
        }
        [WebMethod]
        public static void addToPhotoToSurvey(String url)
        {
            if (photoToSurvey.Contains(url))
            {
                removePhotoFromSurvey(url);
            }
            else
            {
                photoToSurvey.Add(url);
            }
            
            
        }
        [WebMethod]
        public static void removePhotoFromSurvey(String url)
        {
            photoToSurvey.Remove(url);
        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            List<String> tempList = photoToSurvey;
          int musicId = int.Parse(MusicDropDownList.SelectedValue);
          String nazwa = SurveyNameTextBox.Text;
          String opis = ServeyDescribtionTextBox.Text;
          inzPJATKSNM.Controllers.NewSurveyController.saveSurveyAndSkładToDB(tempList, musicId, nazwa, opis);
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {

        }

       
       

    }
}