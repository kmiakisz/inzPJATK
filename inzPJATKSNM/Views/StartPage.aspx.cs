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
        public List<Dzieło> dziela;
        Glosujący glosujacy;
        int id;
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
            glosujacy = new Glosujący();
            glosujacy.Id_Narod = int.Parse(nationalityDDL.SelectedValue);
            glosujacy.Id_Plec = int.Parse(sexDDL.SelectedValue);
            glosujacy.Id_Wiek = int.Parse(ageDDL.SelectedValue);
            //ClientScript.RegisterStartupScript(GetType(),"closeModal();", true);
           
        }
        public void loadDzielaFromDB(int idAnkieta)
        {
            
        }

        protected void vote_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "text", "subscriptionOpenModal();", true);
            //Modal z podziekowaniami i po ok przeniesienie na strone ze wszystkimi trwajacymi ankietami - lub wypierdalaj stąd (zamykamy okno)
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            //Otworz kurwa ten modal
        }
        protected void subscription_Click(object sender, EventArgs e)
        {

            //Modal z podziekowaniami i po ok przeniesienie na strone ze wszystkimi trwajacymi ankietami - lub wypierdalaj stąd (zamykamy okno)
        }
    }
}