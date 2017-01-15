using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using inzPJATKSNM.Models;
using inzPJATKSNM.Controllers;
using inzPJATKSNM.PrivilegeModels;

namespace inzPJATKSNM.Views
{
    public partial class AddAuthor : System.Web.UI.Page
    {
        String name = "", surname = "";
        int Id_nar, Id_plec, Id_epoka;
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

                if(!list.Contains(3))
                {
                    Response.Redirect("AdministratorPanel.aspx");
                }
            }
            else
            {
                Response.Redirect("LogInView.aspx");
            }
        }

        protected void DodajButton_Click(object sender, EventArgs e)
        {
            name = AuthorNameTextBox.Text;
            surname = AuthorSurnameTextBox.Text;
            Id_nar = int.Parse(NationalityDropDownList.SelectedValue);
            Id_plec = int.Parse(PlecDropDownList.SelectedValue);
            Id_epoka = int.Parse(EpokaDropDownList1.SelectedValue);
            try
            {

                inzPJATKSNM.Controllers.AutorController.addNewAuthor(name, surname, Id_nar, Id_plec, Id_epoka);
                Response.Redirect("AdministratorPanel.aspx");
            }
            catch (Exception ex)
            {
                Response.Redirect("ShowSurveys.aspx?val=" + ex.Message);
            }

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