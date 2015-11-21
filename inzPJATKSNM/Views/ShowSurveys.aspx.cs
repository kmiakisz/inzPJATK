using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class ShowSurveys : System.Web.UI.Page
    {
        Dictionary<int, String> surveysFromDb;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSurveysFromDb();
        }
        public void LoadSurveysFromDb()
        {
            surveysFromDb = inzPJATKSNM.Controllers.ShowSurveysController.getAnkiety();
        }

        protected void GridViewSurveys_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("/Views/EditSurvey.aspx");
        }
    }
}