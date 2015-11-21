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
        Dictionary<int, String> nazwyAnkiet;
        Dictionary<int, String> opisyAnkiet;
        Dictionary<int, String> urlAnkiet;
        public int liczbaAnkiet = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSurveysFromDb();
            liczbaAnkiet = policzAnkiety();
        }
        public void LoadSurveysFromDb()
        {
            nazwyAnkiet = inzPJATKSNM.Controllers.ShowSurveysController.getNazwy();
            opisyAnkiet = inzPJATKSNM.Controllers.ShowSurveysController.getOpis();
            urlAnkiet = inzPJATKSNM.Controllers.ShowSurveysController.getFirstURL();

        }
        public int policzAnkiety()
        {
            int ilosc = 0;
            ilosc = nazwyAnkiet.Count();
            return ilosc;
        }

    }
}