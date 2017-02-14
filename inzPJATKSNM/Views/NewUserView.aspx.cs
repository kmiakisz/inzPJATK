using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using inzPJATKSNM.AuthModels;
using System.Threading;
using System.Globalization;
using inzPJATKSNM.Models;
using inzPJATKSNM.Controllers;
using inzPJATKSNM.PrivilegeModels;

namespace inzPJATKSNM.Views
{
    public partial class NewUserView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int loggedId;
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
                if (user.rola.roleId != 1 && !list.Contains(7))
                {
                    Response.Redirect("AdministratorPanel.aspx");
                }
                if(Request.QueryString["err"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "errObjModal();", true);
                }
            }
            else
            {
                Response.Redirect("LogInView.aspx");
            }

             
        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            User user = new User();
            List<String> listaUserow = inzPJATKSNM.Controllers.UserController.getMailList();
            Rola role = new Rola();
            user.login = EmailTxt.Text;
            user.imie = NameTxt.Text;
            user.nazwisko = SurnameTxt.Text;
            role.roleId =Int32.Parse(RoleDDL.SelectedValue);
            user.rola = role;
            try
            {
                if (!listaUserow.Contains(EmailTxt.Text))
                {
                    inzPJATKSNM.Controllers.AuthenticationController.saveUser(user);
                }
                else
                {
                    Response.Redirect("NewUserView.aspx?err=" + "Użytkownik o takim adresie e-mail istnieje już w systemie!", false);
                }
                
            }
            catch (Exception ex)
            {
                Response.Redirect("NewUserView.aspx?err=" + ex,false);
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