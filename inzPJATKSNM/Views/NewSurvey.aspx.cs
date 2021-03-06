﻿using inzPJATKSNM.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class NewSurvey : System.Web.UI.Page
    {
        private static Dictionary<string, Dzieło> surveyPhotos = new Dictionary<string, Dzieło>();
        private static Dictionary<string, Dzieło> photoFromDB = new Dictionary<string, Dzieło>();
        public static List<Dzieło> filteredList = new List<Dzieło>();
        private static List<Dzieło> photoToSurvey = new List<Dzieło>();
        int katFilter = 0;
        int techFilter = 0;
        int autFilter = 0;
        int loggedId;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();

            if (Session["token"] != null)
            {
                string username = inzPJATKSNM.Controllers.AuthenticationController.getLogin((string)HttpContext.Current.Session["token"]);
                inzPJATKSNM.AuthModels.User user = inzPJATKSNM.Controllers.AuthenticationController.getUser(username);
                loggedId = user.userId;
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
                    photoFromDB.Clear();
                    surveyPhotos.Clear();
                    loadPhotosFromDB();
                    filteredList = getPhotoFromDB();
                }
            }
            else
            {
                Response.Redirect("LogInView.aspx");
            }
        }
        public List<Dzieło> removeFilterList(List<Dzieło> filter, int id_kat)
        {
            return filter;
        }
        public List<Dzieło> filterList(int id_kat, int id_autor, int id_technika, int flag)
        {
            if (id_kat == 0 && id_autor == 0 && id_technika == 0)
            {
                filteredList = getPhotoFromDB();
                return filteredList;
            }
            else
            {
                if (flag == 0)
                {
                    filteredList = getPhotoFromDB();
                }
                for (int i = filteredList.Count - 1; i >= 0; i--)
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
            foreach (Dzieło dzielo in filteredList)
            {
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
            photoFromDB.Clear();

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
            surveyPhotos.Add(url, photoFromDB[url]);
            if (surveyPhotos.Count > 10)
            {
                //alert lub exception ze przekrocono ilosc zdjecc
            }                
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
            Int32 user_id = loggedId;
            try
            {
                List<Dzieło> photosToSave = new List<Dzieło>();
                foreach (Dzieło dzielo in surveyPhotos.Values)
                {
                    photosToSave.Add(dzielo);
                }
                inzPJATKSNM.Controllers.NewSurveyController.saveSurveyAndSkładToDB(photosToSave, nazwa, opis, typ,user_id);
                //Response.Redirect("AdministratorPanel.aspx");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //Response.Redirect("ShowSurveys.aspx?err=" + ex.Message);
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
            filterList(katFilter, autFilter, techFilter, 0);
        }
        protected void technikaChanged(object sender, EventArgs e)
        {
            katFilter = Int32.Parse(DropDownList1.SelectedValue);
            techFilter = Int32.Parse(DropDownList2.SelectedValue);
            autFilter = Int32.Parse(DropDownList3.SelectedValue);
            filterList(katFilter, autFilter, techFilter, 0);
        }
        protected void autorChanged(object sender, EventArgs e)
        {
            katFilter = Int32.Parse(DropDownList1.SelectedValue);
            techFilter = Int32.Parse(DropDownList2.SelectedValue);
            autFilter = Int32.Parse(DropDownList3.SelectedValue);
            filterList(katFilter, autFilter, techFilter, 0);
        }
        protected override void InitializeCulture()
        {
            HttpCookie cookie = Request.Cookies["CultureInfo"];

            if (cookie != null && cookie.Value != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value); ;
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pl-PL");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
            }

            base.InitializeCulture();
        } 



    }
}