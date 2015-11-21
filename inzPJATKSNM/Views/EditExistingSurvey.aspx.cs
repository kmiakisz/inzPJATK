using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class EditExistingSurvey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowSurveys.aspx");
        }
    }
}