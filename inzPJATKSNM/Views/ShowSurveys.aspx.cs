using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using inzPJATKSNM.Models;
using inzPJATKSNM.Controllers;
using inzPJATKSNM.PrivilegeModels;

namespace inzPJATKSNM.Views
{
    public partial class ShowSurveys : System.Web.UI.Page
    {
        Dictionary<int, String> nazwyAnkiet;
        Dictionary<int, String> opisyAnkiet;
        Dictionary<int, String> urlAnkiet;
        public int liczbaAnkiet = 0;
        public int CsVariable = 0;
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
                List<Int32> list = UserController.GetUserPrivilegeListIdPerUserId(loggedId);
                if(!list.Contains(9))
                {
                    Response.Redirect("AdministratorPanel.aspx");
                }
                LoadSurveysFromDb();
                liczbaAnkiet = policzAnkiety();
                if (Request.QueryString["val"] != null)
                {
                    int val = int.Parse(Request.QueryString["val"]);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "mailOpenModal();", true);
                }
                if (Request.QueryString["err"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "failOpenModal();", true);
                }
            }
            else
            {
                Response.Redirect("LogInView.aspx");
            }
        }
        public void LoadSurveysFromDb()
        {
            try
            {
                nazwyAnkiet = inzPJATKSNM.Controllers.ShowSurveysController.getNazwy(loggedId);
                opisyAnkiet = inzPJATKSNM.Controllers.ShowSurveysController.getOpis(loggedId);
                urlAnkiet = inzPJATKSNM.Controllers.ShowSurveysController.getFirstURL(loggedId);
            }
            catch (Exception ex)
            {
                Response.Redirect("AdministratorPanel.aspx?err=" + ex.Message);
            }


        }
        public int policzAnkiety()
        {
            int ilosc = 0;
            ilosc = nazwyAnkiet.Count();
            return ilosc;
        }
        public Dictionary<int, String> getURLDict()
        {
            return urlAnkiet;
        }
        public Dictionary<int, String> getOpisyDict()
        {
            return opisyAnkiet;
        }
        public Dictionary<int, String> getNazwyDict()
        {
            return nazwyAnkiet;
        }
        [WebMethod]
        public static void usunAnkiete(int id)
        {

            inzPJATKSNM.Controllers.ShowSurveysController.removeSurvey(id);

        }
        public void redirectToEdit()
        {
            Response.Redirect("EditExistingSurvey.aspx");
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