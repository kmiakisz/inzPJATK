using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class SendMailForm : System.Web.UI.Page
    {
        public int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                id = int.Parse(Request.QueryString["Id"]);
                if (!IsPostBack)
                {
                    body.Value += " Ankieta pod adresem: http://localhost:11222/Views/StartPage.aspx?Id=" + id;
                }
               
            }
            else
            {
                //modal o pustym id
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }

        public List<String> getMail()
        {
            List<String> userMailList = new List<string>();
            try
            {
                userMailList = inzPJATKSNM.Controllers.MailController.getMailList();
            }
            catch (Exception ex)
            {
                Response.Redirect("/Views/ShowSurveys.aspx?err=" + ex.Message);
            }
            
            return userMailList;

        }

        protected void Accept_Click(object sender, EventArgs e)
        {
            int il = 0;
            try{
            if (inzPJATKSNM.Controllers.SurveyController.getSurveyType(id).Equals("PUBLIC"))
            {
                il = inzPJATKSNM.Controllers.MailController.sendPublicMail(subject.Value, body.Value, getMail());
            }
            else
            {
                il = inzPJATKSNM.Controllers.MailController.sendPrivateMail(subject.Value, body.Value, getMail(),id);
            }
            }
            catch (Exception ex)
            {
                Response.Redirect("/Views/ShowSurveys.aspx?err=" + ex.Message);
            }
            
            
            Response.Redirect("/Views/ShowSurveys.aspx?val=" + il);
        }
    }
}