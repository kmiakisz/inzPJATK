using inzPJATKSNM.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ChangesButton_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.login = Login1Txt.Text;
            user.token = TokenTxt.Text;
            inzPJATKSNM.Controllers.AuthenticationController.resetPass(user, user.token);
            Response.Redirect("LogInView.aspx");
        }
    }
}