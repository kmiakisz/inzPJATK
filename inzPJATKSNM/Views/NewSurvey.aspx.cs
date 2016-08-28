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
        public static List<Dzieło> filteredList = new List<Dzieło>();
        private static List<Dzieło> photoToSurvey = new List<Dzieło>();
        int katFilter =0;
        int techFilter=0;
        int autFilter=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
            {
                HttpContext.Current.Cache.Remove((string)entry.Key);
            }
            // Stop Caching in IE
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            // Stop Caching in Firefox
            Response.Cache.SetNoStore();
            if (!IsPostBack)
            {
                loadPhotosFromDB();  
                filteredList = getPhotoFromDB();
            }
            
        }
        public List<Dzieło> removeFilterList(List<Dzieło> filter,int id_kat)
        {
            return filter;
        }
        public List<Dzieło> filterList(int id_kat,int id_autor,int id_technika,int flag){
            if(id_kat==0 && id_autor==0 && id_technika==0){
                filteredList = getPhotoFromDB();
                return filteredList;
            }else{
                if (flag == 0)
                {
                    filteredList = getPhotoFromDB();
                }
                for (int i = filteredList.Count -1; i >= 0; i--)
                {
                    if (id_kat != 0)
                    {
                        if (filteredList[i].Id_Kat != id_kat)
                        {
                            filteredList.RemoveAt(i);
                            continue;
                        }
                    }
                    if (id_autor != 0)
                    {
                        if (filteredList[i].Id_Autora != id_autor)
                        {
                            filteredList.RemoveAt(i);
                            continue;
                        }
                    }
                    if (id_technika != 0)
                    {
                        if (filteredList[i].Id_Tech != id_technika)
                        {
                            filteredList.RemoveAt(i);
                            continue;
                        }
                    }
                 }   
                     
                }                    
            return filteredList;
        }
        public List<Dzieło> getPhotoFromDB()
        {
            List<Dzieło> dzielaDostp = new List<Dzieło>();
            foreach (Dzieło dzielo in photoFromDB.Values)
            {
                dzielaDostp.Add(dzielo);
            }
          
            return dzielaDostp;
        }
        public List<Dzieło> getFilteredPhoto()
          {
            List<Dzieło> photoList = new List<Dzieło>();
             foreach(Dzieło dzielo in filteredList){
                 photoList.Add(dzielo);
             }
      
            return photoList;
        }
        public List<Dzieło> getSurveyPhotos()
        {
            List<Dzieło> dzielaWankiecie = new List<Dzieło>();
            foreach (Dzieło dzielo in surveyPhotos.Values)
            {
                dzielaWankiecie.Add(dzielo);
            }

            return dzielaWankiecie;
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
            filteredList.Remove(photoFromDB[url]);
            photoFromDB.Remove(url);
           
        }
      
        [WebMethod]
        public static void removePhotoFromSurvey(String url)
        {
            photoFromDB.Add(url, surveyPhotos[url]);
            filteredList.Add(surveyPhotos[url]);
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

        protected void kategoriaChanged(object sender, EventArgs e)
        {
            katFilter = Int32.Parse(DropDownList1.SelectedValue);
            techFilter = Int32.Parse(DropDownList2.SelectedValue);
            autFilter = Int32.Parse(DropDownList3.SelectedValue);
            filterList(katFilter,autFilter,techFilter,0);
        }
        protected void technikaChanged(object sender, EventArgs e)
        {
            katFilter = Int32.Parse(DropDownList1.SelectedValue);
            techFilter = Int32.Parse(DropDownList2.SelectedValue);
            autFilter = Int32.Parse(DropDownList3.SelectedValue);
            filterList(katFilter, autFilter, techFilter,0);
        }
        protected void autorChanged(object sender, EventArgs e)
        {
            katFilter = Int32.Parse(DropDownList1.SelectedValue);
            techFilter = Int32.Parse(DropDownList2.SelectedValue);
            autFilter = Int32.Parse(DropDownList3.SelectedValue);
            filterList(katFilter, autFilter, techFilter,0);
        }

       
       

    }
}