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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //inzPJATKSNM.Controllers.MailController.sendMail(subject.Value, body.Value, getMail());
        }

        public List<String> getMail()
        {
            List<String> userMailList;
            userMailList = inzPJATKSNM.Controllers.MailController.getMailList();
            return userMailList;

        }

        protected void Accept_Click(object sender, EventArgs e)
        {

        }
    }
}