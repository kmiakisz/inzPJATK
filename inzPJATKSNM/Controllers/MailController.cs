using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class MailController
    {
        public static  List<String> getMailList()
        {
            List<String> mailList = new List<String>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                Sqlcon.Open();
                string query = "SELECT Email FROM Glosujący";
                using (SqlCommand command = new SqlCommand(query, Sqlcon))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mailList.Add(reader.GetString(0));
                        }
                    }
                }
                Sqlcon.Close();
            }
            return mailList;
        }

        public static void sendMail(String subject,String body,List<String> listaMaili)
        {

              MailMessage message = new MailMessage();
            message.From = new MailAddress("ankietySNM@gmail.com");
            foreach(String mail in listaMaili)
            {
                message.To.Add(new MailAddress(mail));
            }
            message.Subject = subject;
            message.Body = body;

            SmtpClient client = new SmtpClient();
            client.Send(message);
        }

        public static void dummy(String subject,String body,String mail)
        {

            MailMessage message = new MailMessage();
            message.From = new MailAddress("ankietySNM@gmail.com");
            message.To.Add(mail);
            message.Subject = subject;
            message.Body = body;

            SmtpClient client = new SmtpClient();
            client.Send(message);
        }

    }
}