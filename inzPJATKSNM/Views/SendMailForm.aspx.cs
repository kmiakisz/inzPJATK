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
            MailMessage message = new MailMessage();
            message.From = new MailAddress("ankietySNM@gmail.com");


            message.To.Add(new MailAddress("s10509@pjwstk.edu.pl "));
            message.Subject = "CO TAM ? ";
            message.Body = "SIEMA MORDECZKO !";
            //message.CC.Add(new MailAddress("mateuszonasz@gmail.com"));

            SmtpClient client = new SmtpClient();
            client.Send(message);
        }
    }
}