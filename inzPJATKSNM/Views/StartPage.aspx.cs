using inzPJATKSNM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class StartPage : System.Web.UI.Page
    {
        Dictionary<int, int> ocenyDziel = new Dictionary<int, int>();
        public List<Dzieło> dziela;
        int id,idNarod,idPlec,idWiek;
       public int ilDziel = 0;
       List<String> blokowaneIP;
       public String token;
       Dictionary<string, string> oceny = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
          
        
                if (Request.QueryString["Id"] != null)
                {
                    id = int.Parse(Request.QueryString["Id"]);
                    string type = inzPJATKSNM.Controllers.SurveyController.getSurveyType(id);
                    blokowaneIP = inzPJATKSNM.Controllers.SurveyController.getBlockedIPs(id);
                    if (type.Equals("PUBLIC"))
                    {
                        if (blokowaneIP.Count != 0)
                        {
                            if (blokowaneIP.Contains(inzPJATKSNM.Controllers.CommonController.GetVisitorIPAddress()))
                            {
                                Response.Redirect("Surveys.aspx?val=BlockedIp");
                                throw new System.AccessViolationException("Ip is in blocked list", new Exception());
                            }
                        }
                        dziela = new List<Dzieło>();
                        dziela = inzPJATKSNM.Controllers.SurveyController.getDziela(id);
                        ilDziel = dziela.Count();
                    }
                    else
                    {
                        if (Request.QueryString["Token"] != null)
                        {
                            token = Request.QueryString["Token"];
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "subscriptionOpenModal+();", true);
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
            foreach(KeyValuePair<string,string> kvp in ocenyDziel2){
                
                if (i <dziela.Count)
                {
                    ocenyDziel.Add(Int32.Parse(kvp.Key), Int32.Parse(kvp.Value)-1);
                    i++;
                }
       
            }
            idNarod = (int)ViewState["idNarod"];
            idPlec = (int)ViewState["idPlec"];
            idWiek = (int)ViewState["idWiek"];
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

            Response.Redirect("Surveys.aspx");
           
            
            //Modal z podziekowaniami i po ok przeniesienie na strone ze wszystkimi trwajacymi ankietami - lub wypierdalaj stąd (zamykamy okno)
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
           
            //Otworz kurwa ten modal
        }
        protected void subscription_Click(object sender, EventArgs e)
        {
            idNarod = int.Parse(ViewState["idNarod"].ToString());
            idWiek = int.Parse(ViewState["idWiek"].ToString());
            idPlec = int.Parse(ViewState["idPlec"].ToString());

            inzPJATKSNM.Controllers.SurveyController.saveGlosujacy(null, idNarod, idWiek, idPlec, id);
            //Modal z podziekowaniami i po ok przeniesienie na strone ze wszystkimi trwajacymi ankietami - lub wypierdalaj stąd (zamykamy okno)
        }
    }
}