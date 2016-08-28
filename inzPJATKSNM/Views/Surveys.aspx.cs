using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class Surveys : System.Web.UI.Page
    {
        Dictionary<int, String> nazwyAnkiet;
        Dictionary<int, String> opisyAnkiet;
        Dictionary<int, String> urlAnkiet;
        public static string val;
        public int liczbaAnkiet = 0;
        public int CsVariable = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSurveysFromDb();
            liczbaAnkiet = policzAnkiety();
            if (Request.QueryString["val"] != null)
            {
                val = Request.QueryString["val"];
                if(val.Equals("UsedToken")){
                    val = "Jednorazowy link już został użyty!";
                }else if(val.Equals("EmptyToken")){
                    val = "Sprawdź czy link do ankiety jest poprawny!";
                }else if(val.Equals("BlockedIp")){
                    val = "W ankiecie można wziąć udział tylko raz!";
                }
                else
                {
                    if (Request.QueryString["val"].Substring(0, 6).Equals("thanks"))
                    {
                        val = Request.QueryString["val"];
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "subscriptionOpenModal();", true);
                    }
                    else
                    {
                        val = "Nieprawidłowy identyfikator ankiety!";
                    }
                    
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "mailOpenModal();", true);
            }
            if (Request.QueryString["err"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "failOpenModal();", true);
            }
        }
        public void LoadSurveysFromDb()
        {
            nazwyAnkiet = inzPJATKSNM.Controllers.SurveyController.getNazwy();
            opisyAnkiet = inzPJATKSNM.Controllers.SurveyController.getOpis();
            urlAnkiet = inzPJATKSNM.Controllers.SurveyController.getFirstURL();

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
        public static void addEmail(string email)
        {
            int id = Int32.Parse(val.Substring(6, (val.Length-1)-5));
            inzPJATKSNM.Controllers.SurveyController.saveSubscriptionEmail(email, id);
        }
    }
}