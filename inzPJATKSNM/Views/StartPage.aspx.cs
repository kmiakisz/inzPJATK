using inzPJATKSNM.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class StartPage : System.Web.UI.Page
    {
        Dictionary<int, float> ocenyDziel = new Dictionary<int, float>();
        public List<Dzieło> dziela;
        int id, idNarod, idPlec, idWiek;
        public int ilDziel = 0;
        List<String> blokowaneIP;
        public String token;
        string type = "";
        Dictionary<string, string> oceny = new Dictionary<string, string>();
        List<String> adressCollection = new List<string>();
        String addressToCheck;
        bool isIpExists = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            addressToCheck = inzPJATKSNM.Controllers.CommonController.GetVisitorIPAddress();
            isIpExists = inzPJATKSNM.Controllers.SurveyController.CheckIpAddress(addressToCheck, adressCollection);

            if (!isIpExists)
            {
                HiddenField2.Value = "0";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "starVote()", " alert(' page loaded ');", true);
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "starVote()", "", true);
            }
            else
            {
                HiddenField2.Value = "1";
            }
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "starVote()", " alert(' page loaded ');", true);
            if (Request.QueryString["Id"] != null)
            {

                id = int.Parse(Request.QueryString["Id"]);
                try
                {
                    type = inzPJATKSNM.Controllers.SurveyController.getSurveyType(id);
                    blokowaneIP = inzPJATKSNM.Controllers.SurveyController.getBlockedIPs(id);
                }
                catch (Exception ex)
                {
                    Response.Redirect("Surveys.aspx?err=" + ex.Message);
                }

                if (type.Equals("PUBLIC"))
                {
                    if (blokowaneIP.Count != 0)
                    {

                        if (blokowaneIP.Contains(inzPJATKSNM.Controllers.CommonController.GetVisitorIPAddress()))
                        {
                            Response.Redirect("Surveys.aspx?val=BlockedIp");
                        }


                    }
                    dziela = new List<Dzieło>();
                    try
                    {
                        dziela = inzPJATKSNM.Controllers.SurveyController.getDziela(id);
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("Surveys.aspx?err=" + ex.Message);
                    }

                    ilDziel = dziela.Count();
                }
                else
                {
                    if (Request.QueryString["Token"] != null)
                    {
                        token = Request.QueryString["Token"];
                        try
                        {
                            if (inzPJATKSNM.Controllers.SurveyController.checkToken(token, id))
                            {
                                dziela = new List<Dzieło>();
                                dziela = inzPJATKSNM.Controllers.SurveyController.getDziela(id);
                                ilDziel = dziela.Count();
                            }
                            else
                            {
                                Response.Redirect("Surveys.aspx?val=UsedToken");
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Redirect("Surveys.aspx?err=" + ex.Message);
                        }

                    }
                    else
                    {
                        Response.Redirect("Surveys.aspx?val=EmptyToken");
                    }
                }

            }
            else
            // throw new System.AccessViolationException("Token cannot be empty", new Exception());
            { //throw new System.AccessViolationException("Token was used before", new Exception());
                // throw new System.ArgumentException("Parameter ID cannot be null", "original");
                Response.Redirect("Surveys.aspx?val=EmptyId");
            }

        }

        protected void Accept_Click(object sender, EventArgs e)
        {

            ViewState["idNarod"] = int.Parse(nationalityDDL.SelectedValue);
            ViewState["idPlec"] = int.Parse(sexDDL.SelectedValue);
            ViewState["idWiek"] = int.Parse(ageDDL.SelectedValue);

            //ClientScript.RegisterStartupScript(GetType(),"closeModal();", true);

        }
        public void loadDzielaFromDB(int idAnkieta)
        {

        }

        protected void vote_Click(object sender, EventArgs e)
        {


            string cos = TextBox1.Text;
            string removeString = "undefined";
            cos = cos.Remove(cos.IndexOf(removeString), removeString.Length);
            Dictionary<string, string> ocenyDziel2 = new Dictionary<string, string>();
            String[] splitted = cos.Split(';');
            int j = 0;
            foreach (String ankietaOcena in splitted)
            {

                if (j < dziela.Count)
                {
                    String[] pociete = ankietaOcena.Split(',');
                    ocenyDziel2.Add(pociete[0], pociete[1]);
                    j++;
                }


            }
            int i = 0;
            float mnoznik = 10 / ocenyDziel2.Count;
            foreach (KeyValuePair<string, string> kvp in ocenyDziel2)
            {

                if (i < dziela.Count)
                {
                    ocenyDziel.Add(Int32.Parse(kvp.Key), (Int32.Parse(kvp.Value) - 1) * mnoznik);
                    i++;
                }

            }
            if (!isIpExists)
            {
                idNarod = (int)ViewState["idNarod"];
                idPlec = (int)ViewState["idPlec"];
                idWiek = (int)ViewState["idWiek"];
            }
            else
            {
                String ip = inzPJATKSNM.Controllers.CommonController.GetVisitorIPAddress();
                Glosujący g = inzPJATKSNM.Controllers.SurveyController.getInfoByIp(ip);
                idNarod = g.Id_Narod;
                idPlec = g.Id_Plec;
                idWiek = g.Id_Wiek;
            }
            try
            {
                inzPJATKSNM.Controllers.SurveyController.saveAll(ocenyDziel, id, "", idNarod, idWiek, idPlec);
                if (inzPJATKSNM.Controllers.SurveyController.getSurveyType(id).Equals("PUBLIC"))
                {
                    if (inzPJATKSNM.Controllers.CommonController.GetVisitorIPAddress() != null)
                    {
                        inzPJATKSNM.Controllers.SurveyController.saveIPAddress(inzPJATKSNM.Controllers.CommonController.GetVisitorIPAddress(), id);
                    }
                    else
                    {
                        inzPJATKSNM.Controllers.SurveyController.saveIPAddress("127.0.0.1", id);
                    }

                }
                else
                {
                    inzPJATKSNM.Controllers.SurveyController.changeTokenState(token, id);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("Surveys.aspx?err=" + ex.Message);
            }

            Response.Redirect("Surveys.aspx?val=thanks" + id);

        }

        protected void cancel_Click(object sender, EventArgs e)
        {

        }
        protected void subscription_Click(object sender, EventArgs e)
        {
            if (isIpExists)
            {
                idNarod = int.Parse(ViewState["idNarod"].ToString());
                idWiek = int.Parse(ViewState["idWiek"].ToString());
                idPlec = int.Parse(ViewState["idPlec"].ToString());
                try
                {
                    inzPJATKSNM.Controllers.SurveyController.saveGlosujacy(null, idNarod, idWiek, idPlec, id);
                }
                catch (Exception ex)
                {
                    Response.Redirect("Surveys.aspx?err=" + ex.Message);
                }
                Response.Redirect("Surveys.aspx");
                //Modal z podziekowaniami i po ok przeniesienie na strone ze wszystkimi trwajacymi ankietami
            }
        }
        protected void EngButton_Click(object sender, ImageClickEventArgs e)
        {
            string selectedLanguage = "en-GB";
            HttpCookie cookie = new HttpCookie("CultureInfo");
            cookie.Value = selectedLanguage;
            Response.Cookies.Add(cookie);
            Response.Redirect(Request.RawUrl);
        }

        protected void PolButton_Click(object sender, ImageClickEventArgs e)
        {
            string selectedLanguage = "pl-PL";
            HttpCookie cookie = new HttpCookie("CultureInfo");
            cookie.Value = selectedLanguage;
            Response.Cookies.Add(cookie);
            Response.Redirect(Request.RawUrl);
        }
        protected override void InitializeCulture()
        {
            HttpCookie cookie = Request.Cookies["CultureInfo"];

            if (cookie != null && cookie.Value != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value); ;
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pl-PL");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
            }

            base.InitializeCulture();
        }
    }
}