using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class AddAuthor : System.Web.UI.Page
    {
        String name = "",surname="";
        int Id_nar, Id_plec, Id_epoka;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DodajButton_Click(object sender, EventArgs e)
        {
            name = AuthorNameTextBox.Text;
            surname = AuthorSurnameTextBox.Text;
            Id_nar = int.Parse(NationalityDropDownList.SelectedValue);
            Id_plec = int.Parse(PlecDropDownList.SelectedValue);
            Id_epoka = int.Parse(EpokaDropDownList1.SelectedValue);
            try
            {
                
                inzPJATKSNM.Controllers.AutorController.addNewAuthor(name, surname, Id_nar, Id_plec, Id_epoka);
            }
            catch (Exception ex)
            {
                Response.Redirect("ShowSurveys.aspx?val=" + ex.Message);
            }
       
        }
    }
}