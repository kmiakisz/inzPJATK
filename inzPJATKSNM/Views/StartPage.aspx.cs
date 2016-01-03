using inzPJATKSNM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
        protected void Page_Load(object sender, EventArgs e)
        {

      
                if (Request.QueryString["Id"] != null)
                {
                    id = int.Parse(Request.QueryString["Id"]);
                    blokowaneIP = inzPJATKSNM.Controllers.SurveyController.getBlockedIPs(id);
                    if (blokowaneIP.Count != 0)
                    {
                        
                            if (blokowaneIP.Contains(inzPJATKSNM.Controllers.CommonController.GetVisitorIPAddress()))
                            {
                                //wyjebac modala ze juz glosowal i wyjebac ze strony
                            }
                        
                    }
                    
                    dziela = new List<Dzieło>();
                    dziela = inzPJATKSNM.Controllers.SurveyController.getDziela(id);
                    ilDziel = dziela.Count();
                }
                else
                {
                    //tu dodac modal z errorem o pustym id
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
            foreach(Dzieło dzielo in dziela){
               String ocena = Request.Form["0"];
                ocenyDziel.Add(dzielo.Id_dzieło,int.Parse(ocena));
               
            }
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "subscriptionOpenModal();", true);
            
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