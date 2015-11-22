using inzPJATKSNM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class EditExistingSurvey : System.Web.UI.Page
    {
        public Ankieta ankieta = new Ankieta();
        
        int i = 0;
       
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowSurveys.aspx");
        }
        [WebMethod]
        public static void returnId(int id)
        {
            EditExistingSurvey ees = new EditExistingSurvey();
            ees.getData(id);
            
        }
        public void getData(int id)
        {
            inzPJATKSNM.Controllers.EditExistingSurveyController.getSurveyPhotos(id);
            ankieta = inzPJATKSNM.Controllers.EditExistingSurveyController.getSurvey(id);
           
            //SurveyNameTextBox = this.FindControl("SurveyNameTextBox") as TextBox;
            //ServeyDescribtionTextBox = this.FindControl("ServeyDescribtionTextBox") as TextBox;
           // SurveyNameTextBox1.Text = ankieta.Nazwa;
           // Console.WriteLine(ankieta.Nazwa);
            //ServeyDescribtionTextBox1.Text = ankieta.Opis_ankiety;
        }

     public String getNazwa(){
         return ankieta.Nazwa;
     }

  
        
    }
}