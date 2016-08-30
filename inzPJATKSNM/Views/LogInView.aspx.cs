using inzPJATKSNM.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class LogInView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogInButton_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.login = LoginTxt.Text;
            user.haslo = TextBox2.Text;
            if (inzPJATKSNM.Controllers.AuthenticationController.checkUser(user, user.haslo))
            {
                Response.Redirect("ShowSurveys.aspx");
            }
        }

        protected void ResetPwdButton_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.login = LoginTxt.Text;
            inzPJATKSNM.Controllers.AuthenticationController.resetPassToken(user);
            Response.Redirect("ResetPassword.aspx");
        }
    }
}