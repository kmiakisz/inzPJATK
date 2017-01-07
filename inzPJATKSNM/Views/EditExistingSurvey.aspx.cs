using inzPJATKSNM.Controllers;
using inzPJATKSNM.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class EditExistingSurvey : System.Web.UI.Page
    {
        public inzPJATKSNM.Models.Ankieta ankieta = new inzPJATKSNM.Models.Ankieta();
        List<String> listaURLZdjec = new List<String>();
        public Dictionary<Int32, String> freePhotos { get; set; }
        public Dictionary<Int32, String> usedPhotos { get; set; }
        public Int32 currentKey { get; set; }
        public String currentValue { get; set; }
        private static Dictionary<string, Dzieło> surveyPhotos = new Dictionary<string, Dzieło>();
        private static Dictionary<string, Dzieło> photoFromDB = new Dictionary<string, Dzieło>();
        public static List<Dzieło> filteredList = new List<Dzieło>();
        private static List<Dzieło> photoToSurvey = new List<Dzieło>();
        int katFilter = 0;
        int techFilter = 0;
        int autFilter = 0;
        public static String err;
        int surveyId;

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();

            if (Session["token"] != null)
            {
                foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
                {
                    HttpContext.Current.Cache.Remove((string)entry.Key);
                }

                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

                Response.Cache.SetNoStore();

                if (err != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "failOpenModal();", true);
                }
                if (!IsPostBack)
                {
                    if (Session["token"] != null)
                    {
                        int loggedId;
                        string username = inzPJATKSNM.Controllers.AuthenticationController.getLogin((string)HttpContext.Current.Session["token"]);
                        inzPJATKSNM.AuthModels.User user = inzPJATKSNM.Controllers.AuthenticationController.getUser(username);
                        loggedId = user.userId;
                        List<Int32> list = UserController.GetUserPrivilegeListIdPerUserId(loggedId);
                        if (!list.Contains(6))
                        {
                            Response.Redirect("AdministratorPanel.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("LogInView.aspx");
                    }
                    if (Request.QueryString["Id"] != null)
                    {
                        currentValue = "";
                        currentKey = new Int32();
                        int id = int.Parse(Request.QueryString["Id"]);
                        surveyId = int.Parse(Request.QueryString["Id"]);
                        try
                        {
                            if (surveyPhotos.Count == 0)
                            {
                                surveyPhotos = inzPJATKSNM.Controllers.EditExistingSurveyController.getSurveyPhotos(id);
                            }
                           /* if (photoFromDB.Count == 0)
                            {
                                loadPhotosFromDB();
                            }
                            * */
                            loadPhotosFromDB();
                            if (filteredList.Count == 0)
                            {
                                filteredList = getPhotoFromDB();
                            }


                            ankieta = inzPJATKSNM.Controllers.EditExistingSurveyController.getSurvey(id);
                        }
                        catch (Exception ex)
                        {
                            Response.Redirect("ShowSurveys.aspx?err=" + ex.Message);
                        }

                        EndDateTxt.Text = ankieta.Data_zak.ToString();
                        SurveyNameTextBox1.Text = ankieta.Nazwa;
                        ServeyDescribtionTextBox1.Text = ankieta.Opis_ankiety;
                        ZakDatLbl.Text = ZakDatLbl.Text + ankieta.Data_rozp.ToString();
                        string status = inzPJATKSNM.Controllers.EditExistingSurveyController.getStatus(surveyId);
                        if (status.Equals("True"))
                        {
                            Label2.Text += " Aktywna";
                        }
                        else
                        {
                            Label2.Text += " Nie aktywna";
                        }
                        


                    }
                    else
                    {
                        Response.Redirect("ShowSurveys.aspx?err=PusteId");
                    }
                }

            }
            else
            {
                Response.Redirect("LogInView.aspx");
            }
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
        public List<Dzieło> getFilteredPhoto()
        {
            List<Dzieło> photoList = new List<Dzieło>();
            foreach (Dzieło dzielo in filteredList)
            {
                photoList.Add(dzielo);
            }

            return photoList;
        }
        public void loadPhotosFromDB()
        {
            List<Dzieło> tempList = new List<Dzieło>();

            try
            {
                if (photoFromDB.Count == 0)
                {
                    tempList = inzPJATKSNM.Controllers.EditExistingSurveyController.getPhotoList(int.Parse(Request.QueryString["Id"]));
                    foreach (Dzieło dzielo in tempList)
                    {
                        photoFromDB.Add(dzielo.URL, dzielo);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("ShowSurveys.aspx?err=" + ex.Message,false);
            }



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
        protected void AcceptButton_Click(object sender, EventArgs e)
        {

            ankieta.Id_ankiety = int.Parse(Request.QueryString["Id"]);
            ankieta.Data_zak = DateTime.Parse(EndDateTxt.Text);
            ankieta.Nazwa = SurveyNameTextBox1.Text;
            ankieta.Opis_ankiety = ServeyDescribtionTextBox1.Text;
            ankieta.Type = TypeDropDownList.Text;
            try
            {
                inzPJATKSNM.Controllers.EditExistingSurveyController.saveEditsurvey(ankieta, surveyPhotos);
            }
            catch (Exception ex)
            {
                Response.Redirect("ShowSurveys.aspx?err=" + ex.Message);

            }
            Response.Redirect("ShowSurveys.aspx");
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowSurveys.aspx");
        }


        [WebMethod]
        public static void removePhotoFromSurvey(String url)
        {
            if (surveyPhotos.Count != 1)
            {
                photoFromDB.Add(url, surveyPhotos[url]);
                filteredList.Add(surveyPhotos[url]);

                surveyPhotos.Remove(url);
            }
            else
            {
                err = "Nie można usunąć wszystkich zdjęć z ankiety!!!";
            }

        }
        [WebMethod]
        public static void AddPhoto(String url)
        {
            surveyPhotos.Add(url, photoFromDB[url]);
            filteredList.Remove(photoFromDB[url]);
            photoFromDB.Remove(url);
        }
        protected void kategoriaChanged(object sender, EventArgs e)
        {
            katFilter = Int32.Parse(DropDownList2.SelectedValue);
            techFilter = Int32.Parse(DropDownList3.SelectedValue);
            autFilter = Int32.Parse(DropDownList4.SelectedValue);
            filterList(katFilter, autFilter, techFilter, 0);
        }
        protected void technikaChanged(object sender, EventArgs e)
        {
            katFilter = Int32.Parse(DropDownList2.SelectedValue);
            techFilter = Int32.Parse(DropDownList3.SelectedValue);
            autFilter = Int32.Parse(DropDownList4.SelectedValue);
            filterList(katFilter, autFilter, techFilter, 0);
        }
        protected void autorChanged(object sender, EventArgs e)
        {
            katFilter = Int32.Parse(DropDownList2.SelectedValue);
            techFilter = Int32.Parse(DropDownList3.SelectedValue);
            autFilter = Int32.Parse(DropDownList4.SelectedValue);
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