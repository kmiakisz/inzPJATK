using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using inzPJATKSNM.Models;
using inzPJATKSNM.Controllers;
using inzPJATKSNM.PrivilegeModels;

namespace inzPJATKSNM.Views
{
    public partial class AdministratorPanel : System.Web.UI.Page
    {   static int loggedIn;
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
                loggedIn = user.userId;
                List<Int32> list = UserController.GetUserPrivilegeListIdPerUserId(loggedIn);
                if (user.rola.roleId != 1)//only admin may see user management aplett
                {
                    mgmtDiv.Visible = false;
                }
                else
                {
                    mgmtDiv.Visible = true;
                }
                if (!list.Contains(2))//add photo visibility
                {
                    newPhotoDiv.Visible = false;
                }
                else
                {
                    newPhotoDiv.Visible = true;
                }
                if (!list.Contains(3))//add authoor visibility
                {
                    authorsDiv.Visible = false;
                }
                else
                {
                    authorsDiv.Visible = true;
                }
                if (!list.Contains(4))//user panel visibility
                {
                    userPanelDiv.Visible = false;
                }
                else
                {
                    userPanelDiv.Visible = true;
                }
                if (!list.Contains(5))//create survey view visibility
                {
                    newSurveyDiv.Visible = false;
                }
                else
                {
                    newSurveyDiv.Visible = true;
                }
                if (!list.Contains(9))//all surveys visibility
                {
                    showSurveysDiv.Visible = false;
                }
                else
                {
                    showSurveysDiv.Visible = true;
                }
                if (!list.Contains(10))//statistics visibility
                {
                    statisticsDiv.Visible = false;
                }
                else
                {
                    statisticsDiv.Visible = true;
                }
                if(!list.Contains(11))//category visibility
                {
                    categoryDiv.Visible = false;
                }
                else
                {
                    categoryDiv.Visible = true;
                }
                if (!list.Contains(13))//technique visibility
                {
                    techniqueDiv.Visible = false;
                }
                else
                {
                    techniqueDiv.Visible = true;
                }
            }
            else
            {
                Response.Redirect("LoginView.aspx");

            }

            if (Request.QueryString["err"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "failOpenModal();", true);
            }


        }
        private StateBag PreviousPageViewState
        {
            get
            {
                StateBag returnValue = null;
                if (PreviousPage != null)
                {
                    Object objPreviousPage = (Object)PreviousPage;
                    MethodInfo objMethod = objPreviousPage.GetType().GetMethod("ReturnViewState");//System.Reflection class
                    return (StateBag)objMethod.Invoke(objPreviousPage, null);

                }
                return returnValue;
            }
        }

        protected void AddAuthor_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/AddAuthor.aspx");
        }
        public void Redirect()
        {
            Response.Redirect("AboutUser.aspx?id="+loggedId);
        }

        protected void NewPhoto_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/AddNewPhoto.aspx");
        }
        protected void AboutUserClick(object sender, EventArgs e)
        {
            Response.Redirect("AboutUser.aspx?id=" + loggedId);
        }

        protected void AddNewSurveyButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/.NewSurveyaspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
       [System.Web.Services.WebMethod]
        public static String GetUrl(String dupa)
        {
            return "AboutUser.aspx?id=" + loggedIn;
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