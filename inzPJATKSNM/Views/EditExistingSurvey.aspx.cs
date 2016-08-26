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
        public Dictionary<Int32, String> freePhotos { get; set; }
        public Dictionary<Int32, String> usedPhotos {get;set;}
        public Int32 currentKey { get; set; }
        public String currentValue { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            freePhotos = new Dictionary<Int32, String>();
            usedPhotos = new Dictionary<Int32, String>();
            try
            {
                freePhotos = inzPJATKSNM.Controllers.EditExistingSurveyController.getFreePhotos();
                usedPhotos = inzPJATKSNM.Controllers.EditExistingSurveyController.getUsedPhotos(int.Parse(Request.QueryString["Id"]));

            }
            catch (Exception ex)
            {
                Response.Redirect("ShowSurveys?+err=" + ex.Message);
            }
            
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    currentValue = "";
                    currentKey = new Int32();
                    int id = int.Parse(Request.QueryString["Id"]);
                    try
                    {
                        listaURLZdjec = inzPJATKSNM.Controllers.EditExistingSurveyController.getSurveyPhotos(id);
                        ankieta = inzPJATKSNM.Controllers.EditExistingSurveyController.getSurvey(id);
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("ShowSurveys.aspx?err=" + ex.Message);
                    }
                   
                    MusicDropDownList.DataValueField = ankieta.Id_Muzyka.ToString();
                    example1.Value = ankieta.Data_zak.ToString();
                    SurveyNameTextBox1.Text = ankieta.Nazwa;
                    ServeyDescribtionTextBox1.Text = ankieta.Opis_ankiety;
                    Data_zakLAb.Text = ankieta.Data_rozp.ToString();
               
                  
                }
                else
                {
                    Response.Redirect("ShowSurveys.aspx?err=PusteId");
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
            ankieta.Type = TypeDropDownList.Text;
            try
            {
                inzPJATKSNM.Controllers.EditExistingSurveyController.saveEditsurvey(ankieta, listaURLZdjec);//ladujemy te same zdjecia do zmiany po updejcie widoku
            }
            catch (Exception ex)
            {
                Response.Redirect("ShowSurveys.aspx?err="+ex.Message);

            }
            Response.Redirect("ShowSurveys.aspx");
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowSurveys.aspx");
        }
  /*      protected void addToSurvey(int id, string url)
        {
            freePhotos.Remove(id);
            usedPhotos.Add(id, url);
        }
        protected void removeFromSurvey(int id, string url)
        {
            usedPhotos.Remove(id);
            freePhotos.Add(id, url);
        }
*/
        protected void Add_Button_Click(object sender, EventArgs e)
        {
            //addToSurvey(currentKey, currentValue);
        }

        protected void Del_Button_Click(object sender, EventArgs e)
        {
            //removeFromSurvey(currentKey, currentValue);
        }
        [WebMethod]
        public void RemovePhotoFromSurvey(int id, string url)
        {
            usedPhotos.Remove(id);
            freePhotos.Add(id, url);
        }
        [WebMethod]
        public void AddPhotoToSurvey(int id, string url)
        {
            freePhotos.Remove(id);
            usedPhotos.Add(id, url);
        }
    }
}