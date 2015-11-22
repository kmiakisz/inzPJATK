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
        Glosujący glosujacy = new Glosujący();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Accept_Click(object sender, EventArgs e)
        {
            glosujacy.Id_Narod = int.Parse(nationalityDDL.SelectedValue);
            glosujacy.Id_Plec = int.Parse(sexDDL.SelectedValue);
            glosujacy.Id_Wiek = int.Parse(ageDDL.SelectedValue);
            //ClientScript.RegisterStartupScript(GetType(),"closeModal();", true);
           
        }
    }
}