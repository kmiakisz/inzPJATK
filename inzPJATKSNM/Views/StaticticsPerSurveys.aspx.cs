using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class StaticticsPerServey : System.Web.UI.Page
    {
        Dictionary<int, String> nazwyAnkiet;
        Dictionary<int, String> opisyAnkiet;
        Dictionary<int, String> urlAnkiet;
        public string val;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSurveysFromDb();
            if (Request.QueryString["val"] != null)
            {

            }
        }
        public void LoadSurveysFromDb()
        {
            nazwyAnkiet = inzPJATKSNM.Controllers.SurveyController.getNazwy();
            opisyAnkiet = inzPJATKSNM.Controllers.SurveyController.getOpis();
            urlAnkiet = inzPJATKSNM.Controllers.SurveyController.getFirstURL();

        }
        public Dictionary<int, String> getURLDict()
        {
            return urlAnkiet;
        }
        public Dictionary<int, String> getOpisyDict()
        {
            return opisyAnkiet;
        }
        public Dictionary<int, String> getNazwyDict()
        {
            return nazwyAnkiet;
        }
    }
}