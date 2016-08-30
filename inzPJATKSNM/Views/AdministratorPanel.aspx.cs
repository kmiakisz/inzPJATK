using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class AdministratorPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(System.Web.HttpContext.Current.User.Identity.Name != null)
            {
                inzPJATKSNM.AuthModels.User user = inzPJATKSNM.Controllers.AuthenticationController.getUser(System.Web.HttpContext.Current.User.Identity.Name);
                if(user.rola.roleId != 1)
                {
                    mgmtDiv.Visible = false;
                }
                else
                {
                    mgmtDiv.Visible = true;
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

        protected void AddAuthor_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/AddAuthor.aspx");
        }

        protected void NewPhoto_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/AddNewPhoto.aspx");
        }

        protected void AddNewSurveyButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/NewSurvey.aspx");
        }

    }
}