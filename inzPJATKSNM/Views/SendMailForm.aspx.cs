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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                int id = int.Parse(Request.QueryString["Id"]);
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
            List<String> userMailList;
            userMailList = inzPJATKSNM.Controllers.MailController.getMailList();
            return userMailList;

        }

        protected void Accept_Click(object sender, EventArgs e)
        {
            int il = inzPJATKSNM.Controllers.MailController.sendMail(subject.Value, body.Value, getMail());
            Response.Redirect("/Views/ShowSurveys.aspx?val=" + il);
        }
    }
}