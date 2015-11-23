using inzPJATKSNM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class EditExistingSurvey : System.Web.UI.Page
    {
        public inzPJATKSNM.Models.Ankieta ankieta = new inzPJATKSNM.Models.Ankieta();
        List<String> listaURLZdjec = new List<String>();
        String visitorIP;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.QueryString["Id"]!=null)
            {
                int id = int.Parse(Request.QueryString["Id"]);
                listaURLZdjec = inzPJATKSNM.Controllers.EditExistingSurveyController.getSurveyPhotos(id);
                ankieta = inzPJATKSNM.Controllers.EditExistingSurveyController.getSurvey(id);
                MusicDropDownList.DataValueField = ankieta.Id_Muzyka.ToString();
                example1.Value = ankieta.Data_zak.ToString();
                SurveyNameTextBox1.Text = ankieta.Nazwa;
                ServeyDescribtionTextBox1.Text = ankieta.Opis_ankiety;
                Data_zakLAb.Text = ankieta.Data_rozp.ToString();
                visitorIP = inzPJATKSNM.Controllers.CommonController.GetVisitorIPAddress().ToString();

            }
            else
            {
                //tu dodac modal 
            }
            
            
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