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
        Glosujący glosujacy = new Glosujący();
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
                if (Request.QueryString["Id"] != null)
                {
                    id = int.Parse(Request.QueryString["Id"]);
                    dziela = new List<Dzieło>();
                    dziela = inzPJATKSNM.Controllers.SurveyController.getDziela(id);
                }
                else
                {
                    //tu dodac modal z errorem o pustym id
                }
        }

        protected void Accept_Click(object sender, EventArgs e)
        {
            glosujacy.Id_Narod = int.Parse(nationalityDDL.SelectedValue);
            glosujacy.Id_Plec = int.Parse(sexDDL.SelectedValue);
            glosujacy.Id_Wiek = int.Parse(ageDDL.SelectedValue);
            //ClientScript.RegisterStartupScript(GetType(),"closeModal();", true);
           
        }
        public void loadDzielaFromDB(int idAnkieta)
        {
            
        }
    }
}