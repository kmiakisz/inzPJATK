using inzPJATKSNM.AuthModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class LogInView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();


            if (Request.QueryString["err"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "failOpenModal();", true);
            }
        }

        protected void LogInButton_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.login = LoginTxt.Text;
            user.haslo = TextBox2.Text;
            try
            {
                if (inzPJATKSNM.Controllers.AuthenticationController.checkUser(user, user.haslo))
                {
                    Session["token"] = inzPJATKSNM.Controllers.AuthenticationController.generateToken() + " " + user.login;
                    Response.Redirect("AdministratorPanel.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("/Views/LogInView.aspx?err=" + ex.Message);
            }

            /*
             When user click log in button, system invoke procedure, which update Status for Surveys
             */
            inzPJATKSNM.Controllers.EditExistingSurveyController.invokeUpdateStatus();
        }

        protected void ResetPwdButton_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.login = LoginTxt.Text;
            try
            {
                inzPJATKSNM.Controllers.AuthenticationController.resetPassToken(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

            Response.Redirect("ResetPassword.aspx");
        }
        public StateBag ReturnViewState()
        {
            return ViewState;
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