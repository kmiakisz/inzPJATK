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
            mgmtDiv.Visible = false;
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