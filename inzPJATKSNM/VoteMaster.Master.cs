using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM
{
    public partial class VoteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
    }
}