using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
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
        public int CsVariable = 0;
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
        [WebMethod]
        public static void usunAnkiete(int id)
        {
            inzPJATKSNM.Controllers.ShowSurveysController.removeSurvey(id);
           
        }
        public void redirectToEdit()
        {
            Response.Redirect("EditExistingSurvey.aspx");
        }
    }
}