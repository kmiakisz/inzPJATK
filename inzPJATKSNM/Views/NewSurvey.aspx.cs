using inzPJATKSNM.Models;
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
        private static Dictionary<string,Dzieło> surveyPhotos = new Dictionary<string,Dzieło>();
        private static Dictionary<string,Dzieło> photoFromDB = new Dictionary<string,Dzieło>();
        private static List<Dzieło> photoToSurvey = new List<Dzieło>();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                loadPhotosFromDB();      
            }
            
        }
        public Dictionary<string,Dzieło> getPhotoFromDB()
        {
            return photoFromDB;
        }
        public Dictionary<string,Dzieło> getSurveyPhotos()
        {
            return surveyPhotos;
        }
        public void loadPhotosFromDB()
        {
            List<Dzieło> tempList = new List<Dzieło>();
        
            try
            {
                tempList = inzPJATKSNM.Controllers.NewSurveyController.getPhotoList();
                foreach (Dzieło dzielo in tempList)
                {
                    photoFromDB.Add(dzielo.URL, dzielo);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("ShowSurveys.aspx?err=" + ex.Message);
            }
           
        }
    
        [WebMethod]
        public static void addPhoto(String url)
        {
            surveyPhotos.Add(url,photoFromDB[url]);
            photoFromDB.Remove(url);
            


        }
        [WebMethod]
        public static void removePhotoFromSurvey(String url)
        {
            photoFromDB.Add(url, photoFromDB[url]);
            surveyPhotos.Remove(url);
            
        }
     
        protected void AcceptButton_Click(object sender, EventArgs e)
        {
          String nazwa = SurveyNameTextBox.Text;
          String opis = ServeyDescribtionTextBox.Text;
          String typ = TypeDropDownList.SelectedValue.ToString();
          try
          {
              List<Dzieło> photosToSave = new List<Dzieło>();
              foreach (Dzieło dzielo in surveyPhotos.Values)
              {
                  photosToSave.Add(dzielo);
              }
          inzPJATKSNM.Controllers.NewSurveyController.saveSurveyAndSkładToDB(photosToSave, nazwa, opis, typ);
          }
          catch (Exception ex)
          {
              Response.Redirect("ShowSurveys.aspx?err=" + ex.Message);
          }
          
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowSurveys.aspx");
        }

       
       

    }
}