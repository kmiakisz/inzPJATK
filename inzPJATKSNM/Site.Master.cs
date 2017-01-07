using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace inzPJATKSNM
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           // System.Threading.Timer t = new System.Threading.Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            if (Session["token"] != null)
            {
                string username = inzPJATKSNM.Controllers.AuthenticationController.getLogin((string)HttpContext.Current.Session["token"]);
                Label1.Visible = true;               
                Button1.Visible = true;
                AdminPanel.Visible = true;
                if (!IsPostBack)
                {
                    Label1.Text += username;
                }

            }
            else
            {
                Label1.Visible = false;
                Button1.Visible = false;
                AdminPanel.Visible = false;
            }
            
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Remove("Auth");
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("LogInView.aspx");
        }

        protected void EngButton_Click(object sender, ImageClickEventArgs e)
        {
            string selectedLanguage = "en-GB";
            HttpCookie cookie = new HttpCookie("CultureInfo");
            cookie.Value = selectedLanguage;
            Response.Cookies.Add(cookie);
            Response.Redirect(Request.RawUrl);
        }

        protected void PolButton_Click(object sender, ImageClickEventArgs e)
        {
            string selectedLanguage = "pl-PL";
            HttpCookie cookie = new HttpCookie("CultureInfo");
            cookie.Value = selectedLanguage;
            Response.Cookies.Add(cookie);
            Response.Redirect(Request.RawUrl);
        }
      /*  public static void Callback(object state)
        {
            System.Diagnostics.Debug.Write("SIEMA");
        }*/
    }

}