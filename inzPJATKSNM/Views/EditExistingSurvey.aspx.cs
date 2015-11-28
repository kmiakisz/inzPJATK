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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    int id = int.Parse(Request.QueryString["Id"]);
                    listaURLZdjec = inzPJATKSNM.Controllers.EditExistingSurveyController.getSurveyPhotos(id);
                    ankieta = inzPJATKSNM.Controllers.EditExistingSurveyController.getSurvey(id);
                    MusicDropDownList.DataValueField = ankieta.Id_Muzyka.ToString();
                    example1.Value = ankieta.Data_zak.ToString();
                    SurveyNameTextBox1.Text = ankieta.Nazwa;
                    ServeyDescribtionTextBox1.Text = ankieta.Opis_ankiety;
                    Data_zakLAb.Text = ankieta.Data_rozp.ToString();


                }
                else
                {
                    //tu dodac modal z errorem o pustym id
                }
            }
           
            
            
        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            
            ankieta.Id_ankiety = int.Parse(Request.QueryString["Id"]);
            ankieta.Id_Muzyka = 4;
           // ankieta.Id_Muzyka=int.Parse(MusicDropDownList.SelectedValue);
            ankieta.Data_zak=DateTime.Parse(example1.Value);
            ankieta.Nazwa = SurveyNameTextBox1.Text;
            ankieta.Opis_ankiety=ServeyDescribtionTextBox1.Text;
            try
            {
                inzPJATKSNM.Controllers.EditExistingSurveyController.saveEditsurvey(ankieta, listaURLZdjec);//ladujemy te same zdjecia do zmiany po updejcie widoku
            }
            catch (Exception ex)
            {
                //wywalic modala o nieudanej edycji

            }
            Response.Redirect("ShowSurveys.aspx");
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowSurveys.aspx");
        }        
    }
}